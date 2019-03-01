<template>
  <ol v-if="reports.length > 0">
    <li
      v-for="report in sortedReports"
      :key="report.date.getTime()"
      @click="$emit('selected', report)"
    >
      <span>{{ report.date | formatDate }}</span>
      <span>{{ report.query }}</span>
    </li>
  </ol>
  <div v-else>
    Keine Daten vorhanden
  </div>
</template>

<script>
import { reverse } from 'lodash-es'

export default {
  props: {
    reports: {
      type: Array,
      required: true
    }
  },
  computed: {
    sortedReports () {
      return reverse(this.reports)
    }
  },
  filters: {
    formatDate (date) {
      return date.toLocaleString('de-DE', {
        hour: 'numeric',
        minute: 'numeric',
        weekday: 'short',
        day: 'numeric',
        month: 'short',
        year: 'numeric'
      })
    }
  }
}
</script>

<style lang="scss" scoped>
ol {
  list-style-type: none;
  margin: 0;
  padding: 0;
  display: flex;
  flex-direction: column;

  li {
    display: flex;
    flex-direction: column;
    padding: 0.5rem;
    border: 1px solid transparent;
    cursor: pointer;

    &:hover {
      border-color: grey;
    }

    > span {
      &:first-of-type {
        color: grey;
      }

      &:last-of-type {
        font-size: 1.25rem;
      }
    }
  }
}

div { // placeholder
  text-align: center;
}
</style>
