<template>
  <section>
    <DayTabs :weekdays="dayMap" v-model="selectedDay" />
    <ReportPanels :report="preparedReport" :selected="selectedDay" />
  </section>
</template>

<script>
import DayTabs from '@/components/DayTabs'
import ReportPanels from '@/components/ReportPanels'
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
          dateString: this.formatDate(this.addDays(this.report.date, idx))
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
  }
}
</script>

<style lang="scss" scoped>
section {
  flex-grow: 1;
  display: flex;
  flex-direction: column;
  overflow-x: hidden; // for swipe effect of report panels
}
</style>
