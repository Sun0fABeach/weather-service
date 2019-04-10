<template functional>
  <li
    v-bind="data.attrs"
    :title="parent.$mq === 'phone' ? props.label : null"
    :class="{ selected: props.selected }"
    tabindex="0"
    @click="listeners.select"
    @keydown.enter="listeners.select"
  >
    {{ parent.$mq === 'phone' ? props.label.substring(0, 2) : props.label }}
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
  }
}
</script>

<style lang="scss" scoped>

$border-height: 0.125rem;

li {
  flex-grow: 1;
  text-align: center;
  padding: 0.25rem + $border-height 0 0.25rem;
  border-bottom: $border-height solid transparent;
  cursor: pointer;
  transition: border-bottom-color 150ms;

  &.selected {
    border-bottom-color: lighten($color-secondary, 50%);
  }

  &:hover {
    border-bottom-color: $color-highlight;
  }

  &:focus {
    outline: none;
  }
}
</style>
