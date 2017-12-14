import Day from './TournamentDay'
import RegWindow from './TournamentRegistrationWindow'
import moment from 'moment'
import uniq from 'lodash.uniq'

export default class TournamentDivision {
  constructor (dto) {
    this.id = 0
    this.minTeams = null
    this.maxTeams = null
    this.numOfPlayers = 2
    this.numAllowedOnRoster = 2

    this.ageType = null
    this.gender = null
    this.division = null
    this.days = [new Day()]
    this.location = null
    this.registrationWindows = [new RegWindow()]

    if (dto) {
      this.update(dto)
    }
  }

  update (dto) {
    Object.assign(this, dto)
    this.days = dto.days.map(d => new Day(d))
    this.registrationWindows = dto.registrationWindows.map(r => new RegWindow(r))
  }

  get name () {
    return `${this.gender.name} ${this.division.name}`
  }
  get dayCount () {
    return uniq(this.days.map(d => d.date)).length
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
    if (this.registrationWindows.length === 0) return null

    return this.registrationWindows.find((w) => { return w.isCurrent }).fee
  }
  get currentEntryFeeString () {
    if (this.registrationWindows.length === 0) return null

    return this.registrationWindows.find((w) => { return w.isCurrent }).feeString
  }
  get divisionsString () {
    return `${this.gender.name}  ${this.division.name}`
  }
  get currentRegistrationWindow () {
    return this.registrationWindows.find((w) => { return w.isCurrent })
  }
  get addOn () {
    return this.registrationWindows.find((w) => { return w.isCurrent }).addOn
  }
}
