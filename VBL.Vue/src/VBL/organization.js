import endpoints from './endpoints'

export default class OrganizationSDK {
  constructor (http) {
    this.http = http
  }
  getSettings (id) {
    return this.http.get(endpoints.organization.getSettingsById(id))
  }
  getConnectURL (id) {
    return this.http.get(endpoints.organization.getConnectURLById(id))
  }
}
