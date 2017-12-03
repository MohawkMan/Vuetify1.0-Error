import TournamentDivision from './TournamentDivision'

export default class Tournament {
  constructor (dto) {
    this.id = 0
    this.name = ''
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
}
