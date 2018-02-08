export default class CartItem {
  constructor (dto) {
    this.id = 0
    this.name = null
    this.description = null
    this.amount = null
    this.quantity = 1
    this.registration = null
    this.product = null

    if (dto) {
      this.update(dto)
    }
  }
  // methods
  update (dto) {
    if (typeof dto === 'string') dto = JSON.parse(dto)
    Object.assign(this, dto)
  }
  get playerEmails () {
    return this.registration ? this.registration.players.map(player => player.email) : []
  }
}
