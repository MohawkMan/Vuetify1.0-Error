import endpoints from './endpoints'

export default class UserSDK {
  constructor (http) {
    this.http = http
  }

  postEmail (email) {
    return this.http.post(endpoints.user.email, email)
  }
}
