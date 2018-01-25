export default class User {
  constructor (dto) {
    this.update(dto)
  }
  // methods
  update (dto) {
    if (typeof dto === 'string') dto = JSON.parse(dto)
    Object.assign(this, dto)
  }
  isPageAdmin (pageUsername) {
    if (!this.pages || this.pages.length === 0) return false

    let page = this.pages.find(p => p.username === pageUsername)
    return page != null
  }
}
