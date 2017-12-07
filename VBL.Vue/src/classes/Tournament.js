import Division from './TournamentDivision'
import moment from 'moment'
import uniq from 'lodash.uniq'

export default class Tournament {
  constructor (dto) {
    this.id = 0
    this.name = ''
    this.isPublic = false
    this.organizationId = 0
    this.divisions = []
    if (dto) {
      this.update(dto)
    }
  }

  update (dto) {
    Object.assign(this, dto)
    this.divisions = dto.divisions.map(d => new Division(d))
  }
  get divisionHeaders () {
    return [
      {text: 'Type', value: 'ageName', align: 'left'},
      {text: 'Gender', value: 'genderName', align: 'left'},
      {text: 'Division', value: 'divisionName', align: 'left'},
      {text: 'Location', value: 'locationName', align: 'left'}
    ]
  }
  get startDate () {
    if (this.divisions.length === 0) return null

    return moment.min(this.divisions.map(d => d.startDate))
  }
  get divisionsString () {
    return uniq(this.divisions.map(d => d.gender.name + ' ' + d.division.name)).join(', ')
  }
  get locationsString () {
    return uniq(this.divisions.map(d => d.location.name)).join(', ')
  }
}
