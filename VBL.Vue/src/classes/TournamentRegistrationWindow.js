
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

    if (dto) {
      this.update(dto)
    }
  }

  update (dto) {
    Object.assign(this, dto)
  }
}
