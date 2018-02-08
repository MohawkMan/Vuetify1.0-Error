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
            <v-toolbar-title>Current AAU Player Rankings</v-toolbar-title>
            <v-spacer></v-spacer>
            <v-text-field
              append-icon="search"
              label="Search"
              single-line
              hide-details
              v-model="search"
            ></v-text-field>
          </v-toolbar>
          <v-tabs fixed centered v-model="activeTab">
            <v-tabs-bar color="grey lighten-4">
              <v-tabs-slider color="color3"></v-tabs-slider>
              <v-tabs-item href="#girls" ripple router>
                <span>Girls</span>
              </v-tabs-item>
              <v-tabs-item href="#boys" ripple router>
                <span>Boys</span>
              </v-tabs-item>
            </v-tabs-bar>
            <v-tabs-items>
              <v-tabs-content id="girls">
                <ranking-list :players="girls" :searchTerm="search"></ranking-list>
              </v-tabs-content>
              <v-tabs-content id="boys">
                <ranking-list :players="boys" :searchTerm="search"></ranking-list>
              </v-tabs-content>
            </v-tabs-items>
          </v-tabs>
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
      players: null,
      search: '',
      activeTab: null
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
  computed: {
    girls () {
      return this.players && this.players.filter((p) => {
        return !p.isMale
      })
    },
    boys () {
      return this.players && this.players.filter((p) => {
        return p.isMale
      })
    }
  },
  watch: {
    activeTab () {
      this.search = ''
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
