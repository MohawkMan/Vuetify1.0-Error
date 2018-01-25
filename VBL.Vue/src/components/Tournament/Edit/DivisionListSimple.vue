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
          <td class="text-xs-right">
            <v-btn 
              icon
              flat 
              color="color3"
              :loading="busy"
              @click="editDivision(props.index, props.item)">
              <v-icon>edit</v-icon>
            </v-btn>
            <v-btn 
              icon 
              flat 
              color="red darken-4"
              :loading="busy"
              @click="deleteDivision(props.index)">
              <v-icon>delete</v-icon>
            </v-btn>
          </td>
        </template>
        <template slot="no-data">
          <v-container>
            <v-layout row wrap>
              <v-flex>
                <v-btn
                  color="color5 color1--text"
                  :loading="busy"
                  @click.native.stop="addDivision">
                  Add A Division
                </v-btn>
              </v-flex>
            </v-layout>
          </v-container>
        </template>
        <template slot="footer">
          <td colspan="100%" class="text-xs-right">
            <v-btn 
              flat
              class="color3--text"
              @click.native.stop="addDivision"
              :loading="busy"
              v-if="divisions.length > 0">
              <v-icon class="mr-2">playlist_add</v-icon>
              Add Another Division
            </v-btn>
          </td>
        </template>
      </v-data-table>
      <v-dialog v-model="editDialog" max-width="500px">
        <division-simple
          v-if="selectedDivision"
          :division="selectedDivision"
          @save="saveDivision"
          @cancel="cancelEdit"
        ></division-simple>
      </v-dialog>
    </v-flex>
  </v-layout>
</template>

<script>
import Division from '../../../classes/TournamentDivision'
import DivisionSimple from './DivisionSimple.vue'

export default {
  props: ['tournament', 'busy'],
  data () {
    return {
      selectedDivision: null,
      selectedIndex: -1,
      editDialog: false
    }
  },
  computed: {
    divisions () {
      return this.tournament.divisions
    },
    loading () {
      return this.$store.getters.loading
    },
    divisionHeaders () {
      return [
        {text: 'Type', value: 'age.name', align: 'left'},
        {text: 'Gender', value: 'gender.name', align: 'left'},
        {text: 'Division', value: 'division.name', align: 'left'},
        {text: '', value: '', align: 'right', sortable: false}
      ]
    }
  },
  methods: {
    addDivision () {
      let d = new Division(JSON.stringify(this.tournament.divisionTemplate))
      this.editDivision(-1, d)
    },
    deleteDivision (index) {
      this.divisions.splice(index, 1)
    },
    editDivision (index, division) {
      if (division) {
        this.selectedIndex = index
        this.selectedDivision = division
        this.editDialog = true
      }
    },
    cancelEdit () {
      this.editDialog = false
    },
    saveDivision () {
      if (this.selectedIndex === -1) {
        this.divisions.push(this.selectedDivision)
      } else {
        this.divisions[this.selectedIndex] = this.selectedDivision
      }
      this.cancelEdit()
    }
  },
  components: {
    'division-simple': DivisionSimple
  }
}
</script>
