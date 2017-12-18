
import AddOn from './TournamentRegistrationAddOn'

export default class TournamentRegistrationWindow {
  constructor (dto) {
    this.id = 0
    this.fee = null
    this.feeIsPerTeam = true
    this.startDate = null
    this.startDateFormatted = null
    this.startTime = null
    this.endDate = null
    this.endDateFormatted = null
    this.endTime = null
    this.isEarly = false
    this.isLate = false
    this.canPayAtEvent = true
    this.canProcessPayment = false

    this.addOn = new AddOn()

    if (dto) {
      this.update(dto)
    }
  }

  update (dto) {
    Object.assign(this, dto)
  }
  get feeString () {
    return `$ ${parseFloat(this.fee).toFixed(2)}/${this.feeIsPerTeam ? 'per team' : 'per player'}`
  }
  get isCurrent () {
    return true
  }
  get selectPayment () {
    return this.canPayAtEvent && this.canProcessPayment
  }
}
