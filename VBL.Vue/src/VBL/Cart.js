import endpoints from './endpoints'

export default class CartSDK {
  constructor (http) {
    this.http = http
  }

  process (bag) {
    return this.http.post(endpoints.cart.process, bag)
  }
}
