<template>
  <div>
    <ReportPanel
      v-for="(entry, idx) in shownEntries"
      :weatherOfDay="entry"
      :key="idx"
    />
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
  },
  computed: {
    shownEntries () {
      return this.report.filter((entry, idx) => idx === this.selected)
    }
  }
}
</script>

<style>

</style>
