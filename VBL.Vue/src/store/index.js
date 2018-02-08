import Vue from 'vue'
import Vuex from 'vuex'

import Shared from './Shared'
import User from './User'
import Tournament from './Tournament'
import Cart from './Cart'

import createPersistedState from 'vuex-persistedstate'

Vue.use(Vuex)

export const store = new Vuex.Store({
  modules: {
    Shared,
    User,
    Tournament,
    Cart
  },
  plugins: [createPersistedState()]
})
