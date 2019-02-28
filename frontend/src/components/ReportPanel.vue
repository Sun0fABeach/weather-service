<template>
  <div class="report-panel">
    <div>
      <span>{{ labelPrefix }} Temperatur</span>
      <span>{{ weather.temp }} °C</span>
    </div>
    <div>
      <span>{{ labelPrefix }} Luftfeuchtigkeit</span>
      <span>{{ weather.humidity }} %</span>
    </div>
    <div>
      <span>{{ labelPrefix }} Windgeschwindigkeit</span>
      <span>{{ weather.windspeed }} m/s</span>
    </div>

  </div>
</template>

<script>
export default {
  props: {
    weather: {
      type: Object,
      required: true
    },
    forToday: {
      type: Boolean,
      default: false
    }
  },
  computed: {
    labelPrefix () {
      return this.forToday ? 'Aktuelle' : 'ø'
    }
  }
}
</script>

<style lang="scss" scoped>
.report-panel {
  flex-grow: 1;
  display: flex;
  flex-direction: column;
  align-items: center;

  > div {
    display: flex;
    flex-direction: column;
    align-items: center;
    margin-top: 3rem;

    > span {
      &:first-of-type {
        font-size: 1.625rem;
      }
      &:last-of-type {
        font-size: 3.5rem;
        font-weight: bold;
      }
    }
  }
}
</style>
