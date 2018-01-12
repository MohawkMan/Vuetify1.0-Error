<template>
  <v-card>
    <v-toolbar color="color3" v-if="toolbar">
      <v-toolbar-title>{{division.divisionsString}}</v-toolbar-title>
    </v-toolbar>
    <v-data-table
      :headers="tableHeaders"
      :items="rows"
      hide-actions
    >
      <template slot="items" slot-scope="props">
        <tr style="cursor: pointer">
          <td>{{ props.item.finish }}</td>
          <td>{{props.item.name}}</td>
          <td>{{ props.item.points }}</td>
        </tr>
      </template>
      <template slot="no-data">
        <loading-data grid-list-md v-if="loading"></loading-data>
      </template>
    </v-data-table>
  </v-card>
</template>

<script>
export default {
  props: ['division', 'mode', 'toolbar'],
  data () {
    return {
      loading: false
    }
  },
  computed: {
    _mode () {
      return this.mode
        ? this.mode
        : this.$vuetify.breakpoint.xs
          ? 'list'
          : 'table'
    },
    tableHeaders () {
      return [
        {text: 'Finish', value: 'finish', align: 'left'},
        {text: 'Name', value: 'name', align: 'left', sortable: false},
        {text: 'Points Earned', value: 'points', align: 'left', sortable: false}
      ]
    },
    rows () {
      return this.division.teams
    }
  }
}
</script>

<style>

</style>
