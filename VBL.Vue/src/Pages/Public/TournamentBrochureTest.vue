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
    <admin-speed-dial :currentTournament="tournament"></admin-speed-dial>
    <v-layout row v-if="userIsAdmin">
      <v-flex xs12 sm12 md10 offset-md1>
        <v-switch label="Published"
          v-model="tournament.isPublic"
          color="success"
          @click.stop="onPublish"
          hide-details></v-switch>
      </v-flex>
    </v-layout>
    <v-layout row v-if="!tournament.isPublic">
      <v-flex xs12 sm12 md10 offset-md1>
        <v-icon class="mr-1">visibility_off</v-icon>Only you can see this. This tournament is not yet published.
      </v-flex>
    </v-layout>
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
                <h2 v-html="tournament.name" :class="xsClass"></h2>
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
              {{tournament.locationsString}}
            </v-toolbar-title>
          </v-toolbar>
          <v-card v-else color="color4">
            <v-card-title>
              {{startDate}}
              <br>
              {{tournament.locationsString}}
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
              <v-tabs-item href="#information" ripple v-if="!complete">
                <v-icon>info_outline</v-icon>
                <span class="hidden-xs-only">Information</span>
              </v-tabs-item>
              <v-tabs-item href="#location" ripple v-if="!complete">
                <v-icon>location_on</v-icon>
                <span class="hidden-xs-only">Location</span>
              </v-tabs-item>
              <v-tabs-item href="#register" ripple v-if="tournament.regOpen">
                <v-icon>assignment_turned_in</v-icon>
                <span class="hidden-xs-only">Register</span>
              </v-tabs-item>
              <v-tabs-item href="#results" ripple v-if="!upcoming">
                <v-icon>group</v-icon>
                <span class="hidden-xs-only">Results</span>
              </v-tabs-item>
              <v-tabs-item href="#teams" ripple v-if="upcoming && userIsAdmin">
                <v-icon>group</v-icon>
                <span class="hidden-xs-only">Teams</span>
              </v-tabs-item>
            </v-tabs-bar>
            <v-tabs-items>
              <v-tabs-content id="information" v-if="!complete">
                <v-card>
                  <v-card-text v-html="tournament.description"></v-card-text>
                </v-card>
                <v-card>
                  <v-toolbar dense color="color5">
                    <v-toolbar-title>Divisions</v-toolbar-title>
                  </v-toolbar>
                  <division-list :divisions="tournament.divisions" @registerClick="register" mode="list"></division-list>
                </v-card>
              </v-tabs-content>
              <v-tabs-content id="location"  v-if="!complete">
                <v-card>
                  <v-card-title v-if="tournament.isOneLocation">
                    <iframe width="100%" height="450" frameborder="0" style="border:0" :src="tournament.divisions[0].location.googleUrl" allowfullscreen></iframe>
                  </v-card-title>
                </v-card>
              </v-tabs-content>
              <v-tabs-content id="register" v-if="tournament.regOpen">
                <v-card>
                  <tournament-registration
                    v-if="registering"
                    :tournament="tournament"
                    :registration="registration"
                    @registered="onRegistered"
                    @addedToCart="onAddToCart"
                  ></tournament-registration>
                </v-card>
              </v-tabs-content>
              <v-tabs-content id="results" v-if="!upcoming">
                <team-list-ex 
                  :divisions="tournament.divisionsWithTeams"
                  :expandId="6"
                   v-if="complete">
                </team-list-ex>
                  <v-container fill-height v-else>
                    <v-layout row wrap align-center>
                      <v-flex xs8 offset-xs2>
                        <v-layout row wrap text-xs-center>
                          <v-flex xs12>
                            <h3>Awaiting results</h3>
                          </v-flex>
                        </v-layout>
                      </v-flex>
                    </v-layout>
                  </v-container>
              </v-tabs-content>
              <v-tabs-content id="teams" v-if="upcoming && userIsAdmin">
                <v-card>
                  <v-toolbar color="color1" dark>
                    <v-toolbar-title>Registered Teams</v-toolbar-title>
                  </v-toolbar>
                  <v-card-text v-if="loadingTeams">
                    <v-container fill-height>
                      <v-layout row wrap align-center>
                        <v-flex xs8 offset-xs2>
                          <v-layout row wrap text-xs-center>
                            <v-flex xs12>
                              <h3>Updating Teams</h3>
                            </v-flex>
                            <v-flex xs12>
                              <v-progress-linear v-bind:indeterminate="true"></v-progress-linear>
                            </v-flex>
                          </v-layout>
                        </v-flex>
                      </v-layout>
                    </v-container>
                  </v-card-text>
                  <v-card-text v-else>
                    <team-list
                      v-for="(division, i) in tournament.divisionsWithTeams"
                      :key="i"
                      :division="division"
                      :toolbar="true"
                      type="registrations"
                    ></team-list>
                  </v-card-text>
                </v-card>
              </v-tabs-content>
            </v-tabs-items>
          </v-tabs>
        </v-card>
      </v-flex>
    </v-layout>
    <v-layout>
      <v-flex>{{xError}}</v-flex>
    </v-layout>
  </v-container>
