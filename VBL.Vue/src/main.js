import Vue from 'vue'

// Vuetify and individual components
import {
  Vuetify,
  VApp,
  VAvatar,
  VNavigationDrawer,
  VFooter,
  VList,
  VBtn,
  VIcon,
  VGrid,
  VToolbar,
  VCard,
  VForm,
  VTextField,
  VDialog,
  VMenu,
  VBadge,
  VDivider,
  VSubHeader,
  VProgressLinear,
  VProgressCircular,
  VSwitch,
  VCheckbox,
  VSpeedDial,
  VRadioGroup,
  VSelect,
  VDatePicker,
  VTimePicker,
  VDataTable,
  VToolTip,
  VParallax,
  VStepper,
  VCarousel,
  VAlert,
  VTabs,
  VExpansionPanel,
  VChip,
  transitions
} from 'vuetify'
import '../node_modules/vuetify/src/stylus/app.styl'
Vue.use(Vuetify, {
  components: {
    VApp,
    VAvatar,
    VNavigationDrawer,
    VFooter,
    VList,
    VBtn,
    VIcon,
    VGrid,
    VToolbar,
    VCard,
    VForm,
    VTextField,
    VDialog,
    VMenu,
    VBadge,
    VDivider,
    VSubHeader,
    VProgressLinear,
    VProgressCircular,
    VSwitch,
    VCheckbox,
    VSpeedDial,
    VRadioGroup,
    VSelect,
    VDatePicker,
    VTimePicker,
    VDataTable,
    VToolTip,
    VParallax,
    VStepper,
    VCarousel,
    VAlert,
    VTabs,
    VExpansionPanel,
    VChip,
    transitions
  },
  theme: {
    primary: '#152a69',
    secondary: '#446da3',
    accent: '#189ebb',
    error: '#FF5252',
    info: '#2196F3',
    success: '#4CAF50',
    warning: '#FFC107',
    color1: '#152a69',
    color2: '#446da3',
    color3: '#189ebb',
    color4: '#92becb',
    color5: '#e1dad0',
    color6: '#fffced',
    avpYellow1: '#fcd538',
    avpYellow2: '#ffd400'
  }
})

// Validation
import Vuelidate from 'vuelidate'
Vue.use(Vuelidate)

import vbl from './VolleyballLife'

// Http/Ajax
import VueAxios from 'vue-axios'
import axios from 'axios'
Vue.use(VueAxios, axios)
axios.defaults.baseURL = vbl.baseURL

// Auth
import VueAuthenticate from 'vue-authenticate'
Vue.use(VueAuthenticate, {
  baseUrl: vbl.baseURL,
  loginUrl: vbl.user.login,
  signupUrl: vbl.user.register,

  providers: {
    facebook: {
      clientId: '710675772455419',
      redirectUri: 'https://localhost:44351/api/v1.0/Auth/Callback'
    }
  }
})

import App from './App'
import router from './router'
import { store } from './store'
// import * as aTypes from './store/ActionTypes'

import VueAnalytics from 'vue-analytics'
let gaId = process.env.NODE_ENV === 'production' ? 'UA-114195278-1' : 'UA-114195278-2'
Vue.use(VueAnalytics, {
  id: gaId,
  router
})

Vue.config.productionTip = false

import { usDollars, usPhone, ordinal } from './Filters'
Vue.filter('usDollars', usDollars)
Vue.filter('usPhone', usPhone)
Vue.filter('ordinal', ordinal)

import PageTitle from './components/Utils/PageTitle.vue'
Vue.component('page-title', PageTitle)
import Loading from './components/Utils/Loading.vue'
Vue.component('loading-data', Loading)

/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  store,
  render: h => h(App)
})
