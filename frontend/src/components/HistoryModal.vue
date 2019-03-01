<template>
  <portal to="history">
    <transition name="overlay">
      <div class="modal-container" v-if="open" @click.self="dismiss">
        <div>

          <div class="button-bar">
            <button type="button" @click="$emit('clear')">
              Verlauf löschen
            </button>
            <button type="button" @click="dismiss">
              <FontAwesomeIcon icon="times" class="close-icon" />
            </button>
          </div>

          <HistoryTable
            v-if="reports.length > 0"
            :reports="reports"
            @selected="report => $emit('selected', report)"
          />
          <div v-else class="placeholder">Keine Daten vorhanden</div>
        </div>

      </div>
    </transition>
  </portal>
</template>

<script>
import HistoryTable from '@/components/HistoryTable'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'

export default {
  components: {
    HistoryTable,
    FontAwesomeIcon
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
    display: flex;
    flex-direction: column;
    background-color: white;
    padding: 1rem;
    border: 1px solid transparent;
    border-radius: 1rem;
    min-width: 20%;
  }

  .button-bar {
    display: flex;
    justify-content: space-between;
    margin-bottom: 1rem;

    > button {
      background-color: white;
      cursor: pointer;

      &:first-of-type {
        padding: 0.375rem 0.75rem;
        border: 1px solid grey;
        border-radius: 0.5rem;
        &:hover {
          border-color: black;
          color: black;
        }
      }

      &:last-of-type {
        border: none;
        font-size: 1.5rem;
        transition: transform 0.25s;

        &:hover {
          transform: scale(1.2);
          color: black;
        }
      }
    }
  }

  .placeholder {
    text-align: center;
  }
}
</style>
