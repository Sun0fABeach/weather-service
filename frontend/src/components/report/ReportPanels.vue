<template>
  <div>
    <template v-for="(entry, idx) in report">
      <ReportPanel
        :displayed="idx === selected"
        :swipeDirection="swipeDirection"
        :forToday="idx === 0"
        :dayData="entry"
        :key="idx"
      />
    </template>
  </div>
</template>

<script>
import ReportPanel from './ReportPanel'
import { has } from 'lodash-es'

export default {
  components: {
    ReportPanel
  },
  props: {
    report: {
      type: Array,
      required: true,
      validator: entries => entries.length === 6 && !entries.some(entry =>
        !has(entry, 'temp', 'humidity', 'windspeed', 'dayName', 'dateString')
      )
    },
    selected: {
      type: Number,
      required: true
    },
  },
  data () {
    return {
      swipeDirection: '' // set in watcher
    }
  },
  watch: {
    selected: {
      handler (newVal, oldVal) {
        if (oldVal === undefined) {
          this.swipeDirection = 'left' // first render, leftmost day selected
        } else {
          this.swipeDirection = newVal > oldVal ? 'left' : 'right'
        }
      },
      immediate: true
    }
  }
}
</script>

<style lang="scss" scoped>
div {
  position: relative; // for swipe effect via absolute positioned panels
}
</style>
