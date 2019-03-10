<template>
  <portal to="history">
    <transition name="overlay">
      <div class="modal-container" v-if="open" @click.self="dismiss">
        <div>
          <HistoryButtonBar
            @clear="$emit('clear')"
            @dismiss="dismiss"
            :hideClearBtn="reports.length === 0"
          />
          <HistoryList
            :reports="reports"
            @selected="report => $emit('selected', report)"
          />
        </div>
      </div>
    </transition>
  </portal>
</template>

<script>
import HistoryButtonBar from './HistoryButtonBar'
import HistoryList from './HistoryList'

export default {
  components: {
    HistoryButtonBar,
    HistoryList,
  },
  model: {
    prop: 'open',
    event: 'toggleOpen'
  },
  props: {
    open: {
      type: Boolean,
      required: true
    },
    reports: {
      type: Array,
      required: true
    }
  },
  methods: {
    dismiss () {
      this.$emit('toggleOpen', false)
    }
  },
  created () {
    const escapeHandler = event => {
      if (event.key === 'Escape' && this.open) {
        this.dismiss()
      }
    }
    document.addEventListener('keydown', escapeHandler)
    this.$once('hook:destroyed', () =>
      document.removeEventListener('keydown', escapeHandler)
    )
  }
}
</script>

<style lang="scss" scoped>
.modal-container {
  position: fixed;
  top: 0;
  right: 0;
  bottom: 0;
  left: 0;
  background-color: rgba(0, 0, 0, 0.5);
  display: flex;
  justify-content: center;
  align-items: center;

  &.overlay-enter-active, &.overlay-leave-active {
    transition: background-color 100ms;
    > div {
      transition: transform 100ms;
    }
  }
  &.overlay-enter, &.overlay-leave-to {
    background-color: initial;
  }
  &.overlay-enter > div {
    transform: translateY(-20%);
  }
  &.overlay-leave-to > div {
    transform: translateY(20%);
  }

  > div {
    background-color: $color-background;
    padding: 1rem;
    border: 1px solid transparent;
    border-radius: 1rem;
    max-width: 97%;
    max-height: 99%;
    overflow: auto;
  }
}
</style>
