<template>
  <ol>
    <DayTab
      v-for="(dayName, idx) in weekdays"
      :label="dayName"
      :key="dayName"
      :selected="selected === idx"
      @select="$emit('select', idx)"
    />
  </ol>
</template>

<script>
import DayTab from './DayTab'

// eslint-disable-next-line
const dayList = [
  'Sonntag', 'Montag', 'Dienstag', 'Mittwoch',
  'Donnerstag', 'Freitag', 'Samstag'
]

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
      required: true,
      validator: list => list.length === 6
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
