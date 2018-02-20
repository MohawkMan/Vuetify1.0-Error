import * as StatusEnum from '../classes/TournamentStatus'
import moment from 'moment'

export default class TournamentSummary {
  constructor (dto) {
    this.id = 0
    this.startDate = moment()
    this.endDate = moment()
    this.name = ''
    this.locations = []
    this.organization = {
      id: 0,
      name: '',
      username: ''
    }
    this.isPublic = false
    this.sanctionedBy = ''
    this.statusId = 0

    if (dto) {
      this.update(dto)
    }
  }

  update (dto) {
    if (typeof dto === 'string') dto = JSON.parse(dto)
    Object.assign(this, dto)
    this.startDate = moment(dto.startDate)
    this.endDate = moment(dto.endDate)
  }

  // getters
  get dateStatus () {
    if (!this.startDate) return StatusEnum.UPCOMING
    if (this.startDate.isAfter()) return StatusEnum.UPCOMING
    if (moment().isBetween(this.startDate, this.endDate, 'd', '[]')) return StatusEnum.INPROCESS
    return StatusEnum.PAST
  }
  get locationsString () {
    return this.locations.join(', ')
  }
}
