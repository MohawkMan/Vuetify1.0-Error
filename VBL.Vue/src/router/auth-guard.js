import Vue from 'vue'

export default (to, from, next) => {
  if (Vue.prototype.$auth.isAuthenticated()) {
    next()
  } else {
    next('/signin?returnURL=' + to.fullPath)
  }
}
// Make private and public only files
