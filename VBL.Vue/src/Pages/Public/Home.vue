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
      <v-flex xs12 text-xs-center :class="xsClass" class="color2--text">
        Welcome to Volleyball Life
      </v-flex>
    </v-layout>
    <v-layout row wrap>
      <v-flex xs12 sm10 offset-sm1>
        <v-card>
          <v-toolbar dark color="color2" class="mx-auto">
            <v-toolbar-title>Today's Tournaments</v-toolbar-title>
          </v-toolbar>
          <v-container>
            <v-layout>
              <tourney-list :tourneys="runningTourneys" :loading="tournamentListLoading"></tourney-list>
            </v-layout>
          </v-container>
        </v-card>
      </v-flex>
    </v-layout>
    <v-layout row wrap>
      <v-flex xs12 sm10 offset-sm1>
        <v-card>
          <v-toolbar dark color="color3" class="mx-auto">
            <v-toolbar-title>Upcoming Tournaments</v-toolbar-title>
          </v-toolbar>
          <v-container>
            <v-layout>
              <tourney-list :tourneys="upcomingTourneys" :loading="tournamentListLoading"></tourney-list>
            </v-layout>
          </v-container>
        </v-card>
      </v-flex>
    </v-layout>
    <v-layout row wrap>
      <v-flex xs12 sm10 offset-sm1>
        <v-card>
          <v-toolbar dark color="color4" class="mx-auto">
            <v-toolbar-title>Previous Tournaments</v-toolbar-title>
          </v-toolbar>
          <v-container>
            <v-layout>
              <tourney-list :tourneys="pastTourneys" :loading="tournamentListLoading"></tourney-list>
            </v-layout>
          </v-container>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import TourneyList from '../../components/Tournament/TournamentList.vue'
import { mapGetters } from 'vuex'

export default {
  data: () => ({
    carouselItems: [
      {src: '/static/usc1.jpg', text: false, headline: 'Tournament Management', subheadline: ''},
      {src: '/static/stare3.jpg', text: false, headline: 'Player Rankings', subheadline: 'Nationwide point system'},
      {src: '/static/ucla1.jpg', text: false, headline: 'Instruction', subheadline: 'Clinics and lessons'},
      {src: '/static/sunset-volleyball2.jpg', text: false, headline: '', subheadline: ''}
    ]
  }),
  computed: {
    ...mapGetters([
      'user',
      'tournamentListLoading',
      'runningTournaments',
      'upcomingTournaments',
      'pastTournaments'
    ]),
    runningTourneys () {
      return this.runningTournaments()
    },
    upcomingTourneys () {
      return this.upcomingTournaments()
    },
    pastTourneys () {
      return this.pastTournaments()
    },
    xsClass () {
      return {
        'display-2': this.$vuetify.breakpoint.xs,
        'display-4': !this.$vuetify.breakpoint.xs
      }
    }
  },
  methods: {

  },
  components: {
    'tourney-list': TourneyList
  }
}
// Cap Lock: http://jsfiddle.net/Mottie/a6nhqvv0/
</script>

<style>
.greybacked {
  background-color: rgba(0,0,0,.5);
}
</style>