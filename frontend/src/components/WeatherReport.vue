<template>
  <section>
    <DayTabs
      :weekdays="weekdayList"
      v-model="selectedDay"
    />
    <ReportPanels />
    <div>{{ selectedDay }}</div>
    <div>{{ report }}</div>
  </section>
</template>

<script>
import DayTabs from '@/components/DayTabs'
import ReportPanels from '@/components/ReportPanels'

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

</style>
