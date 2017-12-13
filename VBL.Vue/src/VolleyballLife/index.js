export default {
//  baseURL: process.env.NODE_ENV === 'production'
//    ? 'https://api.volleyballlife.com/api/v1.0'
//    ? 'https://volleyballlife-api.azurewebsites.net/api/v1.0'
//    : 'https://localhost:44351/api/v1.0',
  baseURL: 'https://api.volleyballlife.com/api/v1.0',
  user: {
    login: '/account/Login',
    register: '/account/register',
    getCurrent: '/Me'
  },
  tournament: {
    getSelectOptions: (organizationId) => organizationId ? `/Tournament/Selects/${organizationId}` : '/Tournament/Selects',
    getById: (id) => `/tournament/${id}`,
    create: '/tournament',
    getAll: '/tournament/list',
    getByOrganizationId: (organizationId) => `/tournament/list/${organizationId}`
  }
}
