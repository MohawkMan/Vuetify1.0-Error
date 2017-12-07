<template>
  <v-data-table
    :headers="headers"
    :items="rows"
    >
    <template slot="items" slot-scope="props">
      <tr>
        <td @click="gotoDetails(props.item.id)">{{ props.item.date | formatDate }}</td>
        <td @click="gotoDetails(props.item.id)">{{ props.item.name }}</td>
        <td @click="gotoDetails(props.item.id)">{{ props.item.divisions }}</td>
        <td @click="gotoDetails(props.item.id)">{{ props.item.locations }}</td>
        <td>
          <v-btn small :to="'/tournament/' + props.item.id + '/register'">
            Register
          </v-btn>
        </td>
      </tr>
    </template>
    <template slot="no-data">
      <loading-data grid-list-md v-if="loading"></loading-data>
    </template>
  </v-data-table>
</template>

<script>
import { mapGetters } from 'vuex'
import moment from 'moment'

export default {
  props: ['tourneys', 'loading'],
  data () {
    return {
      agefilter: '',
      genderFilter: '',
      divisionFilter: '',
      locationFilter: ''
    }
  },
  computed: {
    ...mapGetters([
      'ageTypeOptions',
      'genderOptions',
      'divisionOptions',
      'locationOptions'
    ]),
    rows () {
      return this.tourneys.map((t) => {
        return {
          id: t.id,
          date: t.startDate,
          name: t.name,
          divisions: t.divisionsString,
          locations: t.locationsString
        }
      })
    },
    headers () {
      return [
        {text: 'Date', value: 'date', align: 'left'},
        {text: 'Name', value: 'name', align: 'left'},
        {text: 'Divisions', value: 'divisions', align: 'left'},
        {text: 'Location', value: 'locations', align: 'left'}
      ]
    }
  },
  methods: {
    gotoDetails (id) {
      this.$router.push({name: 'tournament-brochure', params: {tournamentId: id}})
    }
  },
  filters: {
    formatDate (date) {
      return moment(date).format('dddd, MMMM Do YYYY')
    }
  }
}
</script>

<style>

</style>
