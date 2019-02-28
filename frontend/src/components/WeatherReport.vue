<template>
  <section>
    <DayTabs :weekdays="weekdayList" v-model="selectedDay" />
    <ReportPanels :report="preparedReport" :selected="selectedDay" />
  </section>
</template>

<script>
import DayTabs from '@/components/DayTabs'
import ReportPanels from '@/components/ReportPanels'
import { pick } from 'lodash-es'

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
    weekdayList () {
      return [
        this.report.current.day,
        ...this.report.forecast.map(entry => entry.day)
      ]
    },
    preparedReport () {
      return [
        pick(this.report.current, 'temp', 'humidity', 'windspeed'),
        ...this.report.forecast.map(entry =>
          pick(entry, 'temp', 'humidity', 'windspeed')
        )
      ]
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
  flex-grow: 1;
  display: flex;
  flex-direction: column;
}
</style>
