import PublicOnly from './public-only'

// Public Views
import Home from '@/Pages/Public/Home'
import Players from '@/Pages/Public/Players'
import Rankings from '@/Pages/Public/Rankings'
import Tournaments from '@/Pages/Public/Tournaments'
import Organizations from '@/Pages/Public/Organizations'
import Join from '@/Pages/Public/Account/Register'
import SignIn from '@/Pages/Public/Account/Login'
import Privacy from '@/Pages/Public/Privacy.vue'
import EmailConfirm from '@/Pages/Public/Confirm.vue'
import JuniorPoints from '@/Pages/Public/JuniorPoints.vue'

export default [
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
    path: '/privacy',
    name: 'privacy',
    component: Privacy
  },
  {
    path: '/juniorpoints',
    name: 'juniorpoints',
    component: JuniorPoints
  },
  {
    path: '/confirm/:emailId/:token',
    name: 'confirm',
    component: EmailConfirm,
    props: true
  }
]
