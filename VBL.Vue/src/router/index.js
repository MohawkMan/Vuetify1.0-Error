import Vue from 'vue'
import Router from 'vue-router'
import AuthGuard from './auth-guard'
import PublicOnly from './public-only'

// Public Views
import Home from '@/Pages/Public/Home'
import Players from '@/Pages/Public/Players'
import Rankings from '@/Pages/Public/Rankings'
import Tournaments from '@/Pages/Public/Tournaments'
import Organizations from '@/Pages/Public/Organizations'
import Join from '@/Pages/Public/Account/Register'
import SignIn from '@/Pages/Public/Account/Login'

// Private Views
import Profile from '@/Pages/Private/User/Profile'
import MyTournaments from '@/Pages/Private/User/Tournaments'
import TD from '@/Pages/Private/Organization/Tournament.vue'

Vue.use(Router)

export default new Router({
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
      path: '/td',
      name: 'td',
      component: TD,
      beforeEnter: AuthGuard
    }
  ],
  mode: 'history'
})
