<template>
  <v-container>
    <v-layout row>
      <v-flex xs12 sm10 offset-sm1>
        <v-card>
          <v-toolbar dark color="color2">
            <v-toolbar-title v-if="pageInfo">{{ pageInfo.name }}</v-toolbar-title>
          </v-toolbar>
          <v-container grid-list-lg>
            <v-layout row wrap>
              <v-flex xs12>
                <v-card>
                  <v-toolbar dark color="color3" class="mx-auto">
                    <v-toolbar-title>You have tournaments that need results</v-toolbar-title>
                  </v-toolbar>
                  <v-container>
                    <v-layout>
                      <tourney-list :tourneys="tourneysNeedingResults" :loading="tournamentListLoading"></tourney-list>
                    </v-layout>
                  </v-container>
                </v-card>
              </v-flex>
            </v-layout>
            <v-layout row wrap>
              <v-flex xs12>
                <v-card color="color3" dark v-if="false">
                  <v-card-title>
                    <div>
                      <div class="headline">Next Tournament</div>
                      <div>Next Tournament Info</div>
                    </div>
                  </v-card-title>
                  <v-card-actions>
                    <v-btn flat dark>Manage</v-btn>
                  </v-card-actions>
                </v-card>
              </v-flex>
              <v-flex xs12>
                <v-btn to="tournaments/create">
                  Create New Tournament
                </v-btn>
              </v-flex>
            </v-layout>
          </v-container>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import TourneyList from '../../../components/Tournament/TournamentList.vue'
import { mapGetters } from 'vuex'

export default {
  props: ['username'],
  data () {
    return {

    }
  },
  computed: {
    ...mapGetters([
      'user',
      'loading',
      'getPageInfo',
      'needResults',
      'tournamentListLoading'
    ]),
    pageInfo () {
      return this.getPageInfo(this.username)
    },
    tourneysNeedingResults () {
      return this.needResults(this.username)
    }
  },
  components: {
    'tourney-list': TourneyList
  }
}
</script>

<style>
.headline {
    font-size: 24px!important;
    font-weight: 400;
    line-height: 32px!important;
    letter-spacing: normal!important;
}
</style>