</template>

<script>
// import vbl from '../../VolleyballLife'
import Tourney from '../../classes/Tournament'
import moment from 'moment'
import Divisions from '../../components/Tournament/DivisionList.vue'
import RegistrationUI from '../../components/Tournament/Registration.vue'
import TeamListExpansion from '../../components/Tournament/TeamListExpansion.vue'
import * as StatusEnum from '../../classes/TournamentStatus'
import EditorSimple from '../../components/Tournament/Edit/Simple.vue'
import RegistrationUploader from '../../components/Tournament/RegistrationUploader.vue'
import AdminSpeedDial from '../../components/AdminSpeedDial.vue'
import SDK from '../../VBL'
import TeamList from '../../components/Tournament/TeamList.vue'

export default {
  props: ['tournamentId', 'username', 'mode'],
  data () {
    return {
      fab: null,
      editDialog: false,
      uploadDialog: false,
      tournament: null,
      registering: true,
      registration: null,
      loading: true,
      activeTab: null,
      publish: false,
      loadingTeams: false,
      xError: 'None'
    }
  },
  computed: {
    userIsAdmin () {
      return this.$store.getters.user && this.$store.getters.user.isPageAdmin(this.username)
    },
    divisionsWithTeams () {
      return this.tournament.divisions.filter(division => division.teams.length > 0)
    },
    xsClass () {
      return {
        xs: this.$vuetify.breakpoint.xs
      }
    },
    startDate () {
      return moment(this.tournament.startDate).format('dddd, MMMM Do YYYY')
    },
    complete () {
      return this.tournament.statusId === StatusEnum.COMPLETE
    },
    upcoming () {
      return this.tournament.dateStatus === StatusEnum.UPCOMING
    }
  },
  methods: {
    fetchTourney () {
      if (this.$store.getters.tournamentList) {
        let t = this.$store.getters.getTournamentById(this.tournamentId)
        if (t) {
          this.setTourney(t)
          this.loading = false
          return
        }
      }
      this.loading = true
      const sdk = new SDK(this.axios)
      console.log('here')
      sdk.tournament.getTournamentById(this.tournamentId)
        .then((response) => {
          this.setTourney(response.data)
          this.loading = false
        })
        .catch((response) => {
          console.log('Error fetchTourney')
          console.log(response)
          this.loading = false
        })
    },
    setTourney (dto) {
      this.tournament = new Tourney(dto)
      this.registration = this.tournament.newRegistration()
      if (this.complete) {
        this.activeTab = 'results'
      } else {
        this.fetchTeams()
      }
    },
    register (division) {
      this.registration.setDivision(division)
      this.activeTab = 'register'
    },
    onRegistered (dto) {
      this.setTourney(dto)
      this.activeTab = 'information'
    },
    onAddToCart () {
      this.registration = this.tournament.newRegistration()
    },
    onPublish () {
      const sdk = new SDK(this.axios)
      sdk.tournament.publish(this.tournament.id, !this.tournament.isPublic)
        .then((response) => {
          this.tournament.isPublic = response.data
        })
        .catch((error) => {
          console.log(error)
        })
    },
    fetchTeams () {
      this.loadingTeams = true
      const sdk = new SDK(this.axios)
      sdk.tournament.getSeededTeams(this.tournamentId)
        .then((response) => {
          console.log(response.data)
          this.tournament.updateTeams(response.data)
          this.loadingTeams = false
        })
        .catch((error) => {
          console.log(error)
          this.loadingTeams = false
        })
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
    'team-list-ex': TeamListExpansion,
    'simple-editor': EditorSimple,
    'registration-uploader': RegistrationUploader,
    'admin-speed-dial': AdminSpeedDial,
    'team-list': TeamList
  },
  watch: {
    '$route.params': {
      handler: function () {
        this.fetchTourney()
      },
      deep: true
    },
    activeTab: function (newTab, oldTab) {
      if (oldTab) {
        this.$ga.page(`${this.$route.name}#${newTab}`)
      }
    }
  },
  created () {
    try {
      console.log('Calling fetch tournament')
      this.fetchTourney()
      if (this.mode) {
        this.activeTab = this.mode
      }
      if (this.complete) this.activeTab = 'results'
    } catch (error) {
      this.xError = error
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
