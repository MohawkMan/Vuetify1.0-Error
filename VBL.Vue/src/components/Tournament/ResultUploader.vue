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
          @click.native="open = false"
        >
          <v-icon>close</v-icon>
        </v-btn>
        <v-toolbar-title>Result Upload</v-toolbar-title>
      </v-toolbar>
      <v-card-text>

        <v-container fluid>
          <v-layout row>
            <v-flex xs12>

              <v-container grid-list-md>
                <v-layout row>
                  <v-flex xs1 text-xs-right>
                    <h1>File:</h1>
                  </v-flex>
                  <v-flex xs11>
                    <v-btn 
                      v-if="!currentFile"
                      raised 
                      large 
                      class="color3 white--text" 
                      @click="onPickFile"
                    >Upload File</v-btn>
                    <v-chip v-else color="color4" text-color="white">
                      <v-avatar>
                        <v-icon>insert_drive_file</v-icon>
                      </v-avatar>
                      {{currentFile.name}}
                      <v-icon 
                        right 
                        @click="clear"
                        class="ml-3">cancel</v-icon>
                    </v-chip>
                    <input 
                      type="file" 
                      style="display: none" 
                      ref="fileInput" 
                      accept=".csv"
                      @change="onFilePicked">
                  </v-flex>
                </v-layout>

                <v-layout row>
                  <v-flex xs12>

                    <v-data-table
                      v-if="parseResuls"
                      :headers="headers"
                      :items="rows"
                      disable-initial-sort>
                      <template slot="items" slot-scope="props">
                        <tr>
                          <td v-for="(col,i) in headers" :key="i">
                            {{props.item[col.value]}}
                          </td>
                        </tr>
                      </template>
                    </v-data-table>

                  </v-flex>
                </v-layout>

              </v-container>

            </v-flex>
          </v-layout>
        </v-container>


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
      currentFile: null,
      parseResuls: null,
      header: true
    }
  },
  computed: {
    rows () {
      return this.parseResuls.data
    },
    headers () {
      let cols = []
      for (const prop in this.parseResuls.data[0]) {
        cols.push({
          text: this.header ? `${prop}` : `Col${prop}`,
          value: `${prop}`,
          align: 'left'
        })
      }
      return cols
    }
  },
  methods: {
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
    },
    parseFile () {
      Papa.parse(this.currentFile, {
        header: this.header,
        complete: (results, file) => {
          this.currentFile = file
          this.parseResuls = results
        }
      })
    },
    clear () {
      this.currentFile = null
      this.parseResuls = null
      this.$refs.fileInput.value = null
    }
  }
}
</script>

<style>

</style>
