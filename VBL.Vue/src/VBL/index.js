import endpoints from './endpoints'
import TournamentSDK from './tournament'
import OrganizationSDK from './organization'
import CartSDK from './Cart'

export default class VBL {
  constructor (http) {
    endpoints
    this.http = http
    this.tournament = new TournamentSDK(http)
    this.organization = new OrganizationSDK(http)
    this.cart = new CartSDK(http)
  }
  putFeedback (dto) {
    return this.http.put(endpoints.feedback, dto)
  }
}
