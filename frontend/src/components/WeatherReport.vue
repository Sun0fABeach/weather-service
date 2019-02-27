<template>
  <div>
    <DayTabs />
    <ReportPanels />
    {{ forecast }}
  </div>
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
    forecast: {
      type: Object
    }
  }
}
</script>

<style lang="scss" scoped>

</style>
