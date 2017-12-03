<template>
  <v-container>
    <v-layout row>
      <h2>Create a new tournament</h2>
    </v-layout>
    <v-layout row wrap>
      <v-flex>
        <v-form>
          <v-card>
            <v-toolbar dark color="color2">
                <v-text-field 
                    label="Tournament Name"
                    single-line
                    hide-details
                    v-model="tournament.name"
                    required
                ></v-text-field>
            </v-toolbar>
            <division-list
              :divisions="tournament.divisions"
            ></division-list>
          </v-card>
        </v-form>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import DivisionList from './DivisionList.vue'

export default {
  props: ['tournament'],
  data () {
    return {
    }
  },
  computed: {
    loading () {
      return this.$store.getters.loading
    }
  },
  methods: {
  },
  components: {
    'division-list': DivisionList
  }
}
</script>

<style>

</style>
