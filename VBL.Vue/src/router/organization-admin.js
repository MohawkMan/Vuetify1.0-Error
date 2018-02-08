import AuthGuard from './auth-guard'

import OrganizationHome from '@/Pages/Private/Organization/Home.vue'
import ResultsEditor from '@/Pages/Private/Organization/ResultsEdit.vue'
import Settings from '@/Pages/Private/Organization/Settings.vue'
import OrgBrochure from '@/Pages/Public/OrganizationBrochure.vue'

export default [
  {
    path: '/:username/admin',
    name: 'organization-home',
    component: OrganizationHome,
    beforeEnter: AuthGuard,
    props: true
  },
  {
    path: '/:username/admin/results',
    name: 'organization-results-edit',
    component: ResultsEditor,
    beforeEnter: AuthGuard,
    props: true
  },
  {
    path: '/:username/admin/settings',
    name: 'organization-settings',
    component: Settings,
    beforeEnter: AuthGuard,
    props: true
  },
  {
    path: '/:username',
    name: 'organization-brochure',
    component: OrgBrochure,
    props: true
  }
]
