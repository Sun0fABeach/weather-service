<template>
  <div>
    <button type="button" @click="$emit('clear')">
      Verlauf löschen
    </button>
    <button type="button" @click="$emit('dismiss')">
      <FontAwesomeIcon icon="times" class="close-icon" />
    </button>
  </div>
</template>

<script>
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'

export default {
  components: {
    FontAwesomeIcon
  }
}
</script>

<style lang="scss" scoped>
div {
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
</style>
