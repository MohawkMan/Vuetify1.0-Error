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
  <v-container v-else grid-list-md>
    <v-layout row wrap>
      <v-flex xs12 sm10 offset-sm1>
        <v-card>
          <v-toolbar dark color="color3" class="mx-auto">
            <v-toolbar-title>Current Player Rankings</v-toolbar-title>
          </v-toolbar>
          <ranking-list :players="players"></ranking-list>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>


<script>
import vbl from '../../VolleyballLife'
import RankingList from '../../components/RankingsList.vue'

export default {
  data () {
    return {
      loading: false,
      players: null
    }
  },
  methods: {
    getPlayers () {
      this.loading = true
      this.axios.get(vbl.rankings.all)
        .then((response) => {
          this.players = response.data
          this.loading = false
        })
        .catch((error) => {
          console.log(error)
          this.loading = false
        })
    }
  },
  components: {
    'ranking-list': RankingList
  },
  created () {
    this.getPlayers()
  }
}
</script>

<style>

</style>
