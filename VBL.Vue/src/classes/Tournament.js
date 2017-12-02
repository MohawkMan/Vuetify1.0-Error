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
}
