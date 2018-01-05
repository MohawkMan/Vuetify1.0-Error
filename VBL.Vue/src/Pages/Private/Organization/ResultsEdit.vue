<template>
  <v-container fill-height v-if="loadingList">
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
                      @click.native="currentStep += 1" 
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
                Verify Teams
              </v-stepper-step>
              <v-stepper-content step="2">
                <v-card color="grey lighten-4" class="mb-5">
                  <v-card-text>
                    Here

                  </v-card-text>
                </v-card>
                <v-btn
                  color="color3 white--text" 
                  @click.native="currentStep += 1" 
                >Continue</v-btn>
                <v-btn
                  flat
                  @click.native="currentStep -= 1" 
                >Back</v-btn>
              </v-stepper-content>


            </v-stepper>
          </v-card-text>
        </v-card>
      </v-flex>
    </v-layout>
    <results-uploader :open="uploadDialog"></results-uploader>
  </v-container>
</template>

<script>
import * as actions from '../../../store/ActionTypes'
import Tourney from '../../../classes/Tournament'
import vbl from '../../../VolleyballLife'
import { mapGetters } from 'vuex'
import moment from 'moment'
import Uploader from '../../../components/Tournament/ResultUploader.vue'

export default {
  props: ['username'],
  data () {
    return {
      loadingList: true,
      tourneys: [],
      currentStep: 1,
      selectedTourney: null,
      uploadDialog: false
    }
  },
  computed: {
    ...mapGetters([
      'user'
    ]),
    pageInfo () {
      return this.user.pages.find(page => {
        return page.username === this.username
      })
    },
    fetchUrl () {
      return vbl.tournament.getByOrganizationUserName(this.username)
    },
    rows () {
      return this.tourneys.map((t) => {
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
    fetchList () {
      this.loadingList = true
      this.axios
        .get(this.fetchUrl)
        .then(response => {
          this.tourneys = response.data.map(item => new Tourney(item))
          this.loadingList = false
        })
        .catch(response => {
          console.log(response.data)
          this.loadingList = false
        })
    },
    select (id) {
      this.selectedTourney = this.tourneys.find(t => {
        return t.id === id
      })
    },
    isSelected (id) {
      return this.selectedTourney && this.selectedTourney.id === id
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
    if (this.tourneys.length === 0) {
      this.$store.dispatch(actions.LOAD_SELECT_OPTIONS)
      this.fetchList()
    }
  }
}
</script>

<style scoped>
  .selected td {
    font-weight: bold !important
  }
</style>
