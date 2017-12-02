<template>
  <v-dialog v-model="dialog" fullscreen scrollable transition="dialog-bottom-transition" overlay="false">
    <v-card>
      <v-toolbar dark color="color2">
        <v-btn icon @click.native="cancel" dark small color="red darken-3">
          <v-icon>close</v-icon>
        </v-btn>        
        <v-toolbar-title>{{title}}</v-toolbar-title>
        <v-spacer></v-spacer>
        <v-toolbar-items>
          <v-btn dark flat @click.native="save">
            <v-icon class="mr-2">save</v-icon>
            Save
          </v-btn>
        </v-toolbar-items>
      </v-toolbar>
      <v-card-text>
        <v-form>
          <v-container grid-list-lg>
            <v-layout row wrap>
              <v-flex xs12 sm10 offset-sm1>
                <v-card>
                  <v-toolbar dense flat>
                    <v-toolbar-title>
                      What?
                    </v-toolbar-title>
                  </v-toolbar>
                  <v-container>
                    <v-layout row wrap>
                      <v-flex xs6 sm4>
                        <v-select
                          v-model="division.ageTypeId"
                          :items="ageTypeOptions"
                          item-text="name"
                          item-value="id"
                          label="Age Group"
                          autocomplete
                        >
                        </v-select>
                      </v-flex>
                      <v-flex xs6 sm4>
                        <v-select
                          v-model="division.genderId"
                          :items="genderOptions"
                          item-text="name"
                          item-value="id"
                          label="Gender"
                          autocomplete
                        >
                        </v-select>
                      </v-flex>
                      <v-flex xs6 sm4>
                        <v-select
                          v-model="division.divisionId"
                          :items="divisionOptions"
                          item-text="name"
                          item-value="id"
                          label="Division"
                          autocomplete
                        >
                        </v-select>
                      </v-flex>
                    </v-layout>
                  </v-container>
                </v-card>
                
                <v-card>
                  <v-toolbar dense flat>
                    <v-toolbar-title>
                      When?
                    </v-toolbar-title>
                  </v-toolbar>
                  <v-container>
                    <v-layout row wrap>
                      <v-flex xs12 sm4>
                        <v-menu
                          lazy
                          :close-on-content-click="false"
                          v-model="date"
                          transition="scale-transition"
                          offset-y
                          full-width
                          :nudge-right="40"
                          max-width="290px"
                          min-width="290px"
                        >
                          <v-text-field
                            slot="activator"
                            label="Tournament Date"
                            prepend-icon="event"
                            readonly
                            v-model="division.days[0].date"
                          ></v-text-field>
                          <v-date-picker no-title scrollable actions v-model="division.days[0].date">
                            <template slot-scope="{ save, cancel }">
                              <v-card-actions>
                                <v-spacer></v-spacer>
                                <v-btn flat color="primary" @click="cancel">Cancel</v-btn>
                                <v-btn flat color="primary" @click="save">OK</v-btn>
                              </v-card-actions>
                            </template>
                          </v-date-picker>
                        </v-menu>
                      </v-flex>
                      <v-flex xs6 sm4>
                        <v-menu
                          lazy
                          :close-on-content-click="false"
                          v-model="checkin"
                          transition="scale-transition"
                          offset-y
                          full-width
                          :nudge-right="40"
                          max-width="290px"
                          min-width="290px"
                        >
                          <v-text-field
                            slot="activator"
                            label="Check-in Time"
                            prepend-icon="access_time"
                            readonly
                            v-model="division.days[0].checkInTime"
                          ></v-text-field>
                          <v-time-picker actions v-model="division.days[0].checkInTime">
                            <template slot-scope="{ save, cancel }">
                              <v-card-actions>
                                <v-spacer></v-spacer>
                                <v-btn flat color="primary" @click="cancel">Cancel</v-btn>
                                <v-btn flat color="primary" @click="save">OK</v-btn>
                              </v-card-actions>
                            </template>
                          </v-time-picker>
                        </v-menu>
                      </v-flex>
                      <v-flex xs6 sm4>
                        <v-menu
                          lazy
                          :close-on-content-click="false"
                          v-model="play"
                          transition="scale-transition"
                          offset-y
                          full-width
                          :nudge-right="40"
                          max-width="290px"
                          min-width="290px"
                        >
                          <v-text-field
                            slot="activator"
                            label="Play Start Time"
                            prepend-icon="access_time"
                            readonly
                            v-model="division.days[0].playTime"
                          ></v-text-field>
                          <v-time-picker actions v-model="division.days[0].playTime">
                            <template slot-scope="{ save, cancel }">
                              <v-card-actions align-center justify-center>
                                <v-btn flat color="primary" @click="cancel">Cancel</v-btn>
                                <v-btn flat color="primary" @click="save">OK</v-btn>
                              </v-card-actions>
                            </template>
                          </v-time-picker>
                        </v-menu>
                      </v-flex>                   
                    </v-layout>
                  </v-container>
                </v-card>

                <v-card>
                  <v-toolbar dense flat>
                    <v-toolbar-title>
                      Where?
                    </v-toolbar-title>
                  </v-toolbar>
                  <v-container>
                    <v-layout row wrap>
                      <v-flex xs12 sm4>
                        <v-select
                          :items="locationOptions"
                          item-text="name"
                          item-value="id"
                          label="Location"
                          autocomplete
                          v-model="division.locationId"
                        ></v-select>
                      </v-flex>
                    </v-layout>
                  </v-container>
                </v-card>

                <v-card>
                  <v-toolbar dense flat>
                    <v-toolbar-title>
                      Registration
                    </v-toolbar-title>
                  </v-toolbar>
                  <v-container>
                    <v-layout row wrap>
                      <v-flex xs6 sm3>
                        <v-text-field
                          v-model="division.entryFee"
                          label="Entry Fee"
                          type="number"
                          prefix="$"
                          @blur="formatCurrency"
                        ></v-text-field>
                      </v-flex>
                      <v-flex xs6 sm3>
                        <v-text-field
                          :label="shortText ? 'Max teams' : 'Max number of teams'"
                          type="number"
                          v-model="division.maxTeams"
                        ></v-text-field>
                      </v-flex>
                    </v-layout>
                    <v-layout row wrap>
                      <v-flex xs6 sm3>
                        <v-menu
                          lazy
                          :close-on-content-click="false"
                          v-model="regStart"
                          transition="scale-transition"
                          offset-y
                          full-width
                          :nudge-right="40"
                          max-width="290px"
                          min-width="290px"
                          hide-details
                        >
                          <v-text-field
                            slot="activator"
                            label="Registration Open Date"
                            prepend-icon="event"
                            readonly
                            v-model="division.registrationStartDate"
                          ></v-text-field>
                          <v-date-picker no-title scrollable actions v-model="division.registrationStartDate">
                            <template slot-scope="{ save, cancel }">
                              <v-card-actions>
                                <v-spacer></v-spacer>
                                <v-btn flat color="primary" @click="cancel">Cancel</v-btn>
                                <v-btn flat color="primary" @click="save">OK</v-btn>
                              </v-card-actions>
                            </template>
                          </v-date-picker>
                        </v-menu>
                      </v-flex>
                      <v-flex xs6 sm3>
                        <v-menu
                          lazy
                          :close-on-content-click="false"
                          v-model="regStartT"
                          transition="scale-transition"
                          offset-y
                          full-width
                          :nudge-right="40"
                          max-width="290px"
                          min-width="290px"
                        >
                          <v-text-field
                            slot="activator"
                            label="Registration Open Time"
                            prepend-icon="access_time"
                            readonly
                            v-model="division.registrationStartTime"
                          ></v-text-field>
                          <v-time-picker actions v-model="division.registrationStartTime">
                            <template slot-scope="{ save, cancel }">
                              <v-card-actions>
                                <v-spacer></v-spacer>
                                <v-btn flat color="primary" @click="cancel">Cancel</v-btn>
                                <v-btn flat color="primary" @click="save">OK</v-btn>
                              </v-card-actions>
                            </template>
                          </v-time-picker>
                        </v-menu>
                      </v-flex>
                      <v-flex xs6 sm3>
                        <v-menu
                          lazy
                          :close-on-content-click="false"
                          v-model="regEnd"
                          transition="scale-transition"
                          offset-y
                          full-width
                          :nudge-right="40"
                          max-width="290px"
                          min-width="290px"
                          hide-details
                        >
                          <v-text-field
                            slot="activator"
                            label="Registration Close Date"
                            prepend-icon="event"
                            readonly
                            v-model="division.registrationEndDate"
                          ></v-text-field>
                          <v-date-picker no-title scrollable actions v-model="division.registrationEndDate">
                            <template slot-scope="{ save, cancel }">
                              <v-card-actions>
                                <v-spacer></v-spacer>
                                <v-btn flat color="primary" @click="cancel">Cancel</v-btn>
                                <v-btn flat color="primary" @click="save">OK</v-btn>
                              </v-card-actions>
                            </template>
                          </v-date-picker>
                        </v-menu>
                      </v-flex>
                      <v-flex xs6 sm3>
                        <v-menu
                          lazy
                          :close-on-content-click="false"
                          v-model="regStartT"
                          transition="scale-transition"
                          offset-y
                          full-width
                          :nudge-right="40"
                          max-width="290px"
                          min-width="290px"
                        >
                          <v-text-field
                            slot="activator"
                            label="Registration Close Time"
                            prepend-icon="access_time"
                            readonly
                            v-model="division.registrationEndTime"
                          ></v-text-field>
                          <v-time-picker actions v-model="division.registrationEndTime">
                            <template slot-scope="{ save, cancel }">
                              <v-card-actions>
                                <v-spacer></v-spacer>
                                <v-btn flat color="primary" @click="cancel">Cancel</v-btn>
                                <v-btn flat color="primary" @click="save">OK</v-btn>
                              </v-card-actions>
                            </template>
                          </v-time-picker>
                        </v-menu>
                      </v-flex>
                    </v-layout>
                    <v-layout row wrap>
                    </v-layout>                
                  </v-container>
                </v-card>

              </v-flex>
            </v-layout>
          </v-container>
        </v-form>
      </v-card-text>
    </v-card>
  </v-dialog>
