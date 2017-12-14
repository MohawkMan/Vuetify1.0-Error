
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
    this.canPayAtEvent = false
    this.canProcessPayment = true

    this.addOn = {
      name: 'VolleyOC T-Shirt',
      price: '20',
      description: 'The VolleyOC winter series tank top. Grab one before they are gone... might as well grab 2 so you and your partner can match!',
      imageUrl: '/static/VollyOCShirt1.jpg',
      quantityInStock: 30
    }

    if (dto) {
      this.update(dto)
    }
  }

  update (dto) {
    Object.assign(this, dto)
  }
  get feeString () {
    return `$ ${parseFloat(this.fee).toFixed(2)}/${this.feeIsPerTeam ? 'per team' : 'per player'}`
  }
  get isCurrent () {
    return true
  }
}
