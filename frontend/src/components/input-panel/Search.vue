<template>
  <div>
    <div class="input-wrap" :style="errorShadow">
      <SearchInput
        v-model.trim="query"
        @input="errorMsg = ''"
        @submit="requestWeather"
      />
      <SearchButton @click="requestWeather" :requestPending="requestPending" />
    </div>
    <SearchError :errorMsg="errorMsg" />
  </div>
</template>


<script>
import { capitalize } from 'lodash-es'
import axios from 'axios'
import SearchInput from './SearchInput'
import SearchButton from './SearchButton'
import SearchError from './SearchError'

export default {
  components: {
    SearchInput,
    SearchButton,
    SearchError
  },
  props: {
    inputVal: {
      type: String
    }
  },
  data () {
    return {
      query: '',
      errorMsg: '',
      requestPending: false
    }
  },
  computed: {
    errorShadow () {
      return {
        boxShadow: this.errorMsg ? '0 2px 2px 0 red' : null
      }
    }
  },
  methods: {
    async requestWeather () {
      if (!this.query) {
        return
      }

      try {
        this.errorMsg = ''
        this.requestPending = true

        const response = await axios.get(this.buildRequestUrl(this.query))
        response.data.date = new Date()
        response.data.query = capitalize(this.query)
        this.$emit('result', response.data)

      } catch (error) {
        this.errorMsg = error.response.status === 404
          ? 'Ort / PLZ unbekannt.' : 'Ein Fehler ist aufgetreten.'

      } finally {
        this.requestPending = false
      }
    },

    buildRequestUrl () {
      const key = this.query.match(/\d{5}/) ? 'zipCode' : 'city'
      return `https://localhost:5001/api/weather/forecast?${key}=${this.query}`
    }
  },
  watch: {
    inputVal (str) {
      this.query = str
    }
  },
}
</script>


<style lang="scss" scoped>
div {
  --search-wrap-pad-x: 1.25rem;
  --search-btn-pad: 0.375rem;
  --error-transition-duration: 200ms;
  --hover-transition-duration: 200ms;

  > .input-wrap {
    display: flex;
    align-items: center;
    padding-right: calc(var(--search-wrap-pad-x) - var(--search-btn-pad));
    padding-left: var(--search-wrap-pad-x);
    border: 1px solid lighten($color-secondary, 50%);
    border-radius: 2rem;
    transition: box-shadow var(--error-transition-duration);
    transition: border-color var(--hover-transition-duration);

    &:hover {
      border-color: lighten($color-primary, 20%);
    }
  }
}

</style>
