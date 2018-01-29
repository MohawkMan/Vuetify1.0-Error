<template>
  <v-dialog v-model="open" fullscreen transition="dialog-bottom-transition" :overlay="false">
    <v-card>
      <v-toolbar color="color2" dark>
        <v-toolbar-title>Tournament Edit</v-toolbar-title>
        <v-spacer></v-spacer>
        <v-btn icon @click.native="closeMe">
          <v-icon>close</v-icon>
        </v-btn>        
      </v-toolbar>
      <v-card-text v-if="tournament">
        <v-container grid-list-md>
          <v-layout row wrap>
            <v-flex xs12 sm10 offset-sm1>
              <!-- ORGANIZATION -->
              <v-card v-if="organizations.length > 1">
                <v-toolbar dense flat>
                  <v-toolbar-title>
                    Host
                  </v-toolbar-title>
                </v-toolbar>
                <v-card-text>
                  <v-container grid-list-md>
                    <v-layout row wrap>
                      <v-flex xs12 sm6>
                        <v-select
                          :items="organizations"
                          v-model="tournament.organization"
                          label="Choose a host"
                          autocomplete
                          item-text="name"
                          item-value="id"
                          required
                          return-object
                        ></v-select>
                      </v-flex>
                    </v-layout>
                  </v-container>
                </v-card-text>
              </v-card>
              <!-- DETAILS -->
              <v-card>
                <v-toolbar dense flat>
                  <v-toolbar-title>
                    Details
                  </v-toolbar-title>
                </v-toolbar>
                <v-card-text>
                  <v-container grid-list-md>
                    <!-- NAME -->
                    <v-layout row wrap>
                      <v-flex xs12 sm6>
                        <v-text-field
                          label="Tournament Name"
                          required
                          v-model="tournament.name"
                        ></v-text-field>
                      </v-flex>
                      <v-flex xs12 sm2 offset-sm4 v-if="false">
                        <v-switch label="Advanced" v-model="advanced" light hide-details></v-switch>
                      </v-flex>
                    </v-layout>
                    <!-- DATE & TIMES (IF 1 DAY) -->
                    <v-layout row wrap v-if="tournament.divisionTemplate">
                      <!-- DATE -->
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
                            v-model="tournament.divisionTemplate.days[0].dateFormatted"
                            required
                            @blur="tournament.divisionTemplate.days[0].date = parseDate(tournament.divisionTemplate.days[0].dateFormatted)"
                          ></v-text-field>
                          <v-date-picker 
                            no-title
                            scrollable 
                            actions 
                            v-model="tournament.divisionTemplate.days[0].date"
                            @input="tournament.divisionTemplate.days[0].dateFormatted = formatDate($event)">
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
                      <!-- CHECKIN TIME -->
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
                            v-model="tournament.divisionTemplate.days[0].checkInTime"
                          ></v-text-field>
                          <v-time-picker actions v-model="tournament.divisionTemplate.days[0].checkInTime">
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
                      <!-- PLAY TIME -->
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
                            v-model="tournament.divisionTemplate.days[0].playTime"
                          ></v-text-field>
                          <v-time-picker actions v-model="tournament.divisionTemplate.days[0].playTime">
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
                    <!-- LOCATION -->
                    <v-layout row wrap v-if="tournament.isOneLocation">
                      <v-flex xs12 sm4>
                        <v-select
                          :items="locationOptions"
                          item-text="name"
                          item-value="id"
                          label="Location"
                          prepend-icon="place"
                          autocomplete
                          return-object
                          required
                          v-model="tournament.divisionTemplate.location"
                        ></v-select>
                      </v-flex>
                    </v-layout>
                  </v-container>
                </v-card-text>
              </v-card>
              <!-- REGISTRATION -->
              <v-card>
                <v-toolbar dense flat>
                  <v-toolbar-title>
                    Registration
                  </v-toolbar-title>
                </v-toolbar>
                <template v-for="(item,i) in tournament.divisionTemplate.registrationWindows" >
                  <registration-window 
                    :key="i" 
                    :window="item"
                    :beforeDate="tournament.divisionTemplate.days[0].date"></registration-window>
                  </template>
              </v-card>
              <!-- REGISTRATION FIELDS -->
              <v-card>
                <v-toolbar dense flat>
                  <v-toolbar-title>
                    Registration Fields
                  </v-toolbar-title>
                </v-toolbar>
                <v-card-text>
                  <v-container grid-list-md>
                    <v-layout row wrap>
                      <v-flex xs12>
                        <h2>What fields would you like to collect during registration?</h2>
                      </v-flex>
                    </v-layout>
                    <v-layout row wrap>
                      <v-flex xs6 sm3 md2>
                        First Name
                        <v-radio-group v-model="forcedRadio" column>
                          <v-radio label="Ask" value="fields" hide-details disabled></v-radio>
                          <v-radio label="Require" value="requiredFields" hide-details disabled></v-radio>
                          <v-radio label="Skip" value="" hide-details disabled></v-radio>
                        </v-radio-group>
                      </v-flex>
                      <v-flex xs6 sm3 md2>
                        Last Name
                        <v-radio-group v-model="forcedRadio" column>
                          <v-radio label="Ask" value="fields" hide-details disabled></v-radio>
                          <v-radio label="Require" value="requiredFields" hide-details disabled></v-radio>
                          <v-radio label="Skip" value="" hide-details disabled></v-radio>
                        </v-radio-group>
                      </v-flex>
                      <v-flex xs6 sm3 md2 v-for="(field, i) in fieldChoices" :key="i">
                        {{field.label}}
                        <v-radio-group v-model="field.list" column>
                          <v-radio label="Ask" value="fields" hide-details></v-radio>
                          <v-radio label="Require" value="requiredFields" hide-details></v-radio>
                          <v-radio label="Skip" value="" hide-details></v-radio>
                        </v-radio-group>
                      </v-flex>
                    </v-layout>  
                  </v-container>
                </v-card-text>
              </v-card>
              <!-- ADDITIONAL INFO -->
              <v-card>
                <v-toolbar dense flat>
                  <v-toolbar-title>
                    Additional Info
                  </v-toolbar-title>
                </v-toolbar>
                <v-card-text>
                  <v-container grid-list-md>
                    <v-layout row wrap>
                      <v-flex xs12>
                        <v-text-field
                          full-width
                          multi-line
                          auto-grow
                          class="styledArea"
                          label="Additional Information i.e. prizes, play format, etc"
                          v-model="tournament.description"
                        ></v-text-field>
                      </v-flex>
                    </v-layout>
                  </v-container>
                </v-card-text>
              </v-card>
              <!-- DIVISIONS -->
              <v-card>
                <v-toolbar dense flat>
                  <v-toolbar-title>
                    Divisions
                  </v-toolbar-title>
                </v-toolbar>
                <v-card-text>
                  <division-list-simple
                    :tournament="tournament"
                    :busy="saving"
                  ></division-list-simple>
                </v-card-text>
              </v-card>
              <!-- BUTTONS -->
              <v-card>
                <v-card-text>
                  <v-container grid-list-md>
                    <v-layout row wrap justify-center>
                      <v-btn @click.stop="saveTournament">Save</v-btn>
                      <v-btn @click.stop="closeMe">Cancel</v-btn>
                    </v-layout>
                  </v-container>
                </v-card-text>
              </v-card>
            </v-flex>
          </v-layout>
        </v-container>
      </v-card-text>
    </v-card>
  </v-dialog>
