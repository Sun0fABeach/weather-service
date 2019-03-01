<template>
  <div v-if="reports.length > 0">
    <paginate
      tag="ol"
      name="reports"
      :list="sortedReports"
      :per="5"
      :style="{ height: fixedListHeight }"
      ref="paginator"
    >
      <li
        v-for="report in paginated('reports')"
        :key="report.date.getTime()"
        @click="$emit('selected', report)"
      >
        <span>{{ report.date | formatDate }}</span>
        <span>{{ report.query }}</span>
      </li>
    </paginate>
    <paginate-links
      for="reports"
      :limit="2"
      :show-step-links="true"
      :hide-single-page="true"
      @mousedown.native="fixListHeight"
    />
  </div>
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
  data () {
    return {
      paginate: ['reports'],
      fixedListHeight: null
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
  },
  methods: {
    fixListHeight () {
      /* to prevent list height from shrinking
         when navigating to a page with fewer items */
      this.fixedListHeight = this.$refs.paginator.$el.offsetHeight + 'px'
    }
  }
}
</script>

<style lang="scss" scoped>
ol {
  list-style-type: none;
  margin: 0;
  padding: 0 1rem;
  display: flex;
  flex-direction: column;
  text-align: center;

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
</style>

<style lang="scss"> // pagination links created dynamically, can't use scoped
ul.paginate-links {
  list-style-type: none;
  padding: 0;
  margin: 0.5rem 0 0;
  display: flex;
  justify-content: center;
  font-size: 1.25rem;

  > li {
    border-bottom: 1px solid transparent;
    cursor: default;

    &.left-arrow, &.right-arrow {
      font-weight: bold;
      font-size: 1.25rem;
      line-height: 1.3; // centers it in relation to adjacent numbers
    }

    &.active {
      border-bottom-color: black;
    }

    &.disabled {
      opacity: 0.5;
    }

    &:not(.disabled):hover {
      cursor: pointer;
      border-bottom-color: grey;
    }

    > a {
      display: inline-block;
      padding: 0.25rem 0.625rem 0;
    }
  }
}
</style>
