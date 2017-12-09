<template>
  <v-container v-if="!loading" grid-list-md>
    <v-layout row>
      <v-flex xs12 sm10 offset-sm1>
        <v-card>
          <v-container>
            <!-- Card 1 Title, Photo, Date -->
            <v-layout row wrap>
              <v-flex xs12>
                <v-card
                  flat
                  img="http://volleyoc.wdfiles.com/local--files/octoberseries/big%20TD%20meeting2.jpg"
                  height="200px"
                  class="pa-3"
                >
                  <v-toolbar
                    floating
                    dense prominent extended
                    style="background-color: rgba(0, 0, 0, .7);"
                  >
                    <v-toolbar-title class="white--text">
                      <h2>
                        <span class="blue--text">Volley</span><span class="orange--text">OC</span> {{tourney.name}}
                      </h2>
                    </v-toolbar-title>
                  </v-toolbar>
                </v-card>
                <v-card flat>
                  <v-toolbar flat dense color="color4">
                    <v-toolbar-title>
                        {{tourney.startDate | formatDate}}
                    </v-toolbar-title>
                    <v-spacer></v-spacer>
                    <v-toolbar-title>
                        {{tourney.locationsString}}
                    </v-toolbar-title>
                  </v-toolbar>
                </v-card>
              </v-flex>
            </v-layout>
            <!-- Card 2 -->
            <v-layout row wrap>
              <v-flex xs12>
                <v-card>
                  <v-card-title>
                    Hosted by: <router-link to="../" class="primary--text">{{tourney.organization.name}}</router-link>
                  </v-card-title>
                </v-card>
              </v-flex>
            </v-layout>
            <!-- Card 3 Divisions -->
            <v-layout row wrap>
              <v-flex xs12>
                <v-card>
                  <v-toolbar dense color="color5">
                    <v-toolbar-title>Divisions</v-toolbar-title>
                  </v-toolbar>
                  <division-list :divisions="tourney.divisions"></division-list>
                </v-card>
              </v-flex>
            </v-layout>
            <!-- Card 4 Registration -->
            <v-layout row wrap>
              <v-flex xs12>
                <v-card>
                  <tournament-registration
                    v-if="registering"
                    :tourney="tourney"
                    :registration="registration"
                    ></tournament-registration>
                </v-card>
              </v-flex>
            </v-layout>

          </v-container>

          <v-card-title>
            <div v-html="test"></div>
            <v-btn :to="{name: 'tournament-register', params: {tournamentId: tourney.id}}">
              Register
            </v-btn>
          </v-card-title>

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

export default {
  props: ['tournamentId', 'mode'],
  data () {
    return {
      tourney: null,
      registering: true,
      registration: null,
      loading: true,
      test: '<span class="red--text">Testing</span>'
    }
  },
  methods: {
    fetchTourney () {
      this.loading = true
      this.axios.get(vbl.tournament.getById(this.tournamentId))
        .then((response) => {
          this.tourney = new Tourney(response.data)
          this.registration = this.tourney.newRegistration()
          this.registration.setDivision(this.tourney.divisions[1])

          this.loading = false
        })
        .catch((response) => {
          console.log('Error fetchTourney')
          console.log(response)
          this.loading = false
        })
    },
    register (divisionId) {

    }
  },
  filters: {
    formatDate (date) {
      return moment(date).format('dddd, MMMM Do YYYY')
    }
  },
  components: {
    'division-list': Divisions,
    'tournament-registration': RegistrationUI
  },
  created () {
    this.fetchTourney()
  }
}
</script>

<style scoped>
.titleCard {
  background-color: rgba(0, 0, 0, .5);
  color: white
}
</style>
