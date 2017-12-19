<template>
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
  <v-container v-else grid-list-sm>
    <v-layout row>
      <v-flex xs12 sm12 md10 offset-md1>
        <v-card color="grey lighten-1">
          <!-- Pic and Title -->
          <v-card
            flat
            img="http://volleyoc.wdfiles.com/local--files/octoberseries/big%20TD%20meeting2.jpg"
            height="200px"
            class="pa-1"
          >
            <v-toolbar
              :floating="!$vuetify.breakpoint.xs"
              dense
              :class="xsClass"
              style="background-color: rgba(0, 0, 0, .7);"
            >
              <v-toolbar-title class="white--text">
                <h2 v-html="tourney.name":class="xsClass"></h2>
              </v-toolbar-title>
            </v-toolbar>
          </v-card>
          <!-- Date Bar -->
          <v-toolbar
            v-if="!$vuetify.breakpoint.xs"
            flat
            dense
            color="color4">
            <v-toolbar-title>
                {{startDate}}
            </v-toolbar-title>
            <v-spacer></v-spacer>
            <v-toolbar-title>
              {{tourney.locationsString}}
            </v-toolbar-title>
          </v-toolbar>
          <v-card v-else color="color4">
            <v-card-title>
              {{startDate}}
              <br>
              {{tourney.locationsString}}
            </v-card-title>
          </v-card>
          <!-- tabs -->
          <v-tabs 
            fixed
            :icons="!$vuetify.breakpoint.xs"
            centered
            v-model="activeTab"
          >
            <v-tabs-bar color="grey lighten-4">
              <v-tabs-slider color="color1"></v-tabs-slider>
              <v-tabs-item href="#information" ripple>
                <v-icon>info_outline</v-icon>
                <span class="hidden-xs-only">Information</span>
              </v-tabs-item>
              <v-tabs-item href="#location" ripple>
                <v-icon>location_on</v-icon>
                <span class="hidden-xs-only">Location</span>
              </v-tabs-item>
              <v-tabs-item href="#register" ripple>
                <v-icon>assignment_turned_in</v-icon>
                <span class="hidden-xs-only">Register</span>
              </v-tabs-item>
              <v-tabs-item href="#teams" ripple v-if="tourney.showTeams">
                <v-icon>group</v-icon>
                <span class="hidden-xs-only">Teams</span>
              </v-tabs-item>
            </v-tabs-bar>
            <v-tabs-items>
              <v-tabs-content id="information">
                <v-card>
                  <v-card-text>
                    <u>Age</u>
                    <p>All teams must compete in the age division they will compete in during summer 2018. Age cut-off is September 1st like SCVA and CBVA. Occasionally, exceptions can be made but teams out of age division will not receive playoff entry or points.</p>
                    <u>Format</u>
                    <p>Pool play (3, 4, and 5 team pools). Top 3 teams will advanced to single-elim playoffs.</p>
                    <u>Prizes</u>
                    <p><a href="http://jolynclothing.com/collections" target="_blank">Jolyn Swimsuits</a> for Open division winners, Paul Mitchell products for 1st and 2nd, Medals for 1st, 2nd, and 3rd place all divisions.</p>
                    <u>Weather Issues</u>
                    <p>Tournament will only be cancelled in the unlikely event of heavy rain, wind, and day-long unplayable conditions. Ed will email throughout the week with weather info if things are dicey, and the tournament may be altered to run more quickly if afternoon rain is imminent. If there is a window of playable weather on tournament day, we'll run during that window by altering start time.</p>
                  </v-card-text>
                </v-card>
                <v-card>
                  <v-toolbar dense color="color5">
                    <v-toolbar-title>Divisions</v-toolbar-title>
                  </v-toolbar>
                  <!--
                  -->
                  <division-list :divisions="tourney.divisions" @registerClick="register" mode="list"></division-list>
                </v-card>
              </v-tabs-content>
              <v-tabs-content id="location">
                <v-card>
                  <v-card-title>
                    <iframe width="100%" height="450" frameborder="0" style="border:0" src="https://www.google.com/maps/embed/v1/place?q=VolleyOC%20Courts%20At%20Newland%20St.&key=AIzaSyCwaRA4_FE4ZiG4I3WrEvZWpeW7Ro8gx0M" allowfullscreen></iframe>
                  </v-card-title>
                </v-card>
              </v-tabs-content>
              <v-tabs-content id="register">
                <v-card>
                  <tournament-registration
                    v-if="registering"
                    :tourney="tourney"
                    :registration="registration"
                    @registered="onRegistered"
                  ></tournament-registration>
                </v-card>
              </v-tabs-content>
              <v-tabs-content id="teams" v-if="tourney.showTeams">
                <team-list-ex 
                  :divisions="tourney.divisions"
                  :expandId="6">
                </team-list-ex>
              </v-tabs-content>
            </v-tabs-items>
          </v-tabs>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import vbl from '../../VolleyballLife'
import Tourney from '../../classes/Tournament'
import moment from 'moment'
import Divisions from '../../components/Tournament/DivisionList.vue'
import RegistrationUI from '../../components/Tournament/Registration.vue'
import Teams from '../../components/Tournament/TeamListExpansion.vue'
// import * as mutations from '../../store/MutationTypes'

export default {
  props: ['tournamentId', 'mode'],
  data () {
    return {
      tourney: null,
      registering: true,
      registration: null,
      loading: true,
      activeTab: null
    }
  },
  computed: {
    divisionsWithTeams () {
      return this.tourney.divisions.filter(division => division.teams.length > 0)
    },
    xsClass () {
      return {
        xs: this.$vuetify.breakpoint.xs
      }
    },
    startDate () {
      return moment(this.tourney.startDate).format('dddd, MMMM Do YYYY')
    //  return this.$vuetify.breakpoint.xs
    //  ? moment(this.tourney.startDate).format('MMM Do')
    //  : moment(this.tourney.startDate).format('dddd, MMMM Do YYYY')
    }
  },
  methods: {
    fetchTourney () {
      this.loading = true
      this.axios.get(vbl.tournament.getById(this.tournamentId))
        .then((response) => {
          this.tourney = new Tourney(response.data)
          this.registration = this.tourney.newRegistration()
          this.loading = false
        })
        .catch((response) => {
          console.log('Error fetchTourney')
          console.log(response)
          this.loading = false
        })
    },
    register (division) {
      this.registration.setDivision(division)
      this.activeTab = 'register'
    },
    onRegistered () {
      this.activeTab = 'information'
    }
  },
  filters: {
    formatDate (date) {
      return moment(date).format('dddd, MMMM Do YYYY')
    }
  },
  components: {
    'division-list': Divisions,
    'tournament-registration': RegistrationUI,
    'team-list-ex': Teams
  },
  created () {
    console.log('Calling fetch tourney')
    this.fetchTourney()
    if (this.mode) {
      this.activeTab = this.mode
    }
  }
}
</script>

<style scoped>
.titleCard {
  background-color: rgba(0, 0, 0, .5);
  color: white
}
.xs {
  font-size: small
}
</style>
