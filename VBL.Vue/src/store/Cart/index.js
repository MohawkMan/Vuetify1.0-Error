import * as mutations from '../MutationTypes'
import max from 'lodash.max'
// import * as actions from '../ActionTypes'
// import CartItem from '../../classes/CartItem'

export default {
  state: {
    cart: {
      items: [],
      organization: null
    }
  },
  mutations: {
    [mutations.ADD_CART_ITEM] (state, item) {
      if (!state.cart.organization) state.cart.organization = item.organization

      if (state.cart.organization.id === item.organization.id) {
        let id = 1
        if (state.cart.items.length) {
          id = max(state.cart.items.map(item => item.id)) + 1
        }
        item.id = id
        state.cart.items.push(item)
      }
    },
    [mutations.REMOVE_CART_ITEM] (state, id) {
      const i = state.cart.items.findIndex(item => item.id === id)
      if (i !== -1) {
        state.cart.items.splice(i, 1)
      }
    },
    [mutations.CLEAR_CART] (state) {
      state.cart.items = []
      state.cart.organization = null
    }
  },
  actions: {

  },
  getters: {
    cart: state => {
      return state.cart
    }
  }
}
