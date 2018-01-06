<template>
  <v-container fill-height v-if="tournamentListLoading">
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
  <v-container v-else>
    <v-layout row wrap>
      <v-flex xs12 sm10 offset-sm1>
        <v-card>
          <v-toolbar color="color2" dark>
            <v-toolbar-title>{{pageInfo.name}} Results</v-toolbar-title>
          </v-toolbar>
          <v-card-text>
            <v-stepper v-model="currentStep" vertical>

              <v-stepper-step step="1" editable>
                Select a tournament
              </v-stepper-step>
              <v-stepper-content step="1">
                <v-card color="grey lighten-4" class="mb-5">
                  <v-card-text>

                    <v-data-table
                      :headers="headers"
                      :items="rows"
                      >
                      <template slot="items" slot-scope="props">
                        <tr style="cursor: pointer" :active="isSelected(props.item.id)" :class="isSelected(props.item.id) ? 'selected' : ''">
                          <td>{{ props.item.date | formatDate }}</td>
                          <td>{{props.item.name}}</td>
                          <td >{{ props.item.locations }}</td>
                          <td>
                            <v-btn
                              v-if="!isSelected(props.item.id)"
                              @click="select(props.item.id)" 
                              small 
                              color="color3" 
                              dark>
                              Select
                            </v-btn>
                          </td>
                        </tr>
                      </template>
                    </v-data-table>

                  </v-card-text>
                </v-card>
                <v-container fluid grid-list-md>
                  <v-layout justify-space-between>
                    <v-btn
                      :disabled="!selectedTourney" 
                      color="color3 white--text" 
                      @click.native="next" 
                    >Continue</v-btn>
                    <v-btn 
                      icon
                      v-if="selectedTourney"
                      @click="uploadDialog=true"
                    >
                      <v-icon color="color3">file_upload</v-icon>
                    </v-btn>
                  </v-layout>
                </v-container>
              </v-stepper-content>

              <v-stepper-step step="2" :editable="!!selectedTourney">
                Enter Results
              </v-stepper-step>
              <v-stepper-content step="2">
                <v-card color="grey lighten-4" class="mb-5">
                  <v-card-text>
                    Here

                  </v-card-text>
                </v-card>
                <v-btn
                  color="color3 white--text" 
                  @click.native="next" 
                >Continue</v-btn>
                <v-btn
                  flat
                  @click.native="back" 
                >Back</v-btn>
              </v-stepper-content>


            </v-stepper>
          </v-card-text>
        </v-card>
      </v-flex>
    </v-layout>
    <v-dialog v-model="noTeamWarning" max-width="500px" persistent>
      <v-card>
        <v-toolbar color="yellow darken-3" dark>
          <v-toolbar-title>
            <v-icon x-large>warning</v-icon>
            No Teams
          </v-toolbar-title>
        </v-toolbar>
        <v-card-text class="text-xs-center">
          
          <h2>
            The tournament you selected does not have any registered teams to record results for.<br>
            Would you like to:
          </h2>
          <v-btn @click="upload">
            Upload Registered Teams
          </v-btn>
          <v-btn @click="clear">
            Choose another Tournament
          </v-btn>
        </v-card-text>
      </v-card>
    </v-dialog>
    <results-uploader :open="uploadDialog" @close="uploadDialog = false"></results-uploader>
  </v-container>
</template>

<script>
import * as actions from '../../../store/ActionTypes'
import { mapGetters } from 'vuex'
import moment from 'moment'
import Uploader from '../../../components/Tournament/RegistrationUploader.vue'

export default {
  props: ['username'],
  data () {
    return {
      currentStep: 1,
      selectedTourney: null,
      uploadDialog: false,
      noTeamWarning: false
    }
  },
  computed: {
    ...mapGetters([
      'user',
      'tournamentList',
      'tournamentListLoading'
    ]),
    pageInfo () {
      return this.user.pages.find(page => {
        return page.username === this.username
      })
    },
    rows () {
      return this.tournamentList.map((t) => {
        return {
          id: t.id,
          date: t.startDate,
          name: t.name,
          locations: t.locationsString
        }
      })
    },
    headers () {
      return [
        {text: 'Date', value: 'date', align: 'left'},
        {text: 'Name', value: 'name', align: 'left'},
        {text: 'Location', value: 'locations', align: 'left'}
      ]
    }
  },
  methods: {
    next () {
      if (this.currentStep === 1 && this.selectedTourney.teamCount === 0) {
        this.noTeamWarning = true
        return
      }
      this.currentStep = (+this.currentStep) + 1
    },
    back () {
      this.currentStep = (+this.currentStep) - 1
    },
    select (id) {
      this.selectedTourney = this.tournamentList.find(t => {
        return t.id === id
      })
    },
    isSelected (id) {
      return this.selectedTourney && this.selectedTourney.id === id
    },
    clear () {
      this.selectedTourney = null
      this.noTeamWarning = false
    },
    upload () {
      this.noTeamWarning = false
      this.uploadDialog = true
    }
  },
  filters: {
    formatDate (date) {
      return moment(date).format('dddd, MMMM Do YYYY')
    }
  },
  components: {
    'results-uploader': Uploader
  },
  created () {
    console.log('calling created')
    if (!this.tournamentList) {
      this.$store.dispatch(actions.LOAD_SELECT_OPTIONS)
      this.$store.dispatch(actions.LOAD_TOURNAMENT_LIST, this.username)
    }
  }
}
</script>

<style scoped>
  .selected td {
    font-weight: bold !important
  }
</style>
