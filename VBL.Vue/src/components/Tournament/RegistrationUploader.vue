<template>
  <v-dialog
    v-model="open"
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
        >
          <v-icon>close</v-icon>
        </v-btn>
        <v-toolbar-title>Result Upload</v-toolbar-title>
      </v-toolbar>
      <v-card-text>

        <v-stepper vertical v-model="currentStep">
          <v-stepper-step step="1">
            Choose a file
            <small v-if="currentFile">{{currentFile.name}}</small>
          </v-stepper-step>
          <v-stepper-content step="1" class="text-xs-center">
            <v-btn 
              raised 
              large 
              class="color3 white--text" 
              @click="onPickFile"
            >Upload File</v-btn>
            <input 
              type="file" 
              style="display: none" 
              ref="fileInput" 
              accept=".csv"
              @change="onFilePicked">
          </v-stepper-content>

          <v-stepper-step step="2">
            Map Columns
          </v-stepper-step>
          <v-stepper-content step="2" class="text-xs-center">
            <v-container grid-list-xs>
              <v-layout row>
                <v-flex>
                  <v-data-table
                    hide-actions
                    hide-headers
                    :items="regFields"
                    v-if="parseResuls"
                    class="elevation-1"
                  >
                    <template slot="items" slot-scope="props">
                      <td width="25%">{{props.item.name}}</td>
                      <td>
                        <v-select
                          :items="selectOptions"
                          hide-details	
                          v-model="props.item.value"
                          item-text="value"
                          dense
                        >
                          <template slot="item" slot-scope="data">
                            <v-divider v-if="data.divider"></v-divider>
                            <v-subheader v-else-if="data.header">{{data.header}}</v-subheader>
                            <v-list-tile-content v-else>{{data.item.value}}</v-list-tile-content>
                          </template>
                        </v-select>
                      </td>
                    </template>
                  </v-data-table>
                </v-flex>
              </v-layout>
            </v-container>

          </v-stepper-content>
        </v-stepper>

      </v-card-text>
    </v-card>
  </v-dialog>
</template>

<script>
import Papa from 'papaparse'
export default {
  props: ['open'],
  data () {
    return {
      currentStep: 1,
      currentFile: null,
      parseResuls: null,
      header: true,
      regFields: [
        {name: 'Divsion', value: ''},
        {name: 'Finish', value: ''},
        {name: 'Player 1 First Name', value: ''},
        {name: 'Player 1 Last Name', value: ''},
        {name: 'Player 1 AAU Number', value: ''},
        {name: 'Player 1 USAV Number', value: ''},
        {name: 'Player 1 Phone', value: ''},
        {name: 'Player 1 Email', value: ''},
        {name: 'Player 1 Birthdate', value: ''},
        {name: 'Player 1 City', value: ''},
        {name: 'Player 1 State', value: ''},
        {name: 'Player 1 Club', value: ''},
        {name: 'Player 2 First Name', value: ''},
        {name: 'Player 2 Last Name', value: ''},
        {name: 'Player 2 AAU Number', value: ''},
        {name: 'Player 2 USAV Number', value: ''},
        {name: 'Player 2 Phone', value: ''},
        {name: 'Player 2 Email', value: ''},
        {name: 'Player 2 Birthdate', value: ''},
        {name: 'Player 2 City', value: ''},
        {name: 'Player 2 State', value: ''},
        {name: 'Player 2 Club', value: ''}
      ]
    }
  },
  computed: {
    rows () {
      return this.parseResuls.data
    },
    selectOptions () {
      let options = [
        { value: 'Not Available' },
        { divider: true },
        { header: 'Columns' }
      ]
      for (const prop in this.parseResuls.data[0]) {
        options.push({ value: this.header ? `${prop}` : `Col${prop}` })
      }
      return options
    }
  },
  methods: {
    onClose () {
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
      this.currentFile = file
      this.parseFile()
      this.currentStep = 2
    },
    parseFile () {
      Papa.parse(this.currentFile, {
        header: this.header,
        complete: (results, file) => {
          this.currentFile = file

          for (let e of results.errors) {
            results.data.splice(e.row, 1)
          }

          this.parseResuls = results
        }
      })
    },
    clear () {
      this.currentFile = null
      this.parseResuls = null
      this.$refs.fileInput.value = null
    },
    tryMap () {
      
    }
  }
}
</script>

<style>

</style>
