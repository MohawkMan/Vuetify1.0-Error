import endpoints from './endpoints'
import TournamentSDK from './tournament'

export default class VBL {
  constructor (http) {
    endpoints
    this.http = http
    this.tournament = new TournamentSDK(http)
  }
}
