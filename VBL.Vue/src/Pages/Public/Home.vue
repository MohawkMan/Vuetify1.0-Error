<template>
  <v-container grid-list-md>
    <v-layout row wrap>
      <v-flex xs12>
        <v-carousel>
          <v-carousel-item
            v-for="(item,i) in carouselItems"
            v-bind:key="i"
            v-bind:src="item.src"
            transition="fade"
            reverseTransition="fade"
          >
            <v-layout column align-center justify-center v-if="item.text">
              <div class="greybacked white--text pa-3 text-xs-center">
                <h1>{{item.headline}}</h1>
                <h4>{{item.subheadline}}</h4>
              </div>
            </v-layout>
          </v-carousel-item>
        </v-carousel>
      </v-flex>
    </v-layout>
    <v-layout row wrap>
      <v-flex xs12 text-xs-center class="display-4 color2--text">
        Welcome to Volleyball Life
      </v-flex>
    </v-layout>
    <v-layout row wrap>
      <v-flex xs12 sm10 offset-sm1>
        <v-card>
          <v-toolbar dark color="color2" class="mx-auto">
            <v-toolbar-title>Upcoming Tournaments</v-toolbar-title>
          </v-toolbar>
          <v-container>
            <v-layout>
              <tourney-list :tourneys="tourneys" :loading="loadingList" mode="public"></tourney-list>
            </v-layout>
          </v-container>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import * as actions from '../../store/ActionTypes'
import TourneyList from '../../components/Tournament/TournamentList.vue'
import vbl from '../../VolleyballLife'
import Tourney from '../../classes/Tournament'

export default {
  data: () => ({
    result: '',
    carouselItems: [
      {src: '/static/usc1.jpg', text: false, headline: 'Tournament Management', subheadline: ''},
      {src: '/static/stare3.jpg', text: false, headline: 'Player Rankings', subheadline: 'Nationwide point system'},
      {src: '/static/ucla1.jpg', text: false, headline: 'Instruction', subheadline: 'Clinics and lessons'},
      {src: '/static/sunset-volleyball2.jpg', text: false, headline: '', subheadline: ''}
    ],
    loadingList: true,
    tourneys: []
  }),
  computed: {
    isAuthenticated () {
      return this.$auth.isAuthenticated()
    }
  },
  methods: {
    fetch (endpoint) {
      this.axios.get('https://localhost:44351/api/Account/' + endpoint).then((resp) => {
        this.result = resp.data
      })
    },
    fetchList () {
      this.loadingList = true
      this.axios.get(vbl.tournament.getAll)
        .then((response) => {
          this.tourneys = response.data.map(item => new Tourney(item))
          this.loadingList = false
        })
        .catch((response) => {
          console.log(response.data)
          this.loadingList = false
        })
    },
    login () {
      let user = {
        username: '2146748568',
        password: 'volley13'
      }
      this.$store.dispatch(actions.LOGIN, { user })
    }
  },
  components: {
    'tourney-list': TourneyList
  },
  created () {
    this.$store.dispatch(actions.LOAD_SELECT_OPTIONS)
    this.fetchList()
  }
}
// Cap Lock: http://jsfiddle.net/Mottie/a6nhqvv0/
</script>

<style>
.greybacked {
  background-color: rgba(0,0,0,.5);
}
</style>