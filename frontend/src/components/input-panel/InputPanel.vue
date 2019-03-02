<template>
  <section>
    <a href="#" @click.stop.prevent="historyOpen = !historyOpen">
      Suchverlauf
    </a>
    <Search @result="gotReport" :inputVal="searchInput" />
    <HistoryModal
      v-model="historyOpen"
      :reports="reportsHistory"
      @selected="report => gotReport(report, true)"
      @clear="clearHistory"
    />
  </section>
</template>

<script>
import HistoryModal from '@/components/modal/HistoryModal'
import Search from './Search'
import Dexie from 'dexie'

export default {
  components: {
    HistoryModal,
    Search
  },

  data () {
    return {
      searchInput: '',
      historyOpen: false,
      reportsHistory: []
    }
  },

  methods: {
    async gotReport (report, fromHistory = false) {
      if (fromHistory) {
        this.historyOpen = false
        this.searchInput = report.query
      } else {
        await this.db.weatherReports.add(report)
      }
      this.$emit('report', report)
    },
    async clearHistory () {
      await this.db.weatherReports.clear()
      this.reportsHistory = []
    }
  },

  created () {
    this.db = new Dexie('searchHistory')
    this.db.version(1).stores({ weatherReports: '++' })
  },

  watch: {
    async historyOpen (opens) {
      if (opens) {
        this.reportsHistory = await this.db.weatherReports.toArray()
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
