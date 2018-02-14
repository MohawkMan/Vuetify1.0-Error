import endpoints from './endpoints'
import TournamentSDK from './tournament'
import OrganizationSDK from './organization'
import CartSDK from './Cart'
import UserSDK from './user'

export default class VBL {
  constructor (http) {
    endpoints
    this.http = http
    this.tournament = new TournamentSDK(http)
    this.organization = new OrganizationSDK(http)
    this.cart = new CartSDK(http)
    this.user = new UserSDK(http)
  }
  putFeedback (dto) {
    return this.http.put(endpoints.feedback, dto)
  }
  verifyAAU (dto) {
    // {
    //   "id": "string",
    //   "lastname": "string",
    //   "zipcode": "string",
    //   "dob": "string",
    //   "by": "string"
    // }
    return this.http.post(endpoints.aau.verify, dto)
  }
}
