<template>
  <section>
    <a href="#" @click.stop.prevent="historyOpen = !historyOpen">
      Suchverlauf
    </a>
    <Search @result="reportSelected" />
    <HistoryModal v-model="historyOpen" @selected="reportSelected" />
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
    reportSelected (report) {
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
