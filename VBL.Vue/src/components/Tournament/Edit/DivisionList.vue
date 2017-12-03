<template>
  <v-layout row wrap>
    <v-flex>
      <v-data-table
        :headers="divisionHeaders"
        :items="divisions"
        hide-actions>
        <template slot="items" slot-scope="props">
          <td>{{ props.item.ageType ? props.item.ageType.name : '' }}</td>
          <td>{{ props.item.gender ? props.item.gender.name : '' }}</td>
          <td>{{ props.item.division ? props.item.division.name : '' }}</td>
          <td>{{ props.item.location ? props.item.location.name : '' }}</td>
          <td>{{ props.item.entryFee | usDollars }}</td>
          <td>
            <v-btn fab small dark color="color3" @click="editDivision(props.index, props.item)">
              <v-icon>edit</v-icon>
            </v-btn>
            <v-btn fab small dark color="red darken-4">
              <v-icon>delete</v-icon>
            </v-btn>
          </td>
        </template>
        <template slot="no-data">
          <v-container>
            <v-layout row wrap>
              <v-flex>
                <v-btn @click.native.stop="addDivision">
                  Add Division
                </v-btn>
              </v-flex>
            </v-layout>
          </v-container>
        </template>
      </v-data-table>
      <v-dialog v-model="editDialog"
        fullscreen 
        transition="dialog-bottom-transition" 
        overlay="false">
        <division-edit
          v-if="selectedDivision"
          :division="selectedDivision.division"
          @save="saveDivision"
          @cancel="cancelEdit"
        ></division-edit>
      </v-dialog>
    </v-flex>
  </v-layout>
</template>

<script>
import Division from '../../../classes/TournamentDivision'
import DivisionEdit from './Division.vue'

export default {
  props: ['divisions'],
  data () {
    return {
      selectedDivision: null,
      editDialog: false
    }
  },
  computed: {
    loading () {
      return this.$store.getters.loading
    },
    divisionHeaders () {
      return [
        {text: 'Type', value: 'ageName', align: 'left'},
        {text: 'Gender', value: 'genderName', align: 'left'},
        {text: 'Division', value: 'divisionName', align: 'left'},
        {text: 'Location', value: 'locationName', align: 'left'},
        {text: 'Entry', value: 'entryFee', align: 'left'}
      ]
    }
  },
  methods: {
    addDivision () {
      this.editDivision({
        index: -1,
        division: new Division()
      })
    },
    editDivision (index, division) {
      if (division) {
        this.selectedDivision = division
        this.editDialog = true
      }
    },
    cancelEdit () {
      this.editDialog = false
      // this.selectedDivision = null
    },
    saveDivision () {
      // PLAY WITH THIS AND TEST FIND INDEX
      if (this.selectedDivision.index === -1) {
        this.divisions.push(this.selectedDivision.division)
      } else {
        this.divisions[this.selectedDivision.index] = this.selectedDivision.division
      }
      this.cancelEdit()
    }
  },
  components: {
    'division-edit': DivisionEdit
  }
}
</script>
