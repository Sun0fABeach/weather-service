<template>
  <button type="button" v-on="$listeners">
    <FontAwesomeIcon
      :icon="requestPending ? 'spinner' : 'search'"
      :spin="requestPending"
      class="search-icon"
    />
  </button>
</template>

<script>
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'

export default {
  components: {
    FontAwesomeIcon
  },
  props: {
    requestPending: {
      type: Boolean,
      default: false
    }
  }
}
</script>

<style lang="scss" scoped>
button {
  padding: var(--search-btn-pad);
  border: none;
  background-color: transparent;
  font-size: 1.75rem;
  cursor: pointer;

  > .search-icon {
    color: $color-highlight;
    transition: transform 250ms;
  }

  &:hover > .search-icon {
    transform: scale(1.2);
  }

  &:focus {
    outline: none;
  }
}
</style>
