<template>
  <div>
    <v-navigation-drawer
      fixed
      clipped
      app
      v-model="drawer"
    >
      <v-list dense>
        <template v-for="(item, i) in items">
          <v-layout
            row
            v-if="item.heading"
            align-center
            :key="i"
          >
            <v-flex xs6>
              <v-subheader v-if="item.heading">
                {{ item.heading }}
              </v-subheader>
            </v-flex>
            <v-flex xs6 class="text-xs-center">
              <a href="#!" class="body-2 black--text">EDIT</a>
            </v-flex>
          </v-layout>
          <v-list-group v-else-if="item.children" v-model="item.model" no-action :key="i">
            <v-list-tile slot="item" router :to="item.to">
              <v-list-tile-action>
                <v-icon>{{ item.model ? item.icon : item['icon-alt'] }}</v-icon>
              </v-list-tile-action>
              <v-list-tile-content>
                <v-list-tile-title>
                  {{ item.text }}
                </v-list-tile-title>
              </v-list-tile-content>
            </v-list-tile>
            <v-list-tile
              v-for="(child, i) in item.children"
              :key="i"
              router :to="item.to"
            >
              <v-list-tile-action v-if="child.icon">
                <v-icon>{{ child.icon }}</v-icon>
              </v-list-tile-action>
              <v-list-tile-content>
                <v-list-tile-title>
                  {{ child.text }}
                </v-list-tile-title>
              </v-list-tile-content>
            </v-list-tile>
          </v-list-group>
          <v-divider v-else-if="item.divider" :inset="item.inset" :key="i" class="my-4"></v-divider>
          <v-list-tile v-else router :to="item.to" :key="i">
            <v-list-tile-action>
              <v-icon>{{ item.icon }}</v-icon>
            </v-list-tile-action>
            <v-list-tile-content>
              <v-list-tile-title>
                {{ item.text }}
              </v-list-tile-title>
            </v-list-tile-content>
          </v-list-tile>
        </template>
        <v-list-tile @click="logout">
            <v-list-tile-action>
            <v-icon>exit_to_app</v-icon>
            </v-list-tile-action>
            <v-list-tile-content>
            <v-list-tile-title>
                Log Out
            </v-list-tile-title>
            </v-list-tile-content>
        </v-list-tile>
      </v-list>
    </v-navigation-drawer>
    <v-toolbar
      color="color1"
      dark
      app
      clipped-left
      fixed
    >
      <v-toolbar-title :style="$vuetify.breakpoint.smAndUp ? 'width: 300px; min-width: 250px' : 'min-width: 72px'" class="ml-0 pl-3">
        <v-toolbar-side-icon @click.stop="drawer = !drawer"></v-toolbar-side-icon>
        <router-link to="/" tag="span" class="hidden-xs-only" style="cursor: pointer">Volleyball Life</router-link>
      </v-toolbar-title>
      <div class="d-flex align-center" style="margin-left: auto">
        <v-badge color="red" overlap v-model="hasNotifications">
          <span slot="badge">3</span>
          <v-avatar class="color3">
            <v-icon>notifications</v-icon>
          </v-avatar>
        </v-badge>
        <v-menu bottom open-on-hover offset-y>
          <v-btn flat slot="activator">
            <v-icon class="mr-1">account_circle</v-icon>
            <span>{{ userFullName }}</span>
          </v-btn>
          <v-list>
            <v-list-tile v-for="item in myItems" :key="item.text" router :to="item.to">
              <v-list-tile-action>
                <v-icon>{{ item.icon }}</v-icon>
              </v-list-tile-action>
              <v-list-tile-content>
                <v-list-tile-title>
                  {{ item.text }}
                </v-list-tile-title>
              </v-list-tile-content>
            </v-list-tile>
            <v-list-tile @click="logout">
              <v-list-tile-action>
                <v-icon>exit_to_app</v-icon>
              </v-list-tile-action>
              <v-list-tile-content>
                <v-list-tile-title>
                  Log Out
                </v-list-tile-title>
              </v-list-tile-content>
            </v-list-tile>
          </v-list>
        </v-menu>
      </div>
    </v-toolbar>
  </div>
</template>

<script>
  import * as actions from '../../store/ActionTypes'

  export default {
    data: () => ({
      drawer: null,
      hasNotifications: false
    }),
    computed: {
      user () {
        return this.$store.getters.user
      },
      items () {
        return [
        { icon: 'people', text: 'Players', to: { name: 'players' } },
        { icon: 'format_list_numbered', text: 'Rankings', to: { name: 'rankings' } },
        { icon: 'date_range', text: 'Tournaments', to: { name: 'tournaments' } },
        { icon: 'apps', text: 'Organizations', to: { name: 'organizations' } },
        { divider: true },
        { icon: 'account_circle', text: this.userFullName, to: { name: 'me' } },
        { icon: 'date_range', text: 'My Tournaments', to: { name: 'my-tournaments' } }
        ]
      },
      myItems () {
        return [
          { icon: 'account_circle', text: 'My Profile', to: { name: 'me' } },
          { icon: 'date_range', text: 'My Tournaments', to: { name: 'my-tournaments' } }
        ]
      },
      userFullName () {
        return this.user.firstName + ' ' + this.user.lastName
      }
    },
    methods: {
      logout () {
        this.$store.dispatch(actions.LOGOUT).then(() => {
          this.$router.push({ name: 'signin' })
        })
      }
    }
  }
</script>