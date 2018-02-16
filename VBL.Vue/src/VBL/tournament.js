import endpoints from './endpoints'

export default class TournamentSDK {
  constructor (http) {
    this.http = http
  }
  getTournamentById (id) {
    return this.http.get(endpoints.tournament.getById(id))
  }
  getTournamentCopyById (id) {
    return this.http.get(endpoints.tournament.getCopyById(id))
  }
  getRawTournamentById (id) {
    return this.http.get(endpoints.tournament.getRawById(id))
  }
  save (tournament) {
    if (tournament.dto) {
      tournament = tournament.dto
    }
    console.log(tournament)
    return this.http.put(endpoints.tournament.put, tournament)
  }
  publish (id, isPublic) {
    return this.http.post(endpoints.tournament.publish(id, isPublic))
  }
  getSeededTeams (id) {
    return this.http.get(endpoints.tournament.getSeededTeams(id))
  }
}
