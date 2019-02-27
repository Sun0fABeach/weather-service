import Vue from 'vue'
import App from './App.vue'

import { library as faLib } from '@fortawesome/fontawesome-svg-core'
import { faSearch, faSpinner } from '@fortawesome/free-solid-svg-icons'

faLib.add(faSearch, faSpinner)

Vue.config.productionTip = false

new Vue({
  render: h => h(App)
}).$mount('#app')
