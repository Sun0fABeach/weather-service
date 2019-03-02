<template>
  <section>
    <a href="#" @click.stop.prevent="historyOpen = !historyOpen">
      Suchverlauf
    </a>
    <Search @result="gotReport" :inputVal="searchInput" class="search" />
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
section {
  display: flex;
  flex-direction: column;

  > a {
    display: block;
    margin: 0.75rem 0 1rem;
    text-align: center;
    font-size: 1.375rem;
    color: black;

    &:hover {
      text-decoration: none;
    }
  }
}

@media screen and (min-width: 900px) {
  section {
    flex-direction: row;
    justify-content: center;
    align-items: flex-start;
    margin-top: 1rem;

    > .search {
      order: 0;
    }
    > a {
      order: 1;
      margin: 0.875rem 0 0 1.5rem;
    }
  }
}

</style>
