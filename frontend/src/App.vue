<template>
  <div id="app">
    <InputPanel @forecast="report => weatherData = report" />
    <WeatherReport :forecast="weatherData" />
  </div>
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
  padding: 0.5rem 0.25rem;
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
}
</style>
