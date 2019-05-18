<template functional>
  <div>
    <transition>
      <span v-if="props.errorMsg">
        {{ props.errorMsg }}
      </span>
    </transition>
    &nbsp;
  </div>
</template>

<script>
export default {
  props: {
    errorMsg: {
      default: ''
    }
  }
}
</script>

<style lang="scss" scoped>
div {
  margin-top: 0.375rem;
  margin-left: $search-wrap-pad-x;
  color: red;

  .v-enter-active, .v-leave-active {
    transition: opacity $error-transition-duration;
  }
  .v-enter, .v-leave-to {
    opacity: 0;
  }
}
</style>
