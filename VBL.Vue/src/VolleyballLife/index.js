export default {
  baseURL: 'https://localhost:44351/api/v1.0',
  user: {
    login: '/account/Login',
    register: '/account/register',
    getCurrent: '/Me'
  },
  shared: {
    getSelectOptions: (organizationId) => organizationId ? '/Tournament/Selects/' + organizationId : '/Tournament/Selects'
  }
}
