import Player from './TournamentRegistrationPlayer'
export default class TournamentRegistration {
  constructor () {
    this.tournamentId = 0
    this.division = null
    this.feeConfirmed = false
    this.players = []
  }

  setDivision (division) {
    this.division = division
    this.initPlayers()
  }
  initPlayers () {
    for (; this.players.length < this.division.numOfPlayers;) {
      this.players.push(new Player())
    }
    for (; this.players.length > this.division.numAllowedOnRoster;) {
      this.players.pop()
    }
  }

  get rosterFull () {
    return this.players.length === this.division.numAllowedOnRoster
  }
}