</template>

<script>
import { mapGetters } from 'vuex'

export default {
  props: ['selectedDivision', 'dialog', 'mode'],
  data () {
    return {
      ageTypes: ['Adult', 'Juniors'],
      genders: [{
        text: 'Female',
        value: 0
      },
      {
        text: 'Male',
        value: 1
      }],
      divisions: ['12U', '14U'],
      locations: ['Huntington State Beach'],
      date: false,
      checkin: false,
      play: false,
      regStart: false,
      regStartT: false,
      regEnd: false,
      regEndT: false
    }
  },
  computed: {
    ...mapGetters([
      'ageTypeOptions',
      'genderOptions',
      'divisionOptions',
      'locationOptions'
    ]),
    division () {
      return this.selectedDivision.division
    },
    title () {
      return this.division && this.division.id > 0 ? 'Edit Division' : 'Add New Division'
    },
    shortText () {
      let bp = this.$vuetify.breakpoint.name
      if (bp === 'xs') {
        return true
      }
      return false
    }
  },
  methods: {
    save () {
      this.$emit('save')
    },
    cancel () {
      this.$emit('cancel')
    },
    formatCurrency () {
      this.division.entryFee = parseFloat(this.division.entryFee).toFixed(2)
    }
  }
}
</script>

<style>

</style>
