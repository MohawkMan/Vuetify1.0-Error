export default class TournamentRegistrationPlayer {
  constructor () {
    this.id = 0
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
    this.valid = false
  }
  get fullname () {
    return `${this.firstName} ${this.lastName}`
  }
}
