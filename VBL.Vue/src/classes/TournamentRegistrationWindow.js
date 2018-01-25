
import AddOn from './TournamentRegistrationAddOn'
import moment from 'moment'

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
    // this.isCurrent = false
    // this.open = false

    this.addOn = new AddOn()

    if (dto) {
      this.update(dto)
    }
  }

  setOffset (offset) {
    this.offset = offset
  }
  update (dto) {
    if (typeof dto === 'string') dto = JSON.parse(dto)
    Object.assign(this, dto)
  }
  updateFromTemplate (template) {
    this.fee = template.fee
    this.feeIsPerTeam = template.feeIsPerTeam
    this.startDate = template.startDate
    this.startDateFormatted = template.startDateFormatted
    this.startTime = template.startTime
    this.endDate = template.endDate
    this.endDateFormatted = template.endDateFormatted
    this.endTime = template.endTime
    this.isEarly = template.isEarly
    this.isLate = template.isLate
    this.canPayAtEvent = template.canPayAtEvent
    this.canProcessPayment = template.canProcessPayment
  }
  get feeString () {
    return `$ ${parseFloat(this.fee).toFixed(2)}/${this.feeIsPerTeam ? 'per team' : 'per player'}`
  }
  get isCurrent () {
    if (this.offset) {
      let start = moment(`${this.startDate}T${this.convertTime(this.startTime)}${this.offset}`)
      let end = moment(`${this.endDate}T${this.convertTime(this.endTime)}${this.offset}`)
      return moment().isBetween(start, end, 'minute', '[]')
    }
  }
  get selectPayment () {
    return this.canPayAtEvent && this.canProcessPayment
  }
  convertTime (t) {
    let add = t.toLowerCase().includes('pm') ? 12 : 0
    t = t.toLowerCase().replace(/[^\d:]/g, '')
    let splt = t.split(':')
    return `${+splt[0] + add}:${splt[1]}`.padStart(5, '0')
  }
}
