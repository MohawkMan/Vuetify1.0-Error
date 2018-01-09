import Vue from 'vue'
import * as mutations from '../MutationTypes'
import * as actions from '../ActionTypes'
import vbl from '../../VolleyballLife'
import User from '../../classes/User'

export default {
  state: {
    user: null
  },
  mutations: {
    [mutations.SET_USER] (state, payload) {
      state.user = new User(payload)
    }
  },
  actions: {
    [actions.LOGIN] ({commit, dispatch}, payload) {
      return new Promise((resolve, reject) => {
        commit(mutations.SET_LOADING, true) // set loading = true
        let auth = Vue.prototype.$auth
        if (auth.isAuthenticated()) {
          // already logged in, load the user
          dispatch(actions.LOAD_USER)
          resolve()
        } else {
          // need to login
          auth.login(payload.user)
          .then(() => {
            // Success > load user
            dispatch(actions.LOAD_USER)
            resolve()
          })
          .catch((response) => {
            // Error
            console.log('Error logging in user')
            console.log(response)
            commit(mutations.SET_LOADING, false)
            reject()
          })
        }
      })
    },
    [actions.LOGOUT] ({commit, dispatch}) {
      return new Promise((resolve, reject) => {
        commit(mutations.SET_LOADING, true) // set loading = true

        let auth = Vue.prototype.$auth
        if (!auth.isAuthenticated()) {
          // there is no user logged in
          commit(mutations.SET_USER, null) // clear user
          commit(mutations.SET_LOADING, false) // set loading = false
          resolve()
        }

        auth.logout().then(() => {
          commit(mutations.SET_USER, null) // clear user
          commit(mutations.SET_NAV, 'public')
          commit(mutations.SET_LOADING, false) // set loading = false
          resolve()
        }).catch((error) => {
          console.log(error)
          reject()
        })
      })
    },
    [actions.LOAD_USER] ({commit, state}) {
      commit(mutations.SET_LOADING, true) // set loading = true
      commit(mutations.SET_ERROR, null)
      Vue.prototype.axios.get(vbl.user.getCurrent)
      .then((response) => {
        commit(mutations.SET_USER, response.data)
        commit(mutations.SET_NAV, 'private')
        commit(mutations.SET_LOADING, false)
      })
      .catch((response) => {
        // Error
        console.log('Error loading user')
        console.log(response)
        commit(mutations.SET_ERROR, response.message)
        commit(mutations.SET_LOADING, false)
      })
    }
  },
  getters: {
    user (state) {
      return state.user
    },
    getPageInfo: (state) => (username) => {
      return state.user && state.user.pages && state.user.pages.find(page => {
        return page.username === username
      })
    }
  }
}
