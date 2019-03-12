<template>
  <div v-if="reports.length > 0" class="history-list">
    <paginate
      tag="ol"
      name="reports"
      :list="sortedReports"
      :per="reportsPerPage"
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

    <div class="paginate-links" v-if="$refs.paginator">
      <span>
        {{ $refs.paginator.pageItemsCount | translatePageItemsCount }}
      </span>
      <div>
        <button @click="gotoPage('first')" :disabled="onFirstPage">
          <span>&lt;&lt;</span>
        </button>
        <button @click="gotoPage('prev')" :disabled="onFirstPage">
          <span>&lt;</span>
        </button>
        <button @click="gotoPage('next')" :disabled="onLastPage">
          <span>&gt;</span>
        </button>
        <button @click="gotoPage('last')" :disabled="onLastPage">
          <span>&gt;&gt;</span>
        </button>
      </div>
    </div>
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
      paginate: ['reports']
    }
  },
  computed: {
    sortedReports () {
      return reverse(this.reports)
    },
    reportsPerPage () {
      switch (this.$mq) {
        case 'desktop':
          return 8
        case 'tablet':
          return 6
        default:
          return 4
      }
    },
    onFirstPage () {
      return this.$refs.paginator.currentPage === 0
    },
    onLastPage () {
      const paginator = this.$refs.paginator
      return paginator.currentPage === paginator.lastPage - 1
    }
  },
  methods: {
    gotoPage (which) {
      const paginator = this.$refs.paginator
      let pageNum // index starts at 1 !

      switch (which) {
        case 'first':
          pageNum = 1
          break
        case 'prev':
          pageNum = paginator.currentPage // currentPage starts at 0
          break
        case 'next':
          pageNum = paginator.currentPage + 2
          break
        case 'last':
          // lastPage starts at 0, value is actual last page +1
          pageNum = paginator.lastPage
          break
      }

      paginator.goToPage(pageNum)
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
    },
    translatePageItemsCount (str) {
      return str.replace('of', 'von')
    }
  }
}
</script>

<style lang="scss" scoped>
.history-list {
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
      border-radius: 0.25rem;
      cursor: pointer;

      &:hover {
        border-color: lighten($color-secondary, 20%);
      }

      > span {
        &:first-of-type {
          color: $color-secondary;
        }

        &:last-of-type {
          font-size: 1.25rem;
        }
      }
    }
  }

  .paginate-links {
    > span {
      display: block;
      margin-top: 0.75rem;
      text-align: center;
    }

    > div {
      display: flex;
      justify-content: space-evenly;

      > button {
        color: $color-primary;
        background-color: $color-background;
        border: none;
        padding: 0.5rem;
        font-size: 1.5rem;

        &[disabled] {
          opacity: 0.5;
        }

        &:not([disabled]):hover {
          cursor: pointer;

          > span {
            color: $color-highlight;
            transform: scale(1.2);
          }
        }

        > span {
          display: inline-block;
          transition: transform 0.25s;
        }
      }
    }
  }
}

</style>
