<template>
  <v-container grid-list-md>
    <v-layout row wrap v-if="runningTournaments.length > 0">
      <v-flex xs12 sm10 offset-sm1>
        <v-card>
          <v-toolbar dark color="color2" class="mx-auto">
            <v-toolbar-title>Today's Tournaments</v-toolbar-title>
          </v-toolbar>
          <v-container>
            <v-layout>
              <tourney-list :tourneys="runningTournaments" :loading="loading"></tourney-list>
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
              <tourney-list :tourneys="upcomingTournaments" :loading="loading" :page="upcomingPagination"></tourney-list>
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
              <tourney-list :tourneys="pastTournaments" :loading="loading" :page="pastPagination"></tourney-list>
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
import SDK from '../../VBL'
import Summary from '../../classes/TournamentSummary'
import * as StatusEnum from '../../classes/TournamentStatus'

export default {
  props: ['username'],
  data () {
    return {
      loading: false,
      summaries: null,
      upcomingPagination: { sortBy: 'date', page: 1, rowsPerPage: 25, descending: false, totalItems: 0 },
      pastPagination: { sortBy: 'date', page: 1, rowsPerPage: 25, descending: true, totalItems: 0 }
    }
  },
  computed: {
    ...mapGetters([
      'user'
    ]),
    pastTournaments () {
      return this.filterList(StatusEnum.PAST)
    },
    runningTournaments () {
      return this.filterList(StatusEnum.INPROCESS)
    },
    upcomingTournaments () {
      return this.filterList(StatusEnum.UPCOMING)
    },
    xsClass () {
      return {
        'display-2': this.$vuetify.breakpoint.xs,
        'display-4': !this.$vuetify.breakpoint.xs
      }
    }
  },
  methods: {
    getList () {
      const sdk = new SDK(this.axios)
      this.loading = true
      sdk.tournament.getAllSummaries()
        .then((response) => {
          this.summaries = response.data.map(s => new Summary(s))
          this.loading = false
        })
        .catch((error) => {
          console.log(error)
          this.loading = false
        })
    },
    filterList (status) {
      if (this.username) return this.summaries && this.summaries.filter(t => t.dateStatus === status && t.organization.username === this.username)

      return this.summaries && this.summaries.filter(t => t.dateStatus === status)
    }
  },
  components: {
    'tourney-list': TourneyList
  },
  created () {
    this.getList()
  }
}
</script>

<style>

</style>
