<template>
  <v-dialog
    v-model="open"
    v-if="tournament"
    fullscreen
    transition="dialog-bottom-transition"
    :overlay="false"
    scrollable
  >
    <v-card>
      <v-toolbar class="color2" dark>
        <v-btn 
          icon
          dark
          @click.native="onClose"
          :disabled="busy"
        >
          <v-icon>close</v-icon>
        </v-btn>
        <v-toolbar-title>Result Upload</v-toolbar-title>
      </v-toolbar>
      <v-card-text>
        <v-card>
          <v-card-text>
            <h1>{{tournament.name}}</h1>
            <v-btn @click.stop="workingDialog = true" v-if="false">
              Test
            </v-btn>
          </v-card-text>
        </v-card>
        <v-stepper vertical v-model="currentStep">
          <!-- STEP 1: START-->
          <v-stepper-step step="1">
            Please choose a file for upload
            <small v-if="currentFile">{{currentFile.name}}</small>
          </v-stepper-step>
          <v-stepper-content step="1" class="text-xs-left">
            <v-card color="grey lighten-4" class="pa-2">
              <v-btn 
                raised 
                large 
                class="color3 white--text" 
                @click="onPickFile"
                :disabled="busy"

              >Upload File</v-btn>
              <input 
                type="file" 
                style="display: none" 
                ref="fileInput" 
                accept=".csv"
                @change="onFilePicked">
            </v-card>
          </v-stepper-content>
          <!-- STEP 1: END-->
          
          <!-- STEP 2: START-->
          <v-stepper-step step="2">
            Verify Data
          </v-stepper-step>
          <v-stepper-content step="2">
            <v-card color="grey lighten-4" class="pa-2">

              <v-checkbox
                label="My file has column headers"
                v-model="header"
              ></v-checkbox>

              <v-data-table
                disable-initial-sort	
                :items="rows"
                :headers="headers"
                v-if="parseResuls"
                class="elevation-1"
              >
                <template slot="items" slot-scope="props">
                  <td v-for="(col, i) in headers" :key="i" style="white-space: nowrap;">{{props.item[col.value]}}</td>
                </template>
              </v-data-table>

            </v-card>
            <v-btn
              color="color3 white--text" 
              @click.native="gotoMapping" 
              :disabled="busy"
            >Continue</v-btn>
            <v-btn
              flat
              @click.native="clear" 
              :disabled="busy"
            >Back</v-btn>
          </v-stepper-content>
          <!-- STEP 2: END-->
          
          <!-- STEP 3: START-->
          <v-stepper-step step="3">
            Map Divisions
          </v-stepper-step>
          <v-stepper-content step="3">
            <v-card color="grey lighten-4" class="pa-2">
              <v-container grid-list-xs>
                <v-layout row>
                  <v-flex text-xs-left>
                    <h2>1. What column of your file is the division?</h2>
                  </v-flex>
                </v-layout>
                <v-layout row>
                  <v-flex text-xs-left>
                    <v-select
                      :items="selectOptions"
                      v-model="divisionColumn"
                      item-text="value"
                      label="Column"
                      hint="Choose a column"
                      dense
                      required
                      :rules="[(v) => !!v || 'Choose a column']"
                    >
                    </v-select>
                  </v-flex>
                </v-layout>
                <v-layout row v-if="divisionColumn">
                  <v-flex text-xs-left>
                    <h2>2. Map the divisions in your .CSV file to the divisions of the tournament</h2>
                  </v-flex>
                </v-layout>
                <v-layout row v-if="divisionList">
                  <v-flex>
                    <v-form v-model="validDivMap" ref="divForm">
                      <v-data-table
                        hide-actions
                        disable-initial-sort	
                        :items="divisionList"
                        :headers="[{text: 'Your Division', sortable: false, align: 'right'},{text: '', sortable: false, align: 'right'},{text: 'Tournament Division', sortable: false, align: 'left'}]"
                        class="elevation-1"
                        style="width: 500px; min-width: 500px; max-width: 500px"
                        dense
                      >
                        <template slot="items" slot-scope="props">
                          <td style="text-align: right; width: 200px; min-width: 200px; max-width: 200px; white-space: nowrap; text-overflow: ellipsis;">
                            {{props.item}}
                          </td>
                          <td style="text-align: center; width: 100px; min-width: 100px; max-width: 100px; white-space: nowrap; text-overflow: ellipsis;">
                            <v-icon>compare_arrows</v-icon>
                          </td>
                          <td style="text-align: left; width: 200px; min-width: 200px; max-width: 200px; white-space: nowrap; text-overflow: ellipsis;">
                            <v-select
                              :items="tournament.divisions"
                              v-model="divisionMap[props.item]"
                              item-text="name"
                              return-object
                              label="Division"
                              hint="Select a division"
                              dense
                              required
                              :rules="[(v) => !!v || 'Choose a column']"
                              style="white-space: nowrap; text-overflow: ellipsis;"
                            ></v-select>
                          </td>
                        </template>
                      </v-data-table>
                    </v-form>
                  </v-flex>
                </v-layout>

              </v-container>
            </v-card>
            <v-btn
              color="color3 white--text" 
              @click.native="currentStep = 4" 
              :disabled="!validDivMap || busy"
            >Continue</v-btn>
            <v-btn
              flat
              @click.native="currentStep = 2"
              :disabled="busy"
            >Back</v-btn>

          </v-stepper-content>
          <!-- STEP 3: END-->
          
          <!-- STEP 4: START-->
          <v-stepper-step step="4">
            Map Columns
          </v-stepper-step>
          <v-stepper-content step="4">
            <v-card color="grey lighten-4" class="pa-2">
              <v-container grid-list-xs>
                <v-layout row>
                  <v-flex text-xs-left>
                    <h2>Column mapping</h2>
                  </v-flex>
                </v-layout>
                <v-layout row>
                  <v-flex text-xs-left>
                    Map the column of your .CSV file to the values needed by choosing the column header from the drop down list.<br>
                    If your .CSV file does not contain the needed value select "Not Available"
                  </v-flex>
                </v-layout>
                <v-layout row>
                  <v-flex>
                    <v-form v-model="validMap" ref="form">
                      <v-data-table
                        hide-actions
                        disable-initial-sort	
                        :items="regFields"
                        :headers="mapHeaders"
                        v-if="parseResuls"
                        class="elevation-1"
                        style="width: 880px; min-width: 880px; max-width: 880px"
                      >
                        <template slot="items" slot-scope="props">
                          <td class="text-xs-right" style="width: 180px; min-width: 180px; max-width: 180px; white-space: nowrap; text-overflow: ellipsis;">{{props.item.name}}</td>
                          <td style="width: 250px; min-width: 250px; max-width: 250px;">
                            <v-select
                              :items="selectOptions"
                              v-model="props.item.value"
                              item-text="value"
                              label="Column"
                              hint="Choose a column"
                              dense
                              required
                              :rules="[(v) => !!v || 'Choose a column']"
                              style="white-space: nowrap; text-overflow: ellipsis;"
                            >
                              <template slot="item" slot-scope="data">
                                <v-divider v-if="data.divider"></v-divider>
                                <v-subheader v-else-if="data.header">{{data.header}}</v-subheader>
                                <v-list-tile-content v-else>{{data.item.value}}</v-list-tile-content>
                              </template>
                            </v-select>
                          </td>
                          <td style="width: 150px; min-width: 150px; max-width: 150px; text-align: left; white-space: nowrap; overflow: hidden; text-overflow: ellipsis;">
                            {{parseResuls.data[0][props.item.value]}}
                          </td>
                          <td style="width: 150px; min-width: 150px; max-width: 150px; text-align: left; white-space: nowrap; overflow: hidden; text-overflow: ellipsis;">
                            {{parseResuls.data[1][props.item.value]}}
                          </td>
                          <td style="width: 150px; min-width: 150px; max-width: 150px; text-align: left; white-space: nowrap; overflow: hidden; text-overflow: ellipsis;">
                            {{parseResuls.data[2][props.item.value]}}
                          </td>
                        </template>
                      </v-data-table>
                    </v-form>
                  </v-flex>
                </v-layout>
              </v-container>
            </v-card>
            <v-btn
              color="color3 white--text" 
              @click.native="currentStep = 5" 
              :disabled="!validMap || busy"
            >Continue</v-btn>
            <v-btn
              flat
              @click.native="currentStep = 3"
              :disabled="busy"
            >Back</v-btn>
          </v-stepper-content>
          <!-- STEP 4: END-->

          <!-- STEP 5: START-->
          <v-stepper-step step="5">
            Confirm
          </v-stepper-step>
          <v-stepper-content step="5">
            <v-card color="grey lighten-4" class="pa-2">
            <v-card-text v-if="parseResuls">
              <h3>
                You are about to import the {{this.parseResuls.data.length}} registration 
                records in {{currentFile.name}} to {{tournament.name}}
              </h3>
              <h3 v-if="tournament.teamCount > 0">
                <v-alert outline color="red" icon="priority_high" :value="true">
                  This tournament has existing teams.
                </v-alert>
                Would you like to
                <v-radio-group v-model="overwrite" :mandatory="false">
                  <v-radio label="Add the imported teams" :value="false"></v-radio>
                  <v-radio label="Overwrite the existing teams" :value="true"></v-radio>
                </v-radio-group>
              </h3>
              <h2>
                <v-checkbox
                  label="I have reviewed all the mappings and would like to complete the upload"
                  v-model="mapConfirmed"
                ></v-checkbox>
              </h2>
              <v-btn 
                color="color3 white--text"
                :disabled="!mapConfirmed || busy"
                @click="upload"
                :loading="busy"
              >
                Complete Upload
              </v-btn>
              <v-btn 
                flat
                @click="currentStep = 4"
                :disabled="busy"
              >Back</v-btn>
            </v-card-text>
          </v-card>
          </v-stepper-content>
          <!-- STEP 5: END-->
        </v-stepper>
      </v-card-text>
    </v-card>
    <v-dialog v-model="workingDialog" max-width="500px" persistent>
      <v-card v-if="busy">
        <v-toolbar color="color2 white--text">
          <v-toolbar-title>Working!</v-toolbar-title>
        </v-toolbar>
        <v-card-text class="text-xs-center">
          <h3>Please wait, we are</h3>
          <v-progress-circular indeterminate v-bind:size="50" color="color3"></v-progress-circular>
          <h3>uploading your registrations.</h3>
        </v-card-text>
      </v-card>
      <v-card v-else-if="error">
        <v-toolbar color="error">
          <v-toolbar-title>Error!</v-toolbar-title>
        </v-toolbar>
        <v-card-text class="text-xs-center">
          We seemed to have had an issue<br>
          {{error}}<br>
          If the problem persists please contact support.
        </v-card-text>
        <v-card-actions>
          <v-btn @click.stop="workingDialog = false">OK</v-btn>
        </v-card-actions>
      </v-card>
      <v-card v-else>
        <v-toolbar color="color3">
          <v-toolbar-title>Success!</v-toolbar-title>
        </v-toolbar>
        <v-card-text class="text-xs-center">
          Your upload is complete.
        </v-card-text>
        <v-card-actions>
          <v-btn @click.stop="complete">OK</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-dialog>
