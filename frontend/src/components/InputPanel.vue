<template>
  <section>
    <a href="#" @click.stop.prevent="historyOpen = !historyOpen">
      Suchverlauf
    </a>
    <Search @result="gotReport" />
    <HistoryModal
      v-model="historyOpen"
      :reports="reportsHistory"
      @selected="report => gotReport(report, true)"
      @clear="clearHistory"
    />
  </section>
</template>

<script>
import Search from '@/components/Search'
import HistoryModal from '@/components/HistoryModal'
import HistoryDB from '@/history'

export default {
  components: {
    Search,
    HistoryModal
  },
  data () {
    return {
      historyOpen: false,
      reportsHistory: []
    }
  },
  methods: {
    gotReport (report, fromHistory = false) {
      if (!fromHistory) {
        this.historyDB.add(report)
      }
      this.$emit('report', report)
    },
    clearHistory () {
      this.historyDB.clear()
      this.reportsHistory = []
    }
  },
  created () {
    this.historyDB = new HistoryDB()
  },
  watch: {
    async historyOpen (opens) {
      if (opens) {
        this.reportsHistory = await this.historyDB.retrieve()
      }
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
