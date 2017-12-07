import Vue from 'vue'
import * as mutations from '../MutationTypes'
import * as actions from '../ActionTypes'
import vbl from '../../VolleyballLife'
import selectOptions from './SelectOptions.json'

export default {
  state: {
    loading: false,
    error: null,
    nav: 'public',
    ageTypeOptions: selectOptions.ageTypeOptions,
    genderOptions: selectOptions.genderOptions,
    divisionOptions: selectOptions.divisionOptions,
    locationOptions: selectOptions.locationOptions,
    optionsLoaded: false
  },
  mutations: {
    [mutations.SET_LOADING] (state, payload) {
      state.loading = payload
    },
    [mutations.SET_ERROR] (state, payload) {
      state.error = payload
    },
    [mutations.SET_NAV] (state, payload) {
      state.nav = payload
    },
    [mutations.SET_AGETYPEOPTIONS] (state, payload) {
      state.ageTypeOptions = payload
    },
    [mutations.SET_GENDEROPTIONS] (state, payload) {
      state.genderOptions = payload
    },
    [mutations.SET_DIVISIONOPTIONS] (state, payload) {
      state.divisionOptions = payload
    },
    [mutations.SET_LOCATIONOPTIONS] (state, payload) {
      state.locationOptions = payload
    },
    [mutations.SET_OPTIONS_LOADED] (state, payload) {
      state.optionsLoaded = payload
    }
  },
  actions: {
    [actions.LOAD_SELECT_OPTIONS] ({commit, dispatch, state}) {
      return new Promise((resolve, reject) => {
        console.log('load options called')
        if (!state.optionsLoaded) {
          console.log('loading options')
          commit(mutations.SET_LOADING, true) // set loading = true
          Vue.prototype.axios.get(vbl.tournament.getSelectOptions())
          .then((response) => {
            commit(mutations.SET_AGETYPEOPTIONS, response.data.ageTypeOptions)
            commit(mutations.SET_GENDEROPTIONS, response.data.genderOptions)
            commit(mutations.SET_DIVISIONOPTIONS, response.data.divisionOptions)
            commit(mutations.SET_LOCATIONOPTIONS, response.data.locationOptions)
            commit(mutations.SET_OPTIONS_LOADED, true)
            commit(mutations.SET_LOADING, false) // set loading = false
            resolve()
          })
          .catch((response) => {
            // Error
            console.log(response.data)
            commit(mutations.SET_LOADING, false) // set loading = false
            reject()
          })
        }
        resolve()
      })
    }
  },
  getters: {
    loading (state) {
      return state.loading
    },
    error (state) {
      return state.error
    },
    nav (state) {
      return state.nav
    },
    ageTypeOptions (state) {
      return state.ageTypeOptions
    },
    genderOptions (state) {
      return state.genderOptions
    },
    divisionOptions (state) {
      return state.divisionOptions
    },
    locationOptions (state) {
      return state.locationOptions
    }
  }
}
