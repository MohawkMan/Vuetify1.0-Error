<template>
  <v-container>
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

export default {
  data () {
    return {
      tournament: null
    }
  },
  components: {
    'tournament-edit': Edit
  },
  created () {
    this.tournament = new Tournament()
  }
}
</script>

<style>

</style>
