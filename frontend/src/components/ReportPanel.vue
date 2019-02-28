<template>
  <div class="report-panel" v-show="displayed">
    <div>
      <span>{{ labelPrefix }} Temperatur</span>
      <span>{{ values.temp | roundToDecimal }} °C</span>
    </div>
    <div>
      <span>{{ labelPrefix }} Luftfeuchtigkeit</span>
      <span>{{ values.humidity | roundToInt }} %</span>
    </div>
    <div>
      <span>{{ labelPrefix }} Windgeschwindigkeit</span>
      <span>{{ values.windspeed | roundToDecimal }} m/s</span>
    </div>

  </div>
</template>

<script>
import TWEEN from '@tweenjs/tween.js'
import { pick } from 'lodash-es'

export default {
  props: {
    weather: {
      type: Object,
      required: true
    },
    forToday: {
      type: Boolean,
      default: false
    },
    displayed: {
      type: Boolean,
      default: false
    }
  },
  data () {
    return {
      values: {
        temp: 0,
        humidity: 0,
        windspeed: 0
      }
    }
  },
  computed: {
    labelPrefix () {
      return this.forToday ? 'Aktuelle' : 'ø'
    }
  },
  watch: {
    weather: {
      handler (report) {
        const results = pick(report, ['temp', 'humidity', 'windspeed'])

        if (this.displayed) {
          new TWEEN.Tween(this.values)
            .to(results, 2000)
            .easing(TWEEN.Easing.Quadratic.InOut)
            .start()
        } else {
          this.values = results
        }
      },
      immediate: true
    }
  },
  filters: {
    roundToInt (val) {
      return Math.round(val)
    },
    roundToDecimal (val) {
      return Math.round(val * 10) / 10
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
      text-align: center;

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
