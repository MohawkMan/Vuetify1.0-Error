import Day from './TournamentDay'

export default class TournamentDivision {
  constructor (dto) {
    this.id = 0
    this.ageTypeId = null
    this.genderId = null
    this.divisionId = null
    this.days = [new Day()]
    this.locationId = null
    this.entryFee = null
    this.maxTeams = null
    this.registrationStartDate = null
    this.registrationStartTime = null
    this.registrationEndDate = null
    this.registrationEndTime = null
    this.info = ''
    this.tournamentRegistrationEmailId = null
  }
}
