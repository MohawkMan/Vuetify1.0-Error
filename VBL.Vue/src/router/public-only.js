import Vue from 'vue'

export default (to, from, next) => {
  // console.log(Vue.prototype.$auth.isAuthenticated())
  if (Vue.prototype.$auth.isAuthenticated()) {
    // console.log('Is Auth > redirect')
    next('me')
  } else {
    // console.log('No Auth > continue')
    next()
  }
}
// Make private and public only files
