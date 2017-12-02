<template>
  <div>
    <v-navigation-drawer temporary v-model="sideNav" absolute>
      <v-list>
        <v-list-tile
          router
          to="/"
        >
          <v-list-tile-content>Volleyball Life</v-list-tile-content>
        </v-list-tile>
        <template v-for="item in navItems">
          <v-list-tile v-if="item.to" 
            :key="item.title"
            router
            :to="item.to"
          >
            <v-list-tile-action>
              <v-icon> {{ item.icon }}</v-icon>
            </v-list-tile-action>
            <v-list-tile-content>{{ item.title }}</v-list-tile-content>
          </v-list-tile>
          <v-list-tile v-else
            :key="item.title"
            @click="item.click"
          >
            <v-list-tile-action>
              <v-icon> {{ item.icon }}</v-icon>
            </v-list-tile-action>
            <v-list-tile-content>{{ item.title }}</v-list-tile-content>
          </v-list-tile>
        </template>
      </v-list>
    </v-navigation-drawer>
    <v-toolbar dark color="primary">
      <v-toolbar-side-icon @click.native.stop="sideNav = !sideNav" class="hidden-md-and-up"></v-toolbar-side-icon>
      <v-toolbar-title>
        <router-link to="/" tag="span" style="cursor: pointer">Volleyball Life</router-link>
      </v-toolbar-title>
      <v-spacer></v-spacer>
      <v-toolbar-items class="hidden-sm-and-down">
        <template v-for="item in navItems">
          <v-btn v-if="item.to"
            :key="item.title"
            flat
            router
            :to="item.to"
            >
            <v-icon class="mr-1">{{ item.icon }}</v-icon>
            {{ item.title }}
          </v-btn>
          <v-btn v-else
            :key="item.title"
            flat
            @click="item.click"
            >
            <v-icon class="mr-1">{{ item.icon }}</v-icon>
            {{ item.title }}
          </v-btn>
        </template>
      </v-toolbar-items>
    </v-toolbar>
    <v-dialog v-model="loginDialog" max-width="500px">
      <app-login></app-login>
    </v-dialog>
  </div>
</template>

<script>
import Login from './Account/Login.vue'

export default {
  data () {
    return {
      sideNav: false,
      loginDialog: false
    }
  },
  computed: {
    navItems () {
      let items = [
        {
          icon: 'lock_open',
          title: 'Sign In',
          to: '',
          click: () => {
            this.loginDialog = !this.loginDialog
          }
        }
      ]
      if (this.userIsAuthenticated) {
        items = [
          {
            icon: 'account_circle',
            title: 'My Profile',
            to: '/profile',
            click: ''
          }
        ]
      }
      return items
    },
    userIsAuthenticated () {
      return false
      // return this.$store.getters.user !== null && this.$store.getters.user !== undefined
    }
  },
  components: {
    'app-login': Login
  }
}
</script>