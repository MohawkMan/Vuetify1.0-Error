<template>
  <v-data-table
    :headers="headers"
    :items="rows"
    :pagination.sync="pager"
    >
    <template slot="items" slot-scope="props">
      <tr>
        <td>{{props.item.rank}}</td>
        <td>{{props.item.name}}</td>
        <td>{{props.item.events}}</td>
        <td>{{props.item.currentPoints}}</td>
      </tr>
    </template>
  </v-data-table>
</template>

<script>
import uniq from 'lodash.uniq'
import each from 'lodash.foreach'

export default {
  props: ['players'],
  data () {
    return {
      headers: [
        {text: 'Rank', value: 'rank', align: 'left', sortable: 'false'},
        {text: 'Player', value: 'name', align: 'left', sortable: 'false'},
        {text: 'Events', value: 'events', align: 'left', sortable: 'false'},
        {text: 'Points', value: 'currentPoints', align: 'left', sortable: 'false'}
      ],
      pager: {sortBy: 'rank', descending: false, rowsPerPage: 50}
    }
  },
  computed: {
    rows () {
      let points = uniq(this.players.map(d => d.currentPoints)).sort((a, b) => { return b - a })

      let rank = 1
      for (let point of points) {
        let players = this.players.filter(f => f.currentPoints === point)
        each(players, (player) => {
          player.rank = rank
        })
        rank += players.length
      }
      return this.players
    }
  }
}
</script>

<style>

</style>
