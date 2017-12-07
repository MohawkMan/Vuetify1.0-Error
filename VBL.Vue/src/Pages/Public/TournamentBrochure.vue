<template>
  <v-container v-if="!loading">
    <v-layout row>
      <v-flex xs12 sm10 offset-sm1>
        <v-card>
          <v-card-media
            class="white--text"
            height='200px'
            src="http://volleyoc.wdfiles.com/local--files/octoberseries/big%20TD%20meeting2.jpg"
          >
            <v-container fill-height fluid>
              <v-layout>
                <v-flex xs12 md6>
                  <div class="titleCard">
                    <h1><span class="blue--text">Volley</span><span class="orange--text">OC</span> {{tourney.name}}</h1>
                    <div>{{tourney.startDate | formatDate}}</div>
                  </div>
                </v-flex>  
              </v-layout>
            </v-container>
          </v-card-media>

          <v-card-title v-if="mode === 'view'">
            <v-btn :to="{name: 'tournament-register', params: {tournamentId: tourney.id}}">
              Register
            </v-btn>
          </v-card-title>
          <v-card-title v-else>
            <v-btn :to="{name: 'tournament-brochure', params: {tournamentId: tourney.id}}">
              Back
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

export default {
  props: ['tournamentId', 'mode'],
  data () {
    return {
      tourney: null,
      loading: true
    }
  },
  methods: {
    fetchTourney () {
      this.loading = true
      this.axios.get(vbl.tournament.getById(this.tournamentId))
        .then((response) => {
          this.tourney = new Tourney(response.data)
          this.loading = false
        })
        .catch((response) => {
          console.log(response.data)
          this.loading = false
        })
    }
  },
  filters: {
    formatDate (date) {
      return moment(date).format('dddd, MMMM Do YYYY')
    }
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
