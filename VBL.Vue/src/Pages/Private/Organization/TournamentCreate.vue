<template>
  <v-container>
    <page-title :title="mode + ' Tournament'"></page-title>
    <v-layout row wrap>
      <v-flex>
        <tournament-edit 
          :tournament="tournament"></tournament-edit>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import Tournament from '../../../classes/Tournament'
import Edit from '../../../components/Tournament/Edit/Tournament.vue'
import { mapGetters } from 'vuex'

export default {
  props: ['username'],
  data () {
    return {
      tournament: null,
      mode: null
    }
  },
  computed: {
    ...mapGetters([
      'user',
      'loading',
      'selectedTourney'
    ]),
    pageInfo () {
      return this.user.pages.find((page) => {
        return page.userName === this.username
      })
    }
  },
  components: {
    'tournament-edit': Edit
  },
  created () {
    if (this.$route.name === 'tournament-edit') {
      this.mode = 'Edit'
      if (this.selectedTourney) {
        this.tournament = this.selectedTourney
      } else {
        // this.fetchTourney
      }
    } else {
      this.mode = 'Create New'
      this.tournament = new Tournament()
      this.tournament.organizationId = this.pageInfo.id
    }
  }
}
</script>

<style>

</style>
