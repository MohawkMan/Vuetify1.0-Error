<template>
  <v-card>
    <v-toolbar color="color3" v-if="toolbar">
      <v-toolbar-title>{{division.divisionsString}}</v-toolbar-title>
    </v-toolbar>
    <v-data-table
      v-if="type === 'registrations'"
      :headers="regHeaders"
      :items="rows"
      hide-actions
    >
      <template slot="items" slot-scope="props">
        <tr>
          <td>{{props.item.name}}</td>
        </tr>
      </template>
    </v-data-table>
    <v-data-table
      v-else
      :headers="tableHeaders"
      :items="rows"
      hide-actions
    >
      <template slot="items" slot-scope="props">
        <tr style="cursor: pointer">
          <td class="text-xs-center">{{ props.item.finish }}</td>
          <td>{{props.item.name}}</td>
          <td class="text-xs-center">{{ props.item.points }}</td>
        </tr>
      </template>
      <template slot="footer" v-if="division.sanctioningBodyId && division.sanctioningBodyId.startsWith('AVP')">
        <td colspan="100%" class="text-xs-center" style="rgba(0,0,0,0.54); font-size: 11px;">
          *This is not the official AVP points reporting site. Please check the <a href="http://avpnext.avp.com/rankings.aspx" target="_blank">AVP Site</a> for offical results
        </td>
      </template>
      <template slot="no-data">
        <loading-data grid-list-md v-if="loading"></loading-data>
      </template>
    </v-data-table>
  </v-card>
</template>

<script>
export default {
  props: ['division', 'mode', 'toolbar', 'type'],
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
      let headers = [
        {text: 'Finish', value: '_finish', align: 'center'},
        {text: 'Name', value: 'name', align: 'left', sortable: false}
      ]
      if (this.division.sanctioningBodyId.startsWith('AVP')) {
        headers.push({text: 'AVP Points Earned*', value: 'points', align: 'center', sortable: false})
      } else {
        headers.push({text: 'Points Earned', value: 'points', align: 'center', sortable: false})
      }
      return headers
    },
    rows () {
      return this.division.teams.map((team) => {
        return {
          finish: team.finish < 999 ? team.finish : '',
          _finish: team.finish ? team.finish : 999,
          name: team.name,
          points: team.points
        }
      })
    },
    regHeaders () {
      return [
        {text: 'Name', value: 'name', align: 'left', sortable: false}
      ]
    }
  }
}
</script>

<style>

</style>
