<template>
  <div id="app">
    <input type="text" v-model.trim="query" @keydown.enter="getWeather">
    <div>{{ weather }}</div>
  </div>
</template>

<script>
import axios from 'axios'

export default {
  name: 'app',
  data () {
    return {
      query: '',
      weather: null
    }
  },
  methods: {
    async getWeather () {
      if (!this.query) {
        return
      }
      try {
        const response = await axios.get(
          `https://localhost:5001/api/weather/forecast?city=${this.query}`
        )
        this.weather = response.data
        this.query = ''
      } catch (error) {
        console.log(error)
      }
    }
  }
}
</script>

<style lang="scss">
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
}
</style>
