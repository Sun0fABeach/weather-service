<script>
import axios from 'axios'
import { pick } from 'lodash-es'

const urlBase = 'http://localhost:5000/api/'

export default {
  props: {
    url: {
      required: true,
      type: String
    }
  },
  data: () => ({
    loading: false,
    json: null,
    error: null
  }),
  methods: {
    clear () {
      this.loading = true
      this.json = null
      this.error = null
    }
  },
  render (createElement) {
    return createElement('div', [
      this.$scopedSlots.default({

        fetch: async (urlExtension = null) => {
          this.clear()
          let route = `${urlBase}${this.url}`
          if (urlExtension) {
            route = `${route}${urlExtension}`
          }
          try {
            const response = await axios.get(route)
            this.json = response.data
            return { success: true, json: response.data }
          } catch (error) {
            this.error = error
            return { success: false, error }
          } finally {
            this.loading = false
          }
        },

        clearError: () => { this.error = null },

        ...pick(this, ['loading', 'json', 'error']),
      })
    ])
  }
}
</script>
