export default class Organization {
  constructor (dto) {
    this.id = 0
    this.name = null
    this.username = null
    this.logoUrl = null
    this.description = null
    this.isActive = false
    this.isPublic = false
    this.websiteUrl = null
    this.contact = null
    this.facebook = null
    this.twitter = null
    this.instagram = null
    this.snapchat = null
    this.organizationMembers = []
    this.photos = []

    if (dto) {
      this.update(dto)
    }
  }
  // methods
  update (dto) {
    if (typeof dto === 'string') dto = JSON.parse(dto)
    Object.assign(this, dto)
  }
  // Getters
}
