<template>
  <v-data-table
    :headers="headers"
    :items="players"
    :pagination.sync="pager"
    >
    <template slot="items" slot-scope="props">
      <tr>
        <td></td>
        <td>{{props.item.name}}</td>
        <td>{{props.item.events}}</td>
        <td>{{props.item.currentPoints}}</td>
      </tr>
    </template>
  </v-data-table>
</template>

<script>
export default {
  props: ['players'],
  data () {
    return {
      headers: [
        {text: 'Rank', value: '', align: 'left', sortable: 'false'},
        {text: 'Player', value: 'name', align: 'left', sortable: 'false'},
        {text: 'Events', value: 'name', align: 'left', sortable: 'false'},
        {text: 'Points', value: 'currentPoints', align: 'left', sortable: 'false'}
      ],
      pager: {sortBy: 'currentPoints', descending: true}
    }
  }
}
</script>

<style>

</style>
