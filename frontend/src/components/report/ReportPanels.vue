<template>
  <div class="panels-container">
    <template v-for="(entry, idx) in report">
      <ReportPanel
        :displayed="idx === selected || $mq === 'desktop'"
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
      validator: entries => !entries.some(entry =>
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
        if (this.$mq === 'desktop') {
          this.swipeDirection = 'none'
        } else if (oldVal === undefined) {
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
.panels-container {
  flex-grow: 1;
  display: flex;
  justify-content: center;
  align-items: flex-start;
  position: relative; // for swipe effect via absolute positioned panels
}
</style>
