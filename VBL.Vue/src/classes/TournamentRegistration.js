import Player from './TournamentRegistrationPlayer'
export default class TournamentRegistration {
  constructor () {
    this.tournamentId = 0
    this.division = null
    this.players = []
    this.teamName = null
    this.confirmed = false
    this.paymentType = null
    this.finish = null
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
  get valid () {
    // console.log(!!this.division)
    // console.log(this.players.every(player => player.valid))
    return !!this.division && this.players.every(player => player.valid) && this.confirmed && !!this.paymentType
  }
  get addOnQty () {
    return this.division && this.division.addOn ? this.division.addOn.qty : 0
  }
  get dto () {
    return {
      id: 0,
      tournamentId: this.tournamentId,
      tournamentDivisionId: this.division.id,
      players: this.players,
      teamname: this.teamName,
      confirmed: this.confirmed,
      paymentType: this.paymentType,
      addOnQty: this.addOnQty,
      finish: this.finish
    }
  }
}
