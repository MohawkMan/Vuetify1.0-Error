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
          <td>{{ props.index + 1 }}</td>
          <td>{{props.item.name}}</td>
          <td>{{ props.item.points }}</td>
          <td>{{ props.item.seed }}</td>
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
        {text: 'Name', value: 'name', align: 'left'},
        {text: 'Points', value: 'points', align: 'left'},
        {text: 'Seed', value: 'seed', align: 'left'},
        {text: 'Finish', value: 'finish', align: 'left'}
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
