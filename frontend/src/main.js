import Vue from 'vue'
import { library as faLib } from '@fortawesome/fontawesome-svg-core'
import { faSearch, faSpinner, faTimes } from '@fortawesome/free-solid-svg-icons'
import TWEEN from '@tweenjs/tween.js'
import PortalVue from 'portal-vue'
import VuePaginate from 'vue-paginate'
import App from './App.vue'


faLib.add(faSearch, faSpinner, faTimes)

function animate (time) {
  requestAnimationFrame(animate)
  TWEEN.update(time)
}
requestAnimationFrame(animate)

Vue.use(PortalVue)
Vue.use(VuePaginate)
Vue.config.productionTip = false


new Vue({
  render: h => h(App)
}).$mount('#app')
