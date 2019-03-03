import { library as faLib } from '@fortawesome/fontawesome-svg-core'
import {
  faSearch, faSpinner, faTimes, faHistory
} from '@fortawesome/free-solid-svg-icons'

import TWEEN from '@tweenjs/tween.js'

import VuePaginate from 'vue-paginate'
import PortalVue from 'portal-vue'
import VueMq from 'vue-mq'
import Vue from 'vue'

import App from './App.vue'

import '@/scss/base.scss'


faLib.add(faSearch, faSpinner, faTimes, faHistory)

function animate (time) {
  requestAnimationFrame(animate)
  TWEEN.update(time)
}
requestAnimationFrame(animate)

Vue.use(VuePaginate)
Vue.use(PortalVue)
Vue.use(VueMq, {
  breakpoints: { // defined as max values, exclusive
    phone: 900,
    tablet: 1400,
    desktop: Infinity
  }
})

Vue.config.productionTip = false


new Vue({
  render: h => h(App)
}).$mount('#app')
