<template>
  <v-container grid-list-md>
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
              <tourney-list :tourneys="pastTourneys" :loading="tournamentListLoading" :page="pastPagination"></tourney-list>
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
  props: ['username'],
  data () {
    return {
      pastPagination: {
        sortBy: 'date',
        descending: true
      }
    }
  },
  computed: {
    ...mapGetters([
      'user',
      'tournamentListLoading',
      'runningTournaments',
      'upcomingTournaments',
      'pastTournaments'
    ]),
    runningTourneys () {
      return this.runningTournaments(this.username)
    },
    upcomingTourneys () {
      return this.upcomingTournaments(this.username)
    },
    pastTourneys () {
      return this.pastTournaments(this.username)
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
</script>

<style>

</style>
