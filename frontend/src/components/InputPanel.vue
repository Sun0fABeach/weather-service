<template>
  <section>
    <a href="#" @click.stop.prevent="historyOpen = !historyOpen">
      Suchverlauf
    </a>
    <Search @result="gotReport" />
    <HistoryModal
      v-model="historyOpen"
      @selected="report => gotReport(report, true)"
    />
  </section>
</template>

<script>
import Search from '@/components/Search'
import HistoryModal from '@/components/HistoryModal'

export default {
  components: {
    Search,
    HistoryModal
  },
  data () {
    return {
      historyOpen: false
    }
  },
  methods: {
    gotReport (report, fromHistory = false) {
      if (!fromHistory) {
        report.date = new Date()
        // TODO: save to indexeddb
      }
      this.$emit('report', report)
    }
  }
}
</script>

<style lang="scss" scoped>
a {
  display: block;
  margin: 0.5rem 0 1.5rem;
  text-align: center;
  font-size: 1.25rem;

  &:hover {
    text-decoration: none;
  }
}
</style>
