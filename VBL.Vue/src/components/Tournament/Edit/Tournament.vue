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
            <v-container>
              <v-layout row wrap>
                <v-flex xs10 offset-xs1>
                  Start Date: {{tournament.startDate}}
                </v-flex>
              </v-layout>
              <v-layout row wrap>
                <v-flex xs10 offset-xs1>
                  <division-list
                    :divisions="tournament.divisions"
                  ></division-list>
                </v-flex>
              </v-layout>
              <v-layout row>
                <v-flex>
                  <v-btn
                    color="green --darken3"
                    dark
                    large
                    absolute
                    bottom
                    right
                    fab
                    @click="save"
                    >
                    <v-tooltip left>
                      <v-icon slot="activator">save</v-icon>
                      <span>Save</span>
                    </v-tooltip>
                  </v-btn>
                </v-flex>
              </v-layout>
            </v-container>
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
    save () {
      console.log(JSON.stringify(this.tournament))
    }
  },
  components: {
    'division-list': DivisionList
  }
}
</script>

<style>

</style>
