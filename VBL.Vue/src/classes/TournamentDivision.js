import Day from './TournamentDay'
import RegWindow from './TournamentRegistrationWindow'
import moment from 'moment'

export default class TournamentDivision {
  constructor (dto) {
    this.id = 0
    this.minTeams = null
    this.maxTeams = null

    this.ageType = null
    this.gender = null
    this.division = null
    this.days = [new Day()]
    this.location = null
    this.registrationWindows = [new RegWindow()]
  }

  get startDate () {
    if (this.days.length === 0) return null
    return moment(this.days[0].date)
  }
}
