<template>
  <div>
    <input
      type="text"
      placeholder="Suche nach Ort oder PLZ"
      v-model.trim="query"
      @keydown.enter="requestWeather"
    >
  </div>
</template>

<script>
import axios from 'axios'

export default {
  data () {
    return {
      query: ''
    }
  },
  methods: {
    async requestWeather () {
      if (!this.query) {
        return
      }
      try {
        const response = await axios.get(
          `https://localhost:5001/api/weather/forecast?city=${this.query}`
        )
        this.$emit('forecast', response.data)
        this.query = ''
      } catch (error) {
        console.log(error)
      }
    }
  }
}
</script>

<style lang="scss" scoped>

</style>
