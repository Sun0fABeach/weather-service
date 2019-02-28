<template>
  <main id="app">
    <InputPanel @report="data => weatherData = data" />
    <WeatherReport :report="weatherData" v-if="weatherData" />
  </main>
</template>

<script>
import InputPanel from '@/components/InputPanel'
import WeatherReport from '@/components/WeatherReport'

export default {
  components: {
    InputPanel,
    WeatherReport
  },
  data () {
    return {
      weatherData: null
    }
  }
}
</script>

<style lang="scss">
body {
  margin: 0;
  padding: 0;
  background-color: lightcyan;
}

* {
  box-sizing: border-box;
}

#app {
  display: flex;
  flex-direction: column;
  height: 100vh;
  padding: 0.5rem;
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
}
</style>