</template>

<script>
import Papa from 'papaparse'
import Registration from '../../classes/TournamentRegistration'
import uniq from 'lodash.uniq'
import moment from 'moment'
import vbl from '../../VolleyballLife'

export default {
  props: ['open', 'tournament'],
  data () {
    return {
      overwrite: false,
      currentStep: 1,
      currentFile: null,
      parseResuls: null,
      header: true,
      validMap: false,
      validDivMap: false,
      mapHeaders: [
        { text: 'Value Needed', sortable: false, align: 'right' },
        { text: 'Choose A Column', sortable: false, align: 'left' },
        { text: 'Sample Data', sortable: false, align: 'left' },
        { text: 'Sample Data', sortable: false, align: 'left' },
        { text: 'Sample Data', sortable: false, align: 'left' }
      ],
      regFields: [
        {name: 'Finish', value: '', map: ['finish ', 'finish']},
        {name: 'Player 1 First Name', value: '', map: ['1,f,name', '1,name']},
        {name: 'Player 1 Last Name', value: '', map: ['1,last,name', '1,name']},
        {name: 'Player 1 AAU Number', value: '', map: ['1,aau']},
        {name: 'Player 1 AVP Number', value: '', map: ['1,avp']},
        {name: 'Player 1 USAV Number', value: '', map: ['1,usav']},
        {name: 'Player 1 CBVA Number', value: '', map: ['1,cbva']},
        {name: 'Player 1 Phone', value: '', map: ['1,phone']},
        {name: 'Player 1 Email', value: '', map: ['1,email']},
        {name: 'Player 1 Birthdate', value: '', map: ['1,birth']},
        {name: 'Player 1 City', value: '', map: ['1,city']},
        {name: 'Player 1 State', value: '', map: ['1,state', '1,st']},
        {name: 'Player 1 Club', value: '', map: ['club']},
        {name: 'Player 2 First Name', value: '', map: ['2,f,name', '2,name']},
        {name: 'Player 2 Last Name', value: '', map: ['2,last,name', '2,name']},
        {name: 'Player 2 AAU Number', value: '', map: ['2,aau']},
        {name: 'Player 2 AVP Number', value: '', map: ['2,avp']},
        {name: 'Player 2 USAV Number', value: '', map: ['2,usav']},
        {name: 'Player 2 CBVA Number', value: '', map: ['2,cbva']},
        {name: 'Player 2 Phone', value: '', map: ['2,phone']},
        {name: 'Player 2 Email', value: '', map: ['2,email']},
        {name: 'Player 2 Birthdate', value: '', map: ['2,birth']},
        {name: 'Player 2 City', value: '', map: ['2,city']},
        {name: 'Player 2 State', value: '', map: ['2,state', '2,st']},
        {name: 'Player 2 Club', value: '', map: ['club']}
      ],
      mapConfirmed: false,
      divisionColumn: null,
      divisionList: null,
      divisionMap: null,
      busy: false,
      error: null,
      workingDialog: false
    }
  },
  computed: {
    rows () {
      return this.parseResuls.data
    },
    headers () {
      let head = []
      for (const prop in this.parseResuls.data[0]) {
        head.push({
          text: this.header ? `${prop}` : `Col ${prop}`,
          value: `${prop}`,
          sortable: false,
          align: 'left'
        })
      }
      return head
    },
    selectOptions () {
      let options = [
        { value: 'Not Available' },
        { divider: true },
        { header: 'Columns' }
      ]
      if (this.parseResuls) {
        for (const prop in this.parseResuls.data[0]) {
          options.push({ value: this.header ? `${prop}` : `Col${prop}` })
        }
      }
      return options
    },
    fieldMap () {
      var result = {}
      for (let f of this.regFields) {
        result[f.name] = f.value
      }
      return result
    }
  },
  methods: {
    onClose () {
      this.clear()
      this.$emit('close')
    },
    onPickFile () {
      this.$refs.fileInput.click()
    },
    onFilePicked (event) {
      const file = event.target.files[0]
      if (file.name.lastIndexOf('.csv') <= 0) {
        alert('Please select a CSV file')
        return
      }
      this.busy = true
      this.currentFile = file
      this.parseFile()
      this.currentStep = 2
      this.busy = false
    },
    parseFile () {
      Papa.parse(this.currentFile, {
        header: this.header,
        complete: (results, file) => {
          this.currentFile = file

          for (let e of results.errors) {
            results.data.splice(e.row, 1)
          }
          let checkProperties = (obj) => {
            for (var key in obj) {
              if (obj[key] !== null && obj[key] !== '') return true
            }
            return false
          }

          results.data = results.data.filter((r) => checkProperties(r))

          this.parseResuls = results
          this.tryMap()
        }
      })
    },
    clear () {
      this.currentFile = null
      this.parseResuls = null
      this.$refs.fileInput.value = null
      this.currentStep = 1
    },
    tryMap () {
      const fields = this.regFields.filter((f) => { return f.map && f.map.length > 0 })

      for (let field of fields) { // each field that has a map array
        for (let m of field.map) {
          let options = this.selectOptions.filter(f => f.value) // column headers
          let filters = m.split(',')
          for (let filter of filters) {
            options = options.filter(o => o.value.toLowerCase().includes(filter.toLowerCase()))
          }

          if (options.length > 0) {
            field.value = options[0].value
            break
          }
        }
      }
    },
    gotoMapping () {
      this.currentStep = 3
      this.$refs.form.validate()
    },
    upload () {
      this.busy = true
      this.workingDialog = true
      this.error = null
      let registrations = this.mapRegistrations()
      const url = this.overwrite ? vbl.tournament.bulkRegisterOverwrite : vbl.tournament.bulkRegister

      this.axios.put(url, registrations)
        .then((response) => {
          console.log(response.data)
          this.busy = false
        })
        .catch((error) => {
          var response = error.response.data
          this.error = response
          console.log(error)
          this.busy = false
        })
    },
    mapRegistrations () {
      let registrations = []
      const map = this.fieldMap
      for (let record of this.parseResuls.data) {
        let registration = new Registration()
        registration.tournamentId = this.tournament.id
        registration.setDivision(this.divisionMap[record[this.divisionColumn]])
        let p1 = registration.players[0]
        let p2 = registration.players[1]

        if (map['Player 1 First Name'] === map['Player 1 Last Name']) {
          var name = record[map['Player 1 First Name']].trim().toLowerCase().replace(/(^|\s)[a-z]/g, function (f) { return f.toUpperCase() }).split(' ')
          p1.lastName = name.pop()
          p1.firstName = name.join(' ')
        } else {
          p1.firstName = record[map['Player 1 First Name']].trim().toLowerCase().replace(/(^|\s)[a-z]/g, function (f) { return f.toUpperCase() })
          p1.lastName = record[map['Player 1 Last Name']].trim().toLowerCase().replace(/(^|\s)[a-z]/g, function (f) { return f.toUpperCase() })
        }
        p1.phone = record[map['Player 1 Phone']]
        p1.email = record[map['Player 1 Email']]
        p1.dob = moment(record[map['Player 1 Birthdate']]).format('YYYY-MM-DD')
        p1.dobFormatted = moment(p1.dob).format('MM/DD/YYYY')
        p1.city = record[map['Player 1 City']]
        p1.state = record[map['Player 1 State']]
        p1.aau = record[map['Player 1 AAU Number']]
        p1.avp = record[map['Player 1 AVP Number']]
        p1.cbva = record[map['Player 1 CBVA Number']]
        p1.usav = record[map['Player 1 USAV Number']]
        p1.club = record[map['Player 1 Club']]

        if (map['Player 2 First Name'] === map['Player 2 Last Name']) {
          var name2 = record[map['Player 2 First Name']].trim().toLowerCase().replace(/(^|\s)[a-z]/g, function (f) { return f.toUpperCase() }).split(' ')
          p2.lastName = name2.pop()
          p2.firstName = name2.join(' ')
        } else {
          p2.firstName = record[map['Player 2 First Name']].toLowerCase().replace(/(^|\s)[a-z]/g, function (f) { return f.toUpperCase() })
          p2.lastName = record[map['Player 2 Last Name']].toLowerCase().replace(/(^|\s)[a-z]/g, function (f) { return f.toUpperCase() })
        }
        p2.phone = record[map['Player 2 Phone']]
        p2.email = record[map['Player 2 Email']]
        p2.dob = moment(record[map['Player 2 Birthdate']]).format('YYYY-MM-DD')
        p2.dobFormatted = moment(p2.dob).format('MM/DD/YYYY')
        p2.city = record[map['Player 2 City']]
        p2.state = record[map['Player 2 State']]
        p2.aau = record[map['Player 2 AAU Number']]
        p2.avp = record[map['Player 2 AVP Number']]
        p2.cbva = record[map['Player 2 CBVA Number']]
        p2.usav = record[map['Player 2 USAV Number']]
        p2.club = record[map['Player 2 Club']]

        let finish = record[map['Finish']]
        if (finish && finish > 0) {
          registration.finish = finish
        } else {
          registration.finish = 0
        }

        registrations.push(registration.dto)
      }

      return registrations
    },
    complete () {
      this.$emit('complete')
      this.workingDialog = false
    }
  },
  watch: {
    header () {
      this.parseFile()
    },
    divisionColumn () {
      this.divisionList = uniq(this.parseResuls.data.map(r => r[this.divisionColumn]))
      let map = {}
      for (let d of this.divisionList) {
        map[d] = null
      }

      this.divisionMap = map
    }
  }
}
</script>

<style>

</style>
