import Day from './TournamentDay'

export default class TournamentDivision {
  constructor (dto) {
    this.id = 0
    this.ageType = null
    this.gender = null
    this.division = null
    this.days = [new Day()]
    this.location = null
    this.entryFee = null
    this.maxTeams = null
    this.registrationStartDate = null
    this.registrationStartTime = null
    this.registrationEndDate = null
    this.registrationEndTime = null
    this.info = ''
    this.tournamentRegistrationEmailId = null
  }

  get startDate () {
    if (this.days.length === 0) return undefined
    return this.days[0].date
  }

  get ageName () {
    return this.ageType ? this.ageType.name : ''
  }
  get genderName () {
    return this.genderType ? this.genderType.name : ''
  }
  get divisionName () {
    return this.divisionType ? this.divisionType.name : ''
  }
  get locationName () {
    return this.locationType ? this.locationType.name : ''
  }
}
