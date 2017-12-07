<template>
  <v-container>
    <page-title title="Create New Tournament"></page-title>
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
      tournament: null
    }
  },
  computed: {
    ...mapGetters([
      'user',
      'loading'
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
    this.tournament = new Tournament()
    this.tournament.organizationId = this.pageInfo.id
  }
}
</script>

<style>

</style>
