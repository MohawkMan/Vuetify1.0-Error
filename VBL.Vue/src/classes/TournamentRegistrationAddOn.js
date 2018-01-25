export default class TournamentRegistrationAddOn {
  constructor (dto) {
    this.id = 0
    this.name = 'VolleyOC T-Shirt'
    this.price = 20
    this.description = 'The VolleyOC winter series tank top. Grab one before they are gone... might as well grab 2 so you and your partner can match!'
    this.imageUrl = '/static/VollyOCShirt1.jpg'
    this.quantityInStock = 30
    this.qty = 0
    if (dto) {
      this.update(dto)
    }
  }
  update (dto) {
    if (typeof dto === 'string') dto = JSON.parse(dto)
    Object.assign(this, dto)
  }
  get subtotal () {
    return this.qty * this.price
  }
}
