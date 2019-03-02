<template functional>
  <div>
    <span>{{ props.label }}</span>
    <span>{{ props.value }}</span>
  </div>
</template>

<script>
export default {
  props: {
    label: {
      type: String,
      required: true
    },
    value: {
      type: [String, Number],
      required: true
    }
  }
}
</script>

<style lang="scss" scoped>
div {
  display: flex;
  flex-direction: column;
  align-items: center;
  margin-top: 1rem;

  > span {
    text-align: center;

    &:first-of-type { // label
      font-size: 1.5rem;
    }
    &:last-of-type { // value
      font-size: 3rem;
      font-weight: bold;
    }

    @media screen and (min-width: $min-tablet) {
      &:first-of-type { // label
        font-size: 1.625rem;
      }
      &:last-of-type { // value
        font-size: 3.5rem;
      }
    }

    @media screen and (min-width: $min-desktop) {
      &:first-of-type { // label
        font-size: 1.25rem;
      }
      &:last-of-type { // value
        font-size: 2.5rem;
      }
    }
  }
}
</style>
