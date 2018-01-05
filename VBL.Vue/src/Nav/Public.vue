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
          <v-list-group v-else-if="item.children" v-model="item.model" no-action>
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
          <v-list-tile v-else router :to="item.to">
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
        <v-btn flat router to="/signin" active-class="color3--text">
          <v-icon class="mr-1">lock_open</v-icon>
          Sign In
        </v-btn>
        <v-btn v-if="false" flat router to="/join" active-class="color3--text">
          <v-icon class="mr-1">account_circle</v-icon>
          Join
        </v-btn>
      </div>
    </v-toolbar>
  </div>
</template>

<script>
  export default {
    data: () => ({
      drawer: null,
      items: [
        { icon: 'people', text: 'Players', to: { name: 'players' } },
        { icon: 'format_list_numbered', text: 'Rankings', to: { name: 'rankings' } },
        { icon: 'date_range', text: 'Tournaments', to: { name: 'tournaments' } },
        { icon: 'apps', text: 'Organizations', to: { name: 'organizations' } },
        { icon: 'lock_open', text: 'Sign In', to: { name: 'signin' } } // ,
        // { icon: 'account_circle', text: 'Join', to: { name: 'join' } }
      ]
    })
  }
</script>
