<template>
  <FetchJson
    url="weather/forecast"
    v-slot="{ fetch, loading, error, clearError }"
  >
    <div class="input-wrap" :style="errorShadow">
      <SearchInput
        v-model.trim="query"
        @input="clearError"
        @submit="requestWeather(fetch)"
      />
      <SearchButton
        @click="requestWeather(fetch)"
        :requestPending="loading"
      />
    </div>
    <SearchError :errorMsg="error && buildErrorMsg(error)" />
  </FetchJson>
</template>


<script>
import { capitalize } from 'lodash-es'
import SearchInput from './SearchInput'
import SearchButton from './SearchButton'
import SearchError from './SearchError'
import FetchJson from '@/components/fetch-json'

export default {
  components: {
    SearchInput,
    SearchButton,
    SearchError,
    FetchJson
  },
  props: {
    inputVal: {
      type: String
    }
  },
  data () {
    return {
      query: ''
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
    async requestWeather (fetchCb) {
      const key = this.query.match(/\d{5}/) ? 'zipCode' : 'city'
      const res = await fetchCb(`?${key}=${this.query}`)
      if (res.success) {
        const data = res.json
        data.date = new Date()
        data.query = capitalize(this.query)
        this.$emit('result', data)
      }
    },

    buildErrorMsg (error) {
      return error.response.status === 404
        ? 'Ort / PLZ unbekannt.' : 'Ein Fehler ist aufgetreten.'
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

$hover-transition-duration: 200ms;

div {
  > .input-wrap {
    display: flex;
    align-items: center;
    padding-right: $search-wrap-pad-x - $search-btn-pad;
    padding-left: $search-wrap-pad-x;
    border: 1px solid lighten($color-secondary, 50%);
    border-radius: 2rem;
    transition: box-shadow $error-transition-duration,
                border-color $hover-transition-duration;

    &:hover {
      border-color: lighten($color-primary, 20%);
    }
  }
}
</style>
