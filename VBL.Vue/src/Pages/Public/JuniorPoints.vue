<template>
  <v-container fill-height v-if="loading">
    <v-layout row wrap align-center>
      <v-flex xs8 offset-xs2>
        <v-layout row wrap text-xs-center>
          <v-flex xs12>
            <h3>Loading</h3>
          </v-flex>
          <v-flex xs12>
            <v-progress-linear v-bind:indeterminate="true"></v-progress-linear>
          </v-flex>
        </v-layout>
      </v-flex>
    </v-layout>
  </v-container>
  <v-container v-else grid-list-md>
    <v-layout row wrap>
      <v-flex xs12 sm10 offset-sm1>
        <v-card>
          <v-toolbar dark color="color3" class="mx-auto">
            <v-toolbar-title>AAU Point System</v-toolbar-title>
          </v-toolbar>
          <v-card-text>
            <v-container fluid grid-list-sm>
              <v-layout row wrap>
                <v-flex>
                  <v-card flat>
                    <h2>The AAU Point System</h2>
                    <p>
                    Points are awarded for all participating 12U, 14U, 16U, and 18U AAU juniors tournaments.  The amount of points awarded at each event will be determined by combining our "Base Point Scale" with any approved multipliers for the event. Every event will be subject to the "Team Participation Multiplier" detailed below.
                    </p>
                    <p>
                    Points are awarded to 1st through 9th place for each event. All players who finish outside of 9th place will receive 5 points for participating in the event. Points are cumulative and rankings are calculated by the sum of all points earned within the last calendar year.
                    </p>
                  </v-card>
                </v-flex>
              </v-layout>
              <v-layout row wrap>
                <v-flex>
                  <v-card flat class="text-xs-center">
                    <v-toolbar dense color="color4">
                      <v-toolbar-title>Base Point Scale</v-toolbar-title>
                    </v-toolbar>
                  </v-card>
                </v-flex>
              </v-layout>
              <v-layout row wrap>
                <v-flex xs12 sm6 lg3 v-for="(table, i) in tables" :key="i">
                  <v-card>
                    <v-toolbar dense color="color5">
                      <v-toolbar-title>{{table.title}}</v-toolbar-title>
                    </v-toolbar>
                    <v-card-text>
                      <v-data-table
                        :headers="headers"
                        :items="table.rows"
                        :pagination.sync="pager"
                        hide-actions
                      >
                        <template slot="items" slot-scope="props">
                          <tr>
                            <td v-if="props.item.finish < 100000">{{props.item.finish | ordinal}}</td>
                            <td v-else>All others</td>
                            <td>{{props.item.points}}</td>
                          </tr>
                        </template>
                      </v-data-table>
                    </v-card-text>
                  </v-card>
                </v-flex>
                <v-flex xs12 sm6>
                  <v-card class="text-xs-center">
                    <v-toolbar dense color="color4">
                      <v-toolbar-title>Team Participation Multiplier</v-toolbar-title>
                    </v-toolbar>
                    <v-card-text>
                      <v-data-table
                        :headers="mHeaders"
                        :items="multipliers"
                        disable-initial-sort	
                        hide-actions
                      >
                        <template slot="items" slot-scope="props">
                          <tr>
                            <td class="text-xs-left">{{props.item.description}}</td>
                            <td class="text-xs-left">{{props.item.multiplier}}</td>
                          </tr>
                        </template>
                      </v-data-table>
                    </v-card-text>
                  </v-card>
                </v-flex>
              </v-layout>
              <v-layout row wrap>
              </v-layout>
            </v-container>
          </v-card-text>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import vbl from '../../VolleyballLife'
import uniq from 'lodash.uniq'
// import each from 'lodash.foreach'

export default {
  data () {
    return {
      loading1: false,
      loading2: false,
      pointScale: null,
      multipliers: null,
      headers: [
        {text: 'Finish', align: 'left', sortable: false},
        {text: 'Points', align: 'left', sortable: false}
      ],
      pager: {sortBy: 'finish', descending: false, rowsPerPage: 50},
      mHeaders: [
        {text: 'Teams', align: 'left', sortable: false},
        {text: 'Point Multiplier', align: 'left', sortable: false}
      ]
    }
  },
  computed: {
    tables () {
      let tables = uniq(this.pointScale.map(p => p.division)).sort().reverse()
      return tables.map((table) => {
        return {title: table, rows: this.pointScale.filter(p => p.division === table)}
      })
    },
    loading () {
      return this.loading1 || this.loading2
    }
  },
  methods: {
    getScale () {
      this.loading1 = true
      this.axios.get(vbl.rankings.pointScale)
        .then((response) => {
          this.pointScale = response.data
          this.loading1 = false
        })
        .catch((error) => {
          console.log(error)
          this.loading1 = false
        })
    },
    getMultipliers () {
      this.loading2 = true
      this.axios.get(vbl.rankings.teamMultiplier)
        .then((response) => {
          this.multipliers = response.data
          this.loading2 = false
        })
        .catch((error) => {
          console.log(error)
          this.loading2 = false
        })
    }
  },
  created () {
    this.getScale()
    this.getMultipliers()
  }
}
</script>

<style>

</style>
