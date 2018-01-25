import Day from './TournamentDay'
import RegWindow from './TournamentRegistrationWindow'
import moment from 'moment'
import uniq from 'lodash.uniq'

export default class TournamentDivision {
  constructor (dto) {
    this.ageType = null
    this.days = [new Day()]
    this.division = null
    this.dtRefundCutoff = null
    this.emailNote = null
    this.gender = null
    this.id = 0
    this.location = null
    this.maxTeams = null
    this.minTeams = null
    this.numAllowedOnRoster = 2
    this.numOfPlayers = 2
    this.registrationFields = null
    this.registrationWindows = [new RegWindow()]
    this.sanctioningBodyId = null
    this.tournamentDirectorUserId = null

    if (dto) {
      this.update(dto)
    }
  }

  update (dto) {
    if (typeof dto === 'string') dto = JSON.parse(dto)
    Object.assign(this, dto)
    if (dto.days) this.days = dto.days.map(d => new Day(d))
    if (dto.registrationWindows) this.registrationWindows = dto.registrationWindows.map(r => new RegWindow(r))
  }

  updateFromTemplate (template) {
    this.location = template.location
    template.days.forEach((d, i) => {
      this.days[i].updateFromTemplate(d)
    })
    template.registrationWindows.forEach((r, i) => {
      this.registrationWindows[i].updateFromTemplate(r)
    })
  }

  get name () {
    return `${this.gender.name} ${this.division.name}`
  }
  get dates () {
    this.days.map(d => d.date)
  }
  get uniqueDays () {
    return uniq(this.dates)
  }
  get dayCount () {
    return this.uniqueDays.length
  }
  get startDate () {
    if (this.days.length === 0) return null
    return moment.min(this.days.map(d => moment(d.date)))
  }
  get endDate () {
    if (this.days.length === 0) return null
    return moment.max(this.days.map(d => moment(d.date)))
  }
  get currentEntryFee () {
    return this.currentRegistrationWindow ? this.currentRegistrationWindow.fee : this.lastRegistrationWindow.fee
  }
  get currentEntryFeeString () {
    return this.currentRegistrationWindow ? this.currentRegistrationWindow.feeString : this.lastRegistrationWindow.feeString
  }
  get divisionsString () {
    return `${this.gender.name}  ${this.division.name}`
  }
  get currentRegistrationWindow () {
    if (this.registrationWindows.length === 0) return null
    this.registrationWindows.forEach((r) => r.setOffset(this.location.offset))

    return this.registrationWindows.find((w) => { return w.isCurrent })
  }
  get lastRegistrationWindow () {
    if (this.registrationWindows.length === 0) return null
    return this.registrationWindows[this.registrationWindows.length - 1]
  }
  get addOn () {
    return this.currentRegistrationWindow && this.currentRegistrationWindow.addOn
  }
}
