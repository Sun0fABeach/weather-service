<template>
  <main id="app">
    <h1>Wetterdienst</h1>
    <InputPanel @report="data => weatherData = data" />
    <WeatherReport :report="weatherData" />

    <portal-target name="history" />
  </main>
</template>

<script>
import InputPanel from '@/components/input-panel/InputPanel'
import WeatherReport from '@/components/report/WeatherReport'

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

<style lang="scss" scoped>
#app {
  display: flex;
  flex-direction: column;
  height: 100vh;
  padding: 0.5rem;

  > h1 {
    margin: 0;
    text-align: center;
    margin-top: 0.25rem;

    @media screen and (min-width: $min-tablet) {
      margin-top: 1rem;
    }
  }
}
</style>
