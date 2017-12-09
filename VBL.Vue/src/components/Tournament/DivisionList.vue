<template>
    <v-data-table
    :headers="divisionHeaders"
    :items="divisions"
    hide-actions>
    <template slot="items" slot-scope="props">
      <td>{{ props.item.ageType.name }}</td>
      <td>{{ props.item.gender.name }}</td>
      <td>{{ props.item.division.name }}</td>
      <td>{{ props.item.currentEntryFeeString }}</td>
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

</template>

<script>
export default {
  props: ['divisions'],
  computed: {
    divisionHeaders () {
      return [
        {text: 'Type', value: 'ageType.name', align: 'left'},
        {text: 'Gender', value: 'gender.name', align: 'left'},
        {text: 'Division', value: 'division.name', align: 'left'},
        {text: 'Entry Fee', value: 'currentEntryFee', align: 'left'}
      ]
    }
  }
}
</script>

<style>

</style>
