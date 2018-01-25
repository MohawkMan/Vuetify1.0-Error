export default class Phone {
  constructor (dto) {
    this.id = 0
    this.number = null
    this.isPublic = false
    this.isPrimary = false
    this.isSMS = false
    this.update(dto)
  }
  // methods
  update (dto) {
    if (typeof dto === 'string') dto = JSON.parse(dto)
    Object.assign(this, dto)
  }
  // Getters
  get propsString () {
    let props = []
    this.isPrimary && props.push('Primary')
    this.isPublic && props.push('Public')
    this.isSMS && props.push('SMS Enabled')
    // this.isVerified ? props.push('Verified') : props.push('Unverified')

    return props.join(' | ')
  }
}
