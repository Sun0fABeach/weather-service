<template>
  <transition :name="'swipe-' + swipeDirection">
    <div class="report-panel" v-show="displayed">

      <h2>{{ dayData.dayName }}</h2>
      <h3>{{ dayData.dateString }}</h3>

      <PanelSection
        :label="`${labelPrefix} Temperatur`"
        :value="`${roundToDecimal(tweenedVals.temp)} °C`"
      />
      <PanelSection
        :label="`${labelPrefix} Luftfeuchtigkeit`"
        :value="`${roundToInt(tweenedVals.humidity)} %`"
      />
      <PanelSection
        :label="`${labelPrefix} Windgeschwindigkeit`"
        :value="`${roundToDecimal(tweenedVals.windspeed)} m/s`"
      />
    </div>
  </transition>
</template>

<script>
import PanelSection from '@/components/ReportPanelSection.vue'
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
      return this.forToday ? 'Aktuelle' : 'ø'
    }
  },
  watch: {
    dayData: {
      handler (data) {
        const results = pick(data, ['temp', 'humidity', 'windspeed'])

        if (this.displayed) {
          new TWEEN.Tween(this.tweenedVals)
            .to(results, 2000)
            .easing(TWEEN.Easing.Quadratic.InOut)
            .delay(200)
            .start()
        } else {
          this.tweenedVals = results
        }
      },
      immediate: true
    }
  },
  methods: {
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

  &.swipe-left-enter-active, &.swipe-left-leave-active,
  &.swipe-right-enter-active, &.swipe-right-leave-active {
    position: absolute;
    left: 0;
    right: 0;
    transition: transform 0.25s;
  }
  &.swipe-left-enter, &.swipe-right-leave-to {
    transform: translateX(100%);
  }
  &.swipe-right-enter, &.swipe-left-leave-to {
    transform: translateX(-100%);
  }

  > h2 {
    margin-bottom: 0.25rem;
    font-size: 1.75rem;
  }
  > h3 {
    margin: 0;
  }
}
</style>
