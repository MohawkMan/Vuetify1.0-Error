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
              <tourney-list :tourneys="tourneys" :loading="loadingList" :mode="mode"></tourney-list>
            </v-layout>
          </v-container>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import vbl from '../../VolleyballLife'
import TourneyList from '../../components/Tournament/TournamentList.vue'
import Tourney from '../../classes/Tournament'
import * as actions from '../../store/ActionTypes'
import { mapGetters } from 'vuex'

export default {
  props: ['username'],
  data () {
    return {
      loadingList: true,
      tourneys: []
    }
  },
  computed: {
    ...mapGetters([
      'user'
    ]),
    pageInfo () {
      if (!this.username) return null

      return this.user.pages.find((page) => {
        return page.userName === this.username
      })
    },
    mode () {
      if (!this.username) return 'public'
      return 'admin'
    },
    fetchUrl () {
      if (!this.username) return vbl.tournament.getAll

      return vbl.tournament.getByOrganizationId(this.pageInfo.id)
    }
  },
  methods: {
    fetchList () {
      this.loadingList = true
      this.axios.get(this.fetchUrl)
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
  watch: {
    '$route' (to, from) {
      this.fetchList()
    }
  },
  created () {
    this.$store.dispatch(actions.LOAD_SELECT_OPTIONS)
    this.fetchList()
  }
}
</script>

<style>

</style>
