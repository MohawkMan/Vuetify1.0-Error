import Vue from 'vue'

export default {
  login (username, password) {
    return Vue.$auth.login({username, password})
  }
}
