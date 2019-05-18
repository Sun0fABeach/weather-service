<template>
  <FetchJson
    url="weather/forecast"
    v-slot="{ fetch, loading, error, clearError }"
  >
    <div class="input-wrap" :style="errorShadow">
      <SearchInput
        :value="value"
        @input="val => $emit('input', val.trim())"
        @submit="requestWeather(fetch)"
      />
      <SearchButton
        :requestPending="loading"
        @click="requestWeather(fetch)"
      />
    </div>
    <SearchError :errorMsg="showError && error && buildErrorMsg(error)" />
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
    value: { // model
      type: String
    }
  },
  data () {
    return {
      showError: false
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
      const key = this.value.match(/\d{5}/) ? 'zipCode' : 'city'
      const res = await fetchCb(`?${key}=${this.value}`)
      if (res.success) {
        const data = res.json
        data.date = new Date()
        data.query = capitalize(this.value)
        this.$emit('result', data)
      } else {
        this.showError = true
      }
    },

    buildErrorMsg (error) {
      return error.response.status === 404
        ? 'Ort / PLZ unbekannt.' : 'Ein Fehler ist aufgetreten.'
    }
  },
  watch: {
    value (str) {
      this.showError = false
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
