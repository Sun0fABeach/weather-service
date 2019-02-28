<template>
  <div>
    <template v-for="(entry, idx) in report">
      <ReportPanel
        :displayed="idx === selected"
        :forToday="idx === 0"
        :weather="entry"
        :key="idx"
      />
    </template>
  </div>
</template>

<script>
import ReportPanel from '@/components/ReportPanel'
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
        !has(entry, 'temp', 'humidity', 'windspeed')
      )
    },
    selected: {
      type: Number,
      required: true
    },
  }
}
</script>

<style lang="scss" scoped>
</style>
