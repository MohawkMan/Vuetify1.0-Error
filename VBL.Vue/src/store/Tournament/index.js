import Vue from 'vue'
import * as mutations from '../MutationTypes'
import * as actions from '../ActionTypes'
import Tourney from '../../classes/Tournament'
import vbl from '../../VolleyballLife'
import * as StatusEnum from '../../classes/TournamentStatus'

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
    },
    [mutations.UPDATE_TOURNAMENT] (state, payload) {
      if (payload.id === 0) return
      const i = state.tournamentList.indexOf((t) => {
        return t.id === payload.id
      })
      if (i === -1) {
        state.tournamentList.push(payload)
      } else {
        state.tournamentList[i] = payload
      }
    }
  },
  actions: {
    [actions.LOAD_TOURNAMENT_LIST] ({commit, dispatch, getters}, payload) {
      if (getters.tournamentListLoading) return

      console.log('loading list')
      commit(mutations.SET_TOURNAMENT_LIST_LOADING, true) // set loading = true
      let url = vbl.tournament.getAll

      Vue.prototype.axios.get(url)
        .then(response => {
          console.log('loading list SUCCESS')
          commit(mutations.SET_TOURNAMENT_LIST, response.data.map(item => new Tourney(item)))
          commit(mutations.SET_TOURNAMENT_LIST_LOADING, false) // set loading = false
        })
        .catch(response => {
          console.log('loading list FAILED')
          console.log(response)
          console.log(response.data)
          commit(mutations.SET_TOURNAMENT_LIST_LOADING, false) // set loading = false
        })
    }
  },
  getters: {
    getTournamentById: (state) => (id) => {
      return state.tournamentList.find(t => +t.id === +id)
    },
    selectedTourney: state => {
      return state.selectedTourney
    },
    tournamentList: state => {
      return state.tournamentList
    },
    tournamentListLoading: state => {
      return state.tournamentListLoading
    },
    publishedTournaments: state => {
      return state.tournamentList && state.tournamentList.filter(t => t.isPublic)
    },
    upcomingTournaments: (state) => (username) => {
      if (username) return state.tournamentList && state.tournamentList.filter(t => t.dateStatus === StatusEnum.UPCOMING && t.organization.username === username)

      return state.tournamentList && state.tournamentList.filter(t => t.dateStatus === StatusEnum.UPCOMING)
    },
    runningTournaments: (state) => (username) => {
      if (username) return state.tournamentList && state.tournamentList.filter(t => t.dateStatus === StatusEnum.INPROCESS && t.organization.username === username)

      return state.tournamentList && state.tournamentList.filter(t => t.dateStatus === StatusEnum.INPROCESS)
    },
    pastTournaments: (state) => (username) => {
      if (username) return state.tournamentList && state.tournamentList.filter(t => t.dateStatus === StatusEnum.PAST && t.organization.username === username)

      return state.tournamentList && state.tournamentList.filter(t => t.dateStatus === StatusEnum.PAST)
    },
    needResults: (state) => (username) => {
      if (username) {
        return state.tournamentList && state.tournamentList.filter(t =>
          t.organization.username === username &&
          t.dateStatus !== StatusEnum.UPCOMING &&
          t.statusId === StatusEnum.ACTIVE
        )
      }

      return state.tournamentList && state.tournamentList.filter(t =>
        t.dateStatus !== StatusEnum.UPCOMING &&
        t.statusId === StatusEnum.ACTIVE
      )
    }
  }
}
