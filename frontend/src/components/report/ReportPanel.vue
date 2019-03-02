<template>
  <transition :name="'swipe-' + swipeDirection">
    <div class="report-panel" v-show="displayed">

      <h2>{{ dayData.dayName }}</h2>
      <h3>{{ `${dayData.dateString}, ${dayData.query}` }}</h3>

      <div class="panel-sections">
        <PanelSection
          :label="`${labelPrefix} Temperatur`"
          :value="`${paddedDecimalString(tweenedVals.temp)} °C`"
        />
        <PanelSection
          :label="`${labelPrefix} Luftfeuchtigkeit`"
          :value="`${Math.round(tweenedVals.humidity)} %`"
        />
        <PanelSection
          :label="`${labelPrefix} Windgeschwindigkeit`"
          :value="`${paddedDecimalString(tweenedVals.windspeed)} m/s`"
        />
      </div>
    </div>
  </transition>
</template>

<script>
import PanelSection from './ReportPanelSection.vue'
import TWEEN from '@tweenjs/tween.js'
import { pick } from 'lodash-es'

export default {
  components: {
    PanelSection
  },
  props: {
    dayData: {
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
    },
    swipeDirection: {
      type: String,
      default: 'left',
      validator: direction => ['left', 'right'].indexOf(direction) !== -1
    }
  },
  data () {
    return {
      tweenedVals: {
        temp: 0,
        humidity: 0,
        windspeed: 0
      }
    }
  },
  computed: {
    labelPrefix () {
      return this.forToday ? 'Aktuelle' : 'Ø'
    }
  },
  watch: {
    dayData: {
      handler (data) {
        const delay = 250 // delay until fade/swipe in finished
        const results = pick(data, ['temp', 'humidity', 'windspeed'])

        if (this.displayed) {
          new TWEEN.Tween(this.tweenedVals)
            .to(results, 2000)
            .easing(TWEEN.Easing.Quadratic.InOut)
            .delay(delay)
            .start()
        } else {
          setTimeout(() => { this.tweenedVals = results }, delay)
        }
      },
      immediate: true
    }
  },
  methods: {
    paddedDecimalString (val, precision = 2) {
      const roundedStr = val.toPrecision(precision)
      const len = precision === 1 ? 1 : precision + 1 // consider decimal sign
      return roundedStr.substring(0, len)
    }
  }
}
</script>

<style lang="scss" scoped>
.report-panel {
  display: flex;
  flex-direction: column;
  text-align: center;

  @media screen and (min-width: $min-desktop) {
    margin: 0 0.75rem;
  }

  &.swipe-left-enter-active, &.swipe-left-leave-active,
  &.swipe-right-enter-active, &.swipe-right-leave-active {
    position: absolute;
    top: 0;
    right: 0;
    bottom: 0;
    left: 0;
    transition: transform 250ms;
  }
  &.swipe-left-enter, &.swipe-right-leave-to {
    transform: translateX(100%);
  }
  &.swipe-right-enter, &.swipe-left-leave-to {
    transform: translateX(-100%);
  }

  > h2 {
    margin-top: 1rem;
    margin-bottom: 0.25rem;
    font-size: 1.75rem;
  }
  > h3 {
    margin: 0;
  }

  > .panel-sections {
    flex-grow: 1;
    display: flex;
    flex-direction: column;
    justify-content: space-around;
  }
}
</style>
