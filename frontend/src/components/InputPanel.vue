<template>
  <div>
    <Search @result="report => this.$emit('forecast', report)" />
    <p>Suchverlauf</p>
  </div>
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

</style>
