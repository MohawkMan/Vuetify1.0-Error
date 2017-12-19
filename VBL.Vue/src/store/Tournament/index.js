import * as mutations from '../MutationTypes'

export default {
  state: {
    selectedTourney: null
  },
  mutations: {
    [mutations.SET_SELECTED_TOURNAMENT] (state, payload) {
      state.selectedTourney = payload
    }
  },
  actions: {
  },
  getters: {
    selectedTourney (state) {
      return state.selectedTourney
    }
  }
}
