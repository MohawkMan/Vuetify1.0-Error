import Vue from 'vue'
import Vuex from 'vuex'

import Shared from './Shared'
import User from './User'

Vue.use(Vuex)

export const store = new Vuex.Store({
  modules: {
    Shared,
    User
  }
})
