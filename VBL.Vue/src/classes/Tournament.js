import TournamentDivision from './TournamentDivision'
import moment from 'moment'

export default class Tournament {
  constructor (dto) {
    this.id = 0
    this.name = ''
    this.isPublic = false
    this.organizationId = null
    this.divisions = []
  }

  addDivision (dto) {
    this.divisions.push(new TournamentDivision(dto))
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

    let dates = this.divisions.map(d => new Date(d.startDate))
    return moment(Math.min(...dates)).format('MM/DD/YYYY')
  }
}
