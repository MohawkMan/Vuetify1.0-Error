import endpoints from './endpoints'

export default class TournamentSDK {
  constructor (http) {
    this.http = http
  }
  getTournamentById (id) {
    return this.http.get(endpoints.tournament.getById(id))
  }
  save (tournament) {
    if (tournament.dto) {
      tournament = tournament.dto
    }
    console.log(tournament)
    return this.http.put(endpoints.tournament.put, tournament)
  }
}
