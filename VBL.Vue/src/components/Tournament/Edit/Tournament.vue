<template>
  <v-container>
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
                    :busy="saving"
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
                    :loading="saving"
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
import * as actions from '../../../store/ActionTypes'
import vbl from '../../../VolleyballLife'

export default {
  props: ['tournament'],
  data () {
    return {
      saving: false
    }
  },
  computed: {
    loading () {
      return this.$store.getters.loading
    }
  },
  methods: {
    save () {
      this.saving = true
      console.log(JSON.stringify(this.tournament))
      this.axios.put(vbl.tournament.create, this.tournament)
        .then((response) => {
          console.log(response.data)
          this.saving = false
        })
        .catch((response) => {
          console.log(`Error => response: {response.data}`)
          this.saving = false
        })
    }
  },
  components: {
    'division-list': DivisionList
  },
  created () {
    this.$store.dispatch(actions.LOAD_SELECT_OPTIONS)
  }
}
</script>

<style>

</style>
