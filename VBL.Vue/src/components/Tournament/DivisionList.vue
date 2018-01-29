<template>
  <v-data-table
    v-if="mode === 'table'"
    :headers="divisionHeaders"
    :items="divisions"
    hide-actions>
    <template 
      slot="items" 
      slot-scope="props">
      <tr>
        <td>{{ props.item.ageType.name }}</td>
        <td>{{ props.item.gender.name }}</td>
        <td>{{ props.item.division.name }}</td>
        <td>{{ props.item.currentEntryFeeString }}</td>
        <td>
          <v-btn @click="registerClick(props.item)">Register</v-btn>
        </td>
      </tr>
    </template>
    <template slot="no-data">
      <v-container>
        <v-layout row wrap>
          <v-flex>
          </v-flex>
        </v-layout>
      </v-container>
    </template>
  </v-data-table>
  <v-list
    v-else
    two-line
  >
    <template v-for="(division,i) in divisions">
      <v-list-tile
        avatar
        :key="i"
      >
        <v-list-tile-content>
          <v-list-tile-title>{{division.divisionsString}}</v-list-tile-title>
          <v-list-tile-sub-title>{{division.currentEntryFeeString}}</v-list-tile-sub-title>
        </v-list-tile-content>
        <v-list-tile-action>
          <v-btn 
            v-if="division.currentRegistrationWindow"
            fab
            color="color3 white--text"
            @click="registerClick(division)"
            >
            <v-icon>assignment_turned_in</v-icon>
          </v-btn>
        </v-list-tile-action>
      </v-list-tile>
      <v-divider v-if="i + 1 < divisions.length" :key="'divider' + i"></v-divider>
    </template>

  </v-list>
</template>

<script>
export default {
  props: ['divisions', 'mode'],
  computed: {
    _mode () {
      return this.mode
        ? this.mode
        : this.$vuetify.breakpoint.xs
          ? 'list'
          : 'table'
    },
    divisionHeaders () {
      return [
        {text: 'Type', value: 'ageType.name', align: 'left'},
        {text: 'Gender', value: 'gender.name', align: 'left'},
        {text: 'Division', value: 'division.name', align: 'left'},
        {text: 'Entry Fee', value: 'currentEntryFee', align: 'left'}
      ]
    }
  },
  methods: {
    registerClick (division) {
      this.$emit('registerClick', division)
    }
  }
}
</script>

<style>

</style>
