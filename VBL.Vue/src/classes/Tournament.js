import Division from './TournamentDivision'
import moment from 'moment'
import uniq from 'lodash.uniq'
import sum from 'lodash.sum'
import Registration from './TournamentRegistration'
import * as StatusEnum from '../classes/TournamentStatus'

export default class Tournament {
  constructor (dto) {
    this.description = ''
    this.divisions = []
    this.externalRegistrationUrl = ''
    this.id = 0
    this.isPublic = false
    this.name = ''
    this.organization = null
    this.organizationId = 0
    this.statusId = 0
    this.divisionTemplate = new Division()

    if (dto) {
      this.update(dto)
    }
  }
  // methods
  update (dto) {
    if (typeof dto === 'string') dto = JSON.parse(dto)
    Object.assign(this, dto)
    this.divisions = dto.divisions.map(d => new Division(d))
    this.divisionTemplate = new Division()
    if (dto.divisions.length > 0) {
      this.divisionTemplate.update(dto.divisions[0])
    }
  }
  newRegistration (divisionId) {
    let r = new Registration()
    r.tournamentId = this.id
    r.divisionId = divisionId
    r.organization = this.organization
    return r
  }
  cascadeTemplateChange () {
    this.divisions.forEach((d, i) => {
      d.updateFromTemplate(this.divisionTemplate)
    })
  }

  // getters
  get dto () {
    return {
      description: this.description,
      divisions: this.divisions.map((d) => {
        return d.dto
      }),
      externalRegistrationUrl: this.externalRegistrationUrl,
      id: this.id,
      isPublic: this.isPublic,
      name: this.name,
      organizationId: this.organizationId,
      statusId: this.statusId
    }
  }
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
  get dates () {
    return [].concat(...this.divisions.map(d => d.dates))
  }
  get uniqueDays () {
    return uniq(this.dates)
  }
  get dayCount () {
    return this.uniqueDays.length
  }
  get isOneDay () {
    return this.dayCount < 2
  }
  get startDate () {
    if (this.divisions.length === 0) return null

    return moment.min(this.divisions.map(d => d.startDate))
  }
  get startDateDisplay () {
    return this.startDate && moment(this.startDate).format('dddd, MMMM Do YYYY')
  }
  get endDate () {
    return moment.max(this.divisions.map(d => d.endDate))
  }
  get divisionsString () {
    return uniq(this.divisions.map(d => d.divisionsString)).join(', ')
  }
  get locationCount () {
    return uniq(this.divisions.map(d => d.location && d.location.name)).length
  }
  get isOneLocation () {
    return this.locationCount < 2
  }
  get locationsString () {
    return uniq(this.divisions.map(d => d.location.name)).join(', ')
  }
  get teamCount () {
    return sum(this.divisions.map(d => d.teams.length))
  }
  get dateStatus () {
    if (this.startDate.isAfter()) return StatusEnum.UPCOMING
    if (moment().isBetween(this.startDate, this.endDate, 'd', '[]')) return StatusEnum.INPROCESS
    return StatusEnum.PAST
  }
  get divisionsWithTeams () {
    return this.divisions.filter(d => d.teams.length > 0)
  }
  get regOpen () {
    let open = false
    this.divisions.forEach((d) => {
      if (d.currentRegistrationWindow) {
        open = true
      }
    })
    return open
  }
  get isComplete () {
    return this.statusId === StatusEnum.COMPLETE
  }
  get isCanceled () {
    return this.statusId === StatusEnum.CANCELED
  }
  get isEditable () {
    return !this.isComplete && !this.isCanceled
  }
  get isAAU () {
    return this.divisions && this.divisions.some((d) => {
      return d.sanctioningBodyId === 'AAU'
    })
  }
  get isAVP () {
    return this.divisions && this.divisions.some((d) => {
      return d.sanctioningBodyId.startsWith('AVP')
    })
  }
  get sanctionedBy () {
    var division = this.divisions && this.divisions.find((d) => {
      return d.sanctioningBodyId
    })
    return (division && division.sanctioningBodyId) || ''
  }
}
