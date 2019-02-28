<template>
  <section>
    <p>Suchverlauf</p>
    <Search @result="report => this.$emit('report', report)" />
  </section>
</template>

<script>
import Search from '@/components/Search'

export default {
  components: {
    Search
  }
}
</script>

<style lang="scss" scoped>
p {
  text-align: center;
}
</style>
