<template>
  <div>
    {{ weatherOfDay }}
  </div>
</template>

<script>
export default {
  props: {
    weatherOfDay: {
      type: Object,
      required: true
    }
  }
}
</script>

<style>

</style>
