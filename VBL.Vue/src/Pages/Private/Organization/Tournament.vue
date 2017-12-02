<template>
  <v-container>
    <v-layout row wrap>
      <v-flex>
        <tournament-edit></tournament-edit>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import Edit from '../../../components/Tournament_Edit.vue'

export default {
  data () {
    return {
      tournament: null
    }
  },
  components: {
    'tournament-edit': Edit
  }
}
</script>

<style>

</style>
