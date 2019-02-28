<template>
  <li @click="$emit('select')" :class="{ selected }">
    {{ label | shorten }}
  </li>
</template>

<script>
export default {
  props: {
    label: {
      type: String,
      required: true
    },
    selected: {
      type: Boolean,
      default: false
    }
  },
  filters: {
    shorten (label) {
      return label.substring(0, 2)
    }
  }
}
</script>

<style lang="scss" scoped>
li {
  --border-height: 2px;

  flex-grow: 1;
  text-align: center;
  padding: calc(0.25rem + var(--border-height)) 0 0.25rem;
  border-bottom: var(--border-height) solid transparent;
  cursor: pointer;
  transition: border-bottom-color 150ms;

  &.selected {
    border-bottom-color: black;
  }

  &:hover {
    border-bottom-color: grey;
  }
}
</style>
