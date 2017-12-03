<template>
    <v-layout row wrap>
        <v-flex>
            <v-form>
                <v-card>
                    <v-toolbar dark color="color2">
                        <v-text-field 
                            label="Name your tournament"
                            single-line
                            hide-details
                            v-model="tournament.name"
                            required
                        ></v-text-field>
                    </v-toolbar>
                    <v-data-table
                      :headers="tournament.divisionHeaders"
                      :items="tournament.divisions"
                      hide-actions
                    >
                      <template slot="items" slot-scope="props">
                        <td>{{ props.item.ageName}}</td>
                        <td>{{ props.item.ageName}}</td>
                        <td>{{ props.item.ageName}}</td>
                        <td>{{ props.item.ageName}}</td>
                      </template>
                      <template slot="no-data">
                        <v-alert :value="true" color="error" icon="warning">
                          Sorry, nothing to display here :(
                        </v-alert>
                      </template>
                    </v-data-table>
                    <v-btn @click="addDivision" :disabled="loading">
                        Add Division
                    </v-btn>
                </v-card>
            </v-form>
        </v-flex>
        <division-edit 
            :selectedDivision="selectedDivision" 
            :dialog="divisionDialog" 
            v-if="selectedDivision"
            @cancel="cancelAddDivision"
            @save="saveDivision"
        ></division-edit>
    </v-layout>
</template>

<script>
import * as actions from '../../store/ActionTypes'
import Tournament from '../../classes/Tournament'
import Division from '../../classes/TournamentDivision'
import DivisionEdit from '../../components/Tournament/Division.vue'

export default {
  data () {
    return {
      tournament: null,
      selectedDivision: null,
      divisionDialog: false
    }
  },
  computed: {
    loading () {
      return this.$store.getters.loading
    }
  },
  methods: {
    addDivision () {
      this.selectedDivision = {
        index: -1,
        division: new Division()
      }
      this.divisionDialog = true
    },
    cancelAddDivision () {
      this.selectedDivision = null
      this.divisionDialog = false
    },
    saveDivision () {
      if (this.selectedDivision.index === -1) {
        this.tournament.divisions.push(this.selectedDivision.division)
      } else {
        this.tournament.divisions[this.selectedDivision.index] = this.selectedDivision.division
      }
      this.selectedDivision = null
    }
  },
  components: {
    'division-edit': DivisionEdit
  },
  created () {
    if (!this.ageTypeOptions) {
      this.$store.dispatch(actions.LOAD_SELECT_OPTIONS)
    }
    this.tournament = new Tournament()
  }
}
</script>

<style>

</style>
