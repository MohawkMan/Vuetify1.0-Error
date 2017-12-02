<template>
  <v-app id="vbl">
    <public-nav v-if="nav == 'public'"></public-nav>
    <private-nav v-if="nav == 'private'"></private-nav>
    <v-content>
      <router-view></router-view>
    </v-content>
  </v-app>
</template>

<script>
  import PublicNav from './components/Nav/Public.vue'
  import PrivateNav from './components/Nav/Private.vue'
  import * as actions from './store/ActionTypes'

  export default {
    data: () => ({
      buttons: false
    }),
    computed: {
      nav () {
        return this.$store.getters.nav
      }
    },
    components: {
      'public-nav': PublicNav,
      'private-nav': PrivateNav
    },
    created () {
      if (this.$auth.isAuthenticated()) {
        this.$store.dispatch(actions.LOAD_USER)
      }
    }
  }
</script>
