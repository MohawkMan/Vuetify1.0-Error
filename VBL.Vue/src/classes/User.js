export default class User {
  constructor (dto) {
    this.update(dto)
  }
  // methods
  update (dto) {
    Object.assign(this, dto)
  }
  isPageAdmin (pageUserName) {
    if (!this.pages || this.pages.length === 0) return false

    let page = this.pages.find(p => p.username === pageUserName)
    return page != null
  }
}