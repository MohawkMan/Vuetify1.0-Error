<template>
  <v-data-table
    :headers="headers"
    :items="rows"
    :pagination.sync="pagination"
    >
    <template slot="items" slot-scope="props">
      <tr style="cursor: pointer">
        <td @click="gotoDetails(props.item.link)">{{ props.item.date | formatDate }}</td>
        <td @click="gotoDetails(props.item.link)" v-html="props.item.name"></td>
        <!--
        <td @click="gotoDetails(props.item.link)">{{ props.item.divisions }}</td>
        -->
        <td @click="gotoDetails(props.item.link)">{{ props.item.locations }}</td>
        <td>
          <v-btn small :to="`${props.item.link}/register`" v-if="!admin && props.item.regOpen">
            Register
          </v-btn>
          <v-btn 
            small
            dark
            fab
            color="color3"
            v-if="admin"
            @click="edit(props.item.id)"
            >
            <v-icon>edit</v-icon>
          </v-btn>
        </td>
        <td v-if="admin">
          <v-icon v-if="props.item.public">visibility</v-icon>
          <v-icon v-else>visibility_off</v-icon>
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
import * as mutations from '../../store/MutationTypes'

export default {
  props: ['tourneys', 'loading', 'page'],
  data () {
    return {
      agefilter: '',
      genderFilter: '',
      divisionFilter: '',
      locationFilter: '',
      _pagination: null
    }
  },
  computed: {
    ...mapGetters([
      'user',
      'ageTypeOptions',
      'genderOptions',
      'divisionOptions',
      'locationOptions'
    ]),
    pageName () {
      return this.$route.params.username
    },
    admin () {
      return this.user ? this.user.isPageAdmin(this.pageName) : false
    },
    rows () {
      return this.tourneys ? this.tourneys.map((t) => {
        return {
          id: t.id,
          date: t.startDate,
          name: t.name,
          divisions: t.divisionsString,
          locations: t.locationsString,
          link: `/${t.organization.username}/tournament/${t.id}`,
          public: t.isPublic,
          regOpen: t.regOpen
        }
      }) : []
    },
    headers () {
      return [
        {text: 'Date', value: 'date', align: 'left'},
        {text: 'Name', value: 'name', align: 'left'},
        // {text: 'Divisions', value: 'divisions', align: 'left'},
        {text: 'Location', value: 'locations', align: 'left'}
      ]
    },
    pagination: {
      get: () => {
        if (!this._pagination) this._pagination = this.page
        return this._pagination
      },
      set: (newVal) => {
        this._pagination = newVal
      }
    }
  },
  methods: {
    gotoDetails (link) {
      this.$router.push(link)
    },
    edit (id) {
      let tournament = this.tourneys.find(t => t.id === id)
      this.$store.commit(mutations.SET_SELECTED_TOURNAMENT, tournament)
      this.$router.push({name: 'tournament-edit', params: {username: this.pageName, tournamentId: id}})
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
