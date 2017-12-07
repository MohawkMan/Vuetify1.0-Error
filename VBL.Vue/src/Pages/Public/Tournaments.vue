<template>
  <v-container>
    <v-layout row>
      <v-flex xs12 sm10 offset-sm1>
        <v-card>
          <v-toolbar dark color="color2">
            <v-toolbar-title>Tournaments</v-toolbar-title>
          </v-toolbar>
          <v-container>
            <v-layout>
              <tourney-list :tourneys="tourneys" :loading="loadingList"></tourney-list>
            </v-layout>
          </v-container>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import vbl from '../../VolleyballLife'
import TourneyList from '../../components/Tournament/Public/TournamentList.vue'
import Tourney from '../../classes/Tournament'
import * as actions from '../../store/ActionTypes'

export default {
  data () {
    return {
      loadingList: true,
      tourneys: []
    }
  },
  methods: {
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
</script>

<style>

</style>
