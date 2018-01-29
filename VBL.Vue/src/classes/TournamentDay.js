export default class TournamentDay {
  constructor (dto) {
    this.id = 0
    this.date = null
    this.dateFormatted = null
    this.checkInTime = null
    this.playTime = null

    if (dto) {
      this.update(dto)
    }
  }

  update (dto) {
    if (typeof dto === 'string') dto = JSON.parse(dto)
    Object.assign(this, dto)
  }
  updateFromTemplate (template) {
    this.date = template.date
    this.dateFormatted = template.dateFormatted
    this.checkInTime = template.checkInTime
    this.playTime = template.playTime
  }

  // GETTERS
  get dto () {
    return {
      id: this.id,
      date: this.date,
      checkInTime: this.checkInTime,
      playTime: this.playTime
    }
  }
}
