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
                  ></tournament-registration>
                </v-card>
              </v-tabs-content>
              <v-tabs-content id="results" v-if="!upcoming">
                <team-list-ex 
                  :divisions="tournament.divisions"
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
            </v-tabs-items>
          </v-tabs>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
// import vbl from '../../VolleyballLife'
import Tourney from '../../classes/Tournament'
import moment from 'moment'
import Divisions from '../../components/Tournament/DivisionList.vue'
import RegistrationUI from '../../components/Tournament/Registration.vue'
import Teams from '../../components/Tournament/TeamListExpansion.vue'
import * as StatusEnum from '../../classes/TournamentStatus'
import EditorSimple from '../../components/Tournament/Edit/Simple.vue'
import * as actions from '../../store/ActionTypes'
import RegistrationUploader from '../../components/Tournament/RegistrationUploader.vue'
import AdminSpeedDial from '../../components/AdminSpeedDial.vue'
import SDK from '../../VBL'

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
      activeTab: null
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
      this.loading = true
      const sdk = new SDK(this.axios)
      sdk.tournament.getTournamentById(this.tournamentId)
      // this.axios.get(vbl.tournament.getById(this.tournamentId))
        .then((response) => {
          this.setTourney(response.data)
          if (this.complete) this.activeTab = 'results'
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
    },
    register (division) {
      this.registration.setDivision(division)
      this.activeTab = 'register'
    },
    onRegistered (dto) {
      this.setTourney(dto)
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
    'team-list-ex': Teams,
    'simple-editor': EditorSimple,
    'registration-uploader': RegistrationUploader,
    'admin-speed-dial': AdminSpeedDial
  },
  created () {
    console.log('Calling fetch tournament')
    this.$store.dispatch(actions.LOAD_SELECT_OPTIONS)
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
