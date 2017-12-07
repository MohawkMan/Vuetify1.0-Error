import Vue from 'vue'
import Router from 'vue-router'
import AuthGuard from './auth-guard'
import PublicOnly from './public-only'
import BeforeEach from './before-each'

// Public Views
import Home from '@/Pages/Public/Home'
import Players from '@/Pages/Public/Players'
import Rankings from '@/Pages/Public/Rankings'
import Tournaments from '@/Pages/Public/Tournaments'
import Organizations from '@/Pages/Public/Organizations'
import Join from '@/Pages/Public/Account/Register'
import SignIn from '@/Pages/Public/Account/Login'
import TourneyBrochure from '@/Pages/Public/TournamentBrochure.vue'

// Private Views
import Profile from '@/Pages/Private/User/Profile'
import MyTournaments from '@/Pages/Private/User/Tournaments'
import OrgHome from '@/Pages/Private/Organization/Home.vue'
// import OrgTourneys from '@/Pages/Private/Organization/TournamentList.vue'
import TournamentCreator from '@/Pages/Private/Organization/TournamentCreate.vue'

Vue.use(Router)

const router = new Router({
  routes: [
    {
      path: '/',
      name: 'home',
      component: Home
    },
    {
      path: '/join',
      name: 'join',
      component: Join,
      beforeEnter: PublicOnly
    },
    {
      path: '/signin',
      name: 'signin',
      component: SignIn,
      beforeEnter: PublicOnly
    },
    {
      path: '/players',
      name: 'players',
      component: Players
    },
    {
      path: '/rankings',
      name: 'rankings',
      component: Rankings
    },
    {
      path: '/tournaments',
      name: 'tournaments',
      component: Tournaments
    },
    {
      path: '/organizations',
      name: 'organizations',
      component: Organizations
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
      path: '/:username/tournaments',
      name: 'organization-tournaments',
      component: Tournaments,
      beforeEnter: AuthGuard,
      props: true
    },
    {
      path: '/:username/admin',
      name: 'organization-home',
      component: OrgHome,
      beforeEnter: AuthGuard,
      props: true
    },
    {
      path: '/:username/tournament/:tournamentId',
      name: 'tournament-brochure',
      component: TourneyBrochure,
      props: (route) => { return {tournamentId: route.params.tournamentId, mode: 'view'} }
    },
    {
      path: '/tournament/:tournamentId/register',
      name: 'tournament-register',
      component: TourneyBrochure,
      props: (route) => { return {tournamentId: route.params.tournamentId, mode: 'register'} }
    }
  ],
  mode: 'history'
})

router.beforeEach(BeforeEach)

export default router
