<template>
  <main id="app">
    <h1>Wetterdienst</h1>
    <InputPanel @report="data => weatherData = data" />
    <transition name="fade">
      <WeatherReport :report="weatherData" v-if="weatherData" />
    </transition>
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

<style lang="scss" scoped>
#app {
  display: flex;
  flex-direction: column;
  height: 100vh;
  padding: 0.5rem;
  font-family: 'Avenir', Helvetica, Arial, sans-serif;

  > h1 {
    margin: 0;
    text-align: center;
  }

  .fade-enter-active, .fade-leave-active {
    transition: opacity 150ms;
  }
  .fade-enter, .fade-leave-to {
    opacity: 0;
  }
}
</style>

<style lang="scss">
body {
  margin: 0;
  padding: 0;
  background-color: lightcyan;
}

* {
  box-sizing: border-box;
}
</style>
