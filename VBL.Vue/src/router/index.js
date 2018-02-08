import Vue from 'vue'
import Router from 'vue-router'
import AuthGuard from './auth-guard'
import BeforeEach from './before-each'

// Public Views
import Checkout from '@/Pages/Public/Checkout.vue'
import Tournaments from '@/Pages/Public/Tournaments'
import TourneyBrochure from '@/Pages/Public/TournamentBrochure.vue'

// Private Views
import Profile from '@/Pages/Private/User/Profile'
import MyTournaments from '@/Pages/Private/User/Tournaments'
import OrganizationCreate from '@/Pages/Private/Organization/Create.vue'
import TournamentCreator from '@/Pages/Private/Organization/TournamentCreate.vue'

import PublicPages from './public-routes'
import AdminPages from './organization-admin'
Vue.use(Router)

const router = new Router({
  routes: [
    ...PublicPages,
    {
      path: '/checkout',
      name: 'checkout',
      component: Checkout
    },
    {
      path: '/organization/create',
      name: 'organization-create',
      component: OrganizationCreate,
      beforeEnter: AuthGuard
    },
    {
      path: '/me',
      name: 'me',
      component: Profile,
      beforeEnter: AuthGuard
    },
    {
      path: '/my/tournaments',
      name: 'my-tournaments',
      component: MyTournaments,
      beforeEnter: AuthGuard
    },
    {
      path: '/:username/tournaments/create',
      name: 'tournament-create',
      component: TournamentCreator,
      beforeEnter: AuthGuard
    },
    {
      path: '/:username/tournaments/:id/edit',
      name: 'tournament-edit',
      component: TournamentCreator,
      beforeEnter: AuthGuard
    },
    {
      path: '/:username/tournaments',
      name: 'organization-tournaments',
      component: Tournaments,
      props: true
    },
    {
      path: '/:username/tournament/:tournamentId',
      name: 'tournament-brochure',
      component: TourneyBrochure,
      props: (route) => { return {tournamentId: route.params.tournamentId, mode: 'information', username: route.params.username} }
    },
    {
      path: '/:username/tournament/:tournamentId/register',
      name: 'tournament-register',
      component: TourneyBrochure,
      props: (route) => { return {tournamentId: route.params.tournamentId, mode: 'register', username: route.params.username} }
    },
    {
      path: '/:username/tournament/:tournamentId/location',
      name: 'tournament-location',
      component: TourneyBrochure,
      props: (route) => { return {tournamentId: route.params.tournamentId, mode: 'location', username: route.params.username} }
    },
    {
      path: '/:username/tournament/:tournamentId/teams',
      name: 'tournament-teams',
      component: TourneyBrochure,
      props: (route) => { return {tournamentId: route.params.tournamentId, mode: 'teams', username: route.params.username} }
    },
    ...AdminPages
  ],
  mode: 'history'
})

router.beforeEach(BeforeEach)

export default router
