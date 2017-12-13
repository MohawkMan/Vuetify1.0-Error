<template>
  <v-app id="vbl">
    <public-nav v-if="nav == 'public'"></public-nav>
    <private-nav v-if="nav == 'private'"></private-nav>
    <v-content>
      <v-container fill-height v-if="loading">
        <v-layout row wrap align-center>
          <v-flex xs8 offset-xs2>
            <v-layout row wrap text-xs-center>
              <v-flex xs12>
                <h3>Loading</h3>
              </v-flex>
              <v-flex xs12>
                <v-progress-linear v-bind:indeterminate="true"></v-progress-linear>
              </v-flex>
            </v-layout>
          </v-flex>
        </v-layout>
      </v-container>
      <v-container fill-height v-else-if="error">
        <v-layout row wrap align-center>
          <v-flex xs8 offset-xs2>
            <v-layout row wrap text-xs-center>
              <v-flex xs12>
                <v-alert outline color="error" icon="warning" value="true">
                  Welp... we shanked that one <v-icon>mood_bad</v-icon><br>(Error: {{error}})
                </v-alert>
              </v-flex>
            </v-layout>
          </v-flex>
        </v-layout>
      </v-container>
      <router-view v-else></router-view>
    </v-content>
  </v-app>
</template>

<script>
  import PublicNav from './Nav/Public.vue'
  import PrivateNav from './Nav/Private.vue'
  import * as actions from './store/ActionTypes'
  import { mapGetters } from 'vuex'

  export default {
    data: () => ({
      buttons: false
    }),
    computed: {
      ...mapGetters([
        'nav',
        'user',
        'loading',
        'error'
      ])
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
