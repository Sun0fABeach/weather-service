<template>
  <ol v-if="$mq !== 'desktop'">
    <DayTab
      v-for="(dayName, idx) in weekdays"
      :key="dayName"
      :label="dayName"
      :selected="idx === selected"
      @select="$emit('select', idx)"
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
  margin: 0 0 0.5rem;
  padding: 0;
  font-size: 1.5rem;
  font-weight: bold;

  @media screen and (min-width: $min-tablet) {
    margin-top: 1rem;
  }
}
</style>
