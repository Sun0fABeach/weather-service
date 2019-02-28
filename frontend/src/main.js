import Vue from 'vue'
import App from './App.vue'

import { library as faLib } from '@fortawesome/fontawesome-svg-core'
import { faSearch, faSpinner } from '@fortawesome/free-solid-svg-icons'
import TWEEN from '@tweenjs/tween.js'

faLib.add(faSearch, faSpinner)

function animate (time) {
  requestAnimationFrame(animate)
  TWEEN.update(time)
}
requestAnimationFrame(animate)

Vue.config.productionTip = false

new Vue({
  render: h => h(App)
}).$mount('#app')
