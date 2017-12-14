export default class TournamentRegistrationPlayer {
  constructor () {
    this.vblId = null
    this.firstName = null
    this.lastName = null
    this.phone = null
    this.email = null
    this.dob = null
    this.dobFormatted = null
    this.city = null
    this.state = null
    this.cbva = null
    this.usav = null
  }
  get fullname () {
    return `${this.firstName} ${this.lastName}`
  }
}