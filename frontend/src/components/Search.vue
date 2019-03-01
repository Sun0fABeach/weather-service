<template>
  <div>
    <div class="input-wrap" :style="errorShadow">
      <input
        type="text"
        placeholder="Suche nach Ort oder PLZ"
        v-model.trim="query"
        @keydown.enter="requestWeather"
        @input="error = false"
        ref="input"
      >
      <button type="button" @click="requestWeather">
        <FontAwesomeIcon
          :icon="requestPending ? 'spinner' : 'search'"
          :spin="requestPending"
          class="search-icon"
        />
      </button>
    </div>
    <div class="search-error">
      <transition>
        <span v-if="error">
          {{ error }}
        </span>
      </transition>
      &nbsp;
    </div>
  </div>
</template>


<script>
import axios from 'axios'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'
import { capitalize } from 'lodash-es'

export default {
  components: {
    FontAwesomeIcon
  },
  props: {
    inputVal: {
      type: String
    }
  },
  data () {
    return {
      query: '',
      error: '',
      requestPending: false
    }
  },
  computed: {
    errorShadow () {
      return {
        boxShadow: this.error ? '0 2px 2px 0 red' : null
      }
    }
  },
  methods: {
    async requestWeather () {
      if (!this.query) {
        return
      }

      try {
        this.error = ''
        this.requestPending = true

        const response = await axios.get(this.buildRequestUrl())
        response.data.date = new Date()
        response.data.query = capitalize(this.query)
        this.$emit('result', response.data)

      } catch (error) {
        this.error = error.response.status === 404
          ? 'Ort / PLZ unbekannt.' : 'Ein Fehler ist aufgetreten.'

      } finally {
        this.requestPending = false
      }
    },

    buildRequestUrl () {
      const key = this.query.match(/\d{5}/) ? 'zipCode' : 'city'
      return `https://localhost:5001/api/weather/forecast?${key}=${this.query}`
    }
  },
  watch: {
    inputVal (str) {
      this.query = str
    }
  },
  mounted () {
    this.$refs.input.focus()
  }
}
</script>


<style lang="scss" scoped>
div {
  --search-wrap-pad-x: 1.25rem;
  --search-btn-pad: 0.375rem;
  --error-transition-duration: 200ms;
}

.input-wrap {
  display: flex;
  align-items: center;
  padding-right: calc(var(--search-wrap-pad-x) - var(--search-btn-pad));
  padding-left: var(--search-wrap-pad-x);
  background-color: white;
  border: 1px solid transparent;
  border-radius: 2rem;
  transition: box-shadow var(--error-transition-duration);

  > input {
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

  > button {
    padding: var(--search-btn-pad);
    cursor: pointer;
    border: none;
    background-color: transparent;
    font-size: 1.75rem;

    > .search-icon {
      color: #00489a;
      transition: transform 250ms;
    }

    &:hover > .search-icon {
      transform: scale(1.2, 1.2);
    }
  }
}

.search-error {
  margin-top: 0.375rem;
  margin-left: var(--search-wrap-pad-x);
  color: red;
}

.v-enter-active, .v-leave-active {
  transition: opacity var(--error-transition-duration);
}
.v-enter, .v-leave-to {
  opacity: 0;
}
</style>
