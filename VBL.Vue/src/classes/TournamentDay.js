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
    Object.assign(this, dto)
  }
}
