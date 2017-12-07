import Vue from 'vue'
import {store} from '../store'
import * as actions from '../store/ActionTypes'

export default (to, from, next) => {
  console.log('beforeEach')
  if (Vue.prototype.$auth.isAuthenticated() && !store.getters.user) {
    console.log('beforeEach loading user')
    store.dispatch(actions.LOAD_USER)
  }
  next()
}
