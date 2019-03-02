<template>
  <div>
    <button
      v-if="!hideClearBtn"
      type="button"
      @click="$emit('clear')"
      class="clear"
    >
      Verlauf löschen
    </button>

    <button
      type="button"
      @click="$emit('dismiss')"
      class="close"
    >
      <FontAwesomeIcon icon="times" class="close-icon" />
    </button>
  </div>
</template>

<script>
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'

export default {
  components: {
    FontAwesomeIcon
  },
  props: {
    hideClearBtn: {
      type: Boolean,
      default: false
    }
  }
}
</script>

<style lang="scss" scoped>
div {
  display: flex;
  margin-bottom: 1rem;

  > button {
    background-color: white;
    cursor: pointer;

    &.clear {
      padding: 0.375rem 0.75rem;
      border: 1px solid grey;
      border-radius: 0.5rem;

      &:hover {
        border-color: black;
        color: black;
      }
    }

    &.close {
      margin-left: auto;
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
</style>