</template>

<script>
import Tournament from '../../../classes/Tournament'
import { mapGetters } from 'vuex'
import RegWindow from './RegistrationWindow.vue'
import DivisionListSimple from './DivisionListSimple.vue'
import SDK from '../../../VBL'

export default {
  props: ['tournamentDto', 'open'],
  data () {
    return {
      advanced: false,
      date: false,
      checkin: false,
      play: false,
      saving: false,
      tournament: null,
      forcedRadio: 'requiredFields',
      fieldChoices: [
        { label: 'Phone', value: 'phone', list: '' },
        { label: 'Email', value: 'email', list: '' },
        { label: 'City & State', value: 'cityState', list: '' },
        { label: 'Date of birth', value: 'dob', list: '' },
        { label: 'Club', value: 'club', list: '' },
        { label: 'AAU', value: 'aau', list: '' },
        { label: 'AVP', value: 'avp', list: '' },
        { label: 'USAV', value: 'usav', list: '' },
        { label: 'CBVA', value: 'cbva', list: '' }
      ]
    }
  },
  computed: {
    ...mapGetters([
      'ageTypeOptions',
      'genderOptions',
      'divisionOptions',
      'locationOptions',
      'user'
    ]),
    dayCount () {
      return this.tournament.dayCount
    },
    organizations () {
      return this.user.pages.map((p) => {
        return {
          id: p.id,
          name: p.name,
          username: p.username
        }
      })
    }
  },
  methods: {
    loadTournament () {
      this.tournament = new Tournament(this.tournamentDto)
      if (this.organizations.length === 1) {
        this.tournament.organization = this.organizations[0]
      }

      if (this.tournament.divisionTemplate && this.tournament.divisionTemplate.registrationFields) {
        this.tournament.divisionTemplate.registrationFields.fields.forEach((f) => {
          let item = this.fieldChoices.find((c) => {
            return c.value === f
          })
          item.list = 'fields'
        })
        this.tournament.divisionTemplate.registrationFields.requiredFields.forEach((f) => {
          let item = this.fieldChoices.find((c) => {
            return c.value === f
          })
          item.list = 'requiredFields'
        })
      }
    },
    clearTournament () {
      this.tournament = null
    },
    saveTournament () {
      const sdk = new SDK(this.axios)
      sdk.tournament.save(this.tournament)
        .then((response) => {
          console.log('Save Success')
        })
        .catch((error) => {
          console.log('Save Error')
          console.log(error)
        })
    },
    formatDate (date) {
      if (!date) return null

      const [year, month, day] = date.split('-')
      return `${month}/${day}/${year}`
    },
    parseDate (date) {
      if (!date) return null

      const [month, day, year] = date.split('/')
      return `${year}-${month.padStart(2, '0')}-${day.padStart(2, '0')}`
    },
    closeMe () {
      this.$emit('closeClick')
    }
  },
  watch: {
    open (newVal, oldVal) {
      if (newVal) {
        clearTimeout()
        this.loadTournament()
      } else {
        setTimeout(this.clearTournament, 300)
      }
    },
    'tournament.divisionTemplate': {
      handler (newVal, oldVal) {
        if (newVal && oldVal) {
          this.tournament.cascadeTemplateChange()
        }
      },
      deep: true
    },
    'tournament.organization': {
      handler (newVal, oldVal) {
        if (newVal) {
          this.tournament.organizationId = newVal.id
        }
      }
    },
    fieldChoices: {
      handler (newVal, oldVal) {
        this.tournament.divisionTemplate.registrationFields.fields = newVal.filter((f) => {
          return f.list === 'fields'
        }).map((m) => {
          return m.value
        })
        this.tournament.divisionTemplate.registrationFields.requiredFields = newVal.filter((f) => {
          return f.list === 'requiredFields'
        }).map((m) => {
          return m.value
        })
      },
      deep: true
    }
  },
  components: {
    'registration-window': RegWindow,
    'division-list-simple': DivisionListSimple
  }
}
</script>

<style>
  .styledArea textarea {
    border-bottom: 2px solid #ccc;
  }
  .styledArea textarea:focus {
    border-bottom: 2px solid rgb(21, 42, 105);
  }
</style>
