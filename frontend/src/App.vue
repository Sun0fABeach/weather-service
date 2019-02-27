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
}

#app {
  display: flex;
  flex-direction: column;
  height: 100vh;
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
}
</style>
