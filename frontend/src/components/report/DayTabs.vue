<template>
  <ol v-if="$mq !== 'desktop'">
    <DayTab
      v-for="(dayName, idx) in weekdays"
      :key="dayName"
      :label="dayName"
      :selected="selected === idx"
      @click="$emit('select', idx)"
      @keydown.enter="$emit('select', idx)"
    />
  </ol>
</template>

<script>
import DayTab from './DayTab'

export default {
  components: {
    DayTab
  },
  model: {
    prop: 'selected',
    event: 'select'
  },
  props: {
    weekdays: {
      type: Array,
      required: true
    },
    selected: {
      type: Number,
      required: true,
      validator: val => val >= 0 && val <= 6
    }
  }
}
</script>

<style lang="scss" scoped>
ol {
  list-style-type: none;
  display: flex;
  justify-content: space-between;
  margin: 0.5rem 0;
  padding: 0;
  font-size: 1.5rem;
  font-weight: bold;
}
</style>
