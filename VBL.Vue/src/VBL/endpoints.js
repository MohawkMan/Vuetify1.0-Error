export default {
  baseURL: process.env.NODE_ENV === 'production'
    ? 'https://api.volleyballlife.com/api/v1.0'
    : 'https://localhost:44351/api/v1.0',
//  baseURL: 'https://api.volleyballlife.com/api/v1.0',
//  baseURL: 'https://localhost:44351/api/v1.0',
  email: {
    confirm: (emailId) => `email/confirm/${emailId}`
  },
  organization: {
    get: (usernameOrId) => `/organization/${usernameOrId}`
  },
  rankings: {
    all: 'Ranking',
    pointScale: 'Ranking/PointScale',
    teamMultiplier: 'Ranking/TeamMultiplier'
  },
  tournament: {
    getSelectOptions: (organizationId) => organizationId ? `/Tournament/Selects/${organizationId}` : '/Tournament/Selects',
    getById: (id) => `/tournament/${id}`,
    getCopyById: (id) => `/tournament/${id}/copy`,
    getRawById: (id) => `/tournament/${id}/raw`,
    put: '/tournament',
    getAll: '/tournament/list',
    getByOrganizationId: (organizationId) => `/tournament/list/${organizationId}`,
    getByOrganizationUserName: (username) => `/tournament/${username}/list`,
    register: '/tournament/register',
    bulkRegister: '/tournament/register/upload',
    bulkRegisterOverwrite: '/tournament/register/upload/true'
  },
  user: {
    login: '/account/Login',
    register: '/account/register',
    getCurrent: '/Me',
    phone: '/phone/me'
  }
}
