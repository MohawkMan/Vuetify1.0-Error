import Division from './TournamentDivision'
import moment from 'moment'
import uniq from 'lodash.uniq'
import sum from 'lodash.sum'
import Registration from './TournamentRegistration'

export default class Tournament {
  constructor (dto) {
    this.id = 0
    this.name = ''
    this.isPublic = false
    this.organizationId = 0
    this.organization = null
    this.divisions = []
    if (dto) {
      this.update(dto)
    }
  }
  // methods
  update (dto) {
    Object.assign(this, dto)
    this.divisions = dto.divisions.map(d => new Division(d))
  }

  newRegistration (divisionId) {
    let r = new Registration()
    r.tournamentId = this.id
    r.divisionId = divisionId
    return r
  }
  // getters
  get divisionHeaders () {
    return [
      {text: 'Type', value: 'ageName', align: 'left'},
      {text: 'Gender', value: 'genderName', align: 'left'},
      {text: 'Division', value: 'divisionName', align: 'left'},
      {text: 'Location', value: 'locationName', align: 'left'}
    ]
  }
  get ageCount () {
    if (this.divisions.length === 0) return false

    return uniq(this.divisions.map(d => d.ageType.name)).length > 1
  }
  get ageGroups () {
    return uniq(this.divisions.map(d => d.ageType.name))
  }
  get dayCount () {
    return sum(this.divisions.map(d => d.dayCount))
  }
  get startDate () {
    if (this.divisions.length === 0) return null

    return moment.min(this.divisions.map(d => d.startDate))
  }
  get divisionsString () {
    return uniq(this.divisions.map(d => d.divisionsString)).join(', ')
  }
  get locationCount () {
    return uniq(this.divisions.map(d => d.location.name)).length
  }
  get locationsString () {
    return uniq(this.divisions.map(d => d.location.name)).join(', ')
  }
}
