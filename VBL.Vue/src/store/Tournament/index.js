import Vue from 'vue'
import * as mutations from '../MutationTypes'
import * as actions from '../ActionTypes'
import Tourney from '../../classes/Tournament'
import vbl from '../../VolleyballLife'

export default {
  state: {
    selectedTourney: null,
    tournamentList: null,
    tournamentListLoading: false
  },
  mutations: {
    [mutations.SET_SELECTED_TOURNAMENT] (state, payload) {
      state.selectedTourney = payload
    },
    [mutations.SET_TOURNAMENT_LIST] (state, payload) {
      state.tournamentList = payload
    },
    [mutations.SET_TOURNAMENT_LIST_LOADING] (state, payload) {
      state.tournamentListLoading = payload
    }
  },
  actions: {
    [actions.LOAD_TOURNAMENT_LIST] ({commit, dispatch, getters}, payload) {
      if (getters.tournamentListLoading) return

      console.log('loading list')
      commit(mutations.SET_TOURNAMENT_LIST_LOADING, true) // set loading = true
      let url = vbl.tournament.getByOrganizationUserName(payload)

      Vue.prototype.axios.get(url)
        .then(response => {
          console.log('loading list SUCCESS')
          commit(mutations.SET_TOURNAMENT_LIST, response.data.map(item => new Tourney(item)))
          commit(mutations.SET_TOURNAMENT_LIST_LOADING, false) // set loading = false
        })
        .catch(response => {
          console.log('loading list FAILED')
          console.log(response.data)
          commit(mutations.SET_TOURNAMENT_LIST_LOADING, false) // set loading = false
        })
    }
  },
  getters: {
    selectedTourney (state) {
      return state.selectedTourney
    },
    tournamentList (state) {
      return state.tournamentList
    },
    tournamentListLoading (state) {
      return state.tournamentListLoading
    }
  }
}
