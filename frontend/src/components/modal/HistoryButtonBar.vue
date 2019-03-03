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
  margin-bottom: 1.25rem;

  > button {
    background-color: $color-background;;
    cursor: pointer;

    &.clear {
      padding: 0.375rem 0.75rem;
      color: lighten($color-secondary, 20%);
      border: 1px solid $color-secondary;
      border-radius: 0.5rem;

      &:hover {
        color: $color-highlight;
      }
    }

    &.close {
      margin-left: auto;
      border: none;
      font-size: 1.5rem;
      transition: transform 0.25s;

      &:hover {
        transform: scale(1.2);
        color: $color-highlight;
      }
    }
  }
}
</style>
