<template>
  <input
    type="text"
    placeholder="Suche nach Ort oder PLZ"
    v-bind="$attrs"
    @input="$emit('input', $event.target.value)"
    @keydown.enter="$emit('submit')"
    ref="input"
  >
</template>

<script>
export default {
  inheritAttrs: false,
  mounted () {
    this.$refs.input.focus()
  }
}
</script>

<style lang="scss" scoped>
input {
  border: none;
  background-color: transparent;
  font-size: 1.25rem;
  line-height: 2.5;
  flex-grow: 1;
  min-width: 0;

  &:focus {
    outline: none;
  }
}
</style>
