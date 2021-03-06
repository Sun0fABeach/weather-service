<template>
  <transition name="fade">
    <section v-if="report">
      <DayTabs :weekdays="dayMap" v-model="selectedDay" />
      <ReportPanels :report="preparedReport" :selected="selectedDay" />
    </section>
  </transition>
</template>

<script>
import DayTabs from './DayTabs'
import ReportPanels from './ReportPanels'
import { pick } from 'lodash-es'

const dayNames = [
  'Sonntag', 'Montag', 'Dienstag', 'Mittwoch',
  'Donnerstag', 'Freitag', 'Samstag'
]

export default {
  components: {
    DayTabs,
    ReportPanels
  },
  props: {
    report: {
      type: Object
    }
  },
  data () {
    return {
      selectedDay: 0
    }
  },
  computed: {
    dayMap () {
      const weekdayNums = [
        this.report.current.day,
        ...this.report.forecast.map(entry => entry.day)
      ]
      return weekdayNums.map(dayNum => dayNames[dayNum])
    },
    preparedReport () {
      return [this.report.current, ...this.report.forecast].map(
        (entry, idx) => ({
          ...pick(entry, 'temp', 'humidity', 'windspeed'),
          dayName: this.dayMap[idx],
          dateString: this.formatDate(this.addDays(this.report.date, idx)),
          query: this.report.query
        })
      )
    }
  },
  methods: {
    formatDate (date) {
      return `${date.getDate()}.${date.getMonth() + 1}.${date.getFullYear()}`
    },
    addDays (date, addedDays) {
      return new Date(date.getTime() + addedDays * 86400000)
    }
  },
  watch: {
    report () {
      this.selectedDay = 0
    }
  }
}
</script>

<style lang="scss" scoped>
section {
  flex-shrink: 0;
  flex-grow: 1;
  display: flex;
  flex-direction: column;
  overflow: hidden; // for swipe effect of report panels

  &.fade-enter-active, &.fade-leave-active {
    transition: opacity 250ms;
  }
  &.fade-enter, &.fade-leave-to {
    opacity: 0;
  }
}
</style>
