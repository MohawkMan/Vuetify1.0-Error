import endpoints from './endpoints'

export default class VBL {
  constructor (axios) {
    axios
    endpoints
  }

  getCurrentUser () {
    return this.axios.get(endpoints.user.getCurrent)
  }
}
