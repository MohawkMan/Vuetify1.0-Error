<template>
  <v-data-table
    :headers="headers"
    :items="rows"
    :pagination.sync="myPager"
    >
    <template slot="items" slot-scope="props">
      <tr style="cursor: pointer">
        <td class="text-xs-center" @click="gotoDetails(props.item.link)">
          <img src="/static/AAU.png" height="40px" :alt="props.item.sanctionedBy" v-if="props.item.sanctionedBy === 'AAU'">
          <img src="/static/avpfirstlogo.png" height="40px" :alt="props.item.sanctionedBy" v-else-if="props.item.sanctionedBy.startsWith('AVP')">
        </td>
        <td @click="gotoDetails(props.item.link)">
          {{ props.item.date | formatDate }}
          <v-tooltip right v-if="!props.item.public">
            <v-icon slot="activator">visibility_off</v-icon>
            <span>Only you can see this</span>
          </v-tooltip>
        </td>
        <td @click="gotoDetails(props.item.link)">{{ props.item.name }}</td>
        <td @click="gotoDetails(props.item.link)">{{ props.item.locations }}</td>
        <td>
          <v-btn small :to="`${props.item.link}/register`" v-if="!admin && props.item.regOpen">
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
import * as mutations from '../../store/MutationTypes'

export default {
  props: {
    tourneys: Array,
    loading: Boolean,
    page: {
      type: Object,
      default: function () {
        return { sortBy: 'date', page: 1, rowsPerPage: 5, descending: false, totalItems: 0 }
      }
    }
  },
  data () {
    return {
      agefilter: '',
      genderFilter: '',
      divisionFilter: '',
      locationFilter: '',
      myPager: this.page
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
      return this.user && this.user.isPageAdmin(this.pageName)
    },
    rows () {
      return this.tourneys ? this.tourneys.map((t) => {
        return {
          id: t.id,
          date: t.startDate,
          name: t.name,
          locations: t.locationsString,
          link: `/${t.organization.username}/tournament/${t.id}`,
          public: t.isPublic,
          regOpen: t.regOpen,
          sanctionedBy: t.sanctionedBy
        }
      }) : []
    },
    headers () {
      return [
        {text: '', value: 'sanctionedBy', align: 'left'},
        {text: 'Date', value: 'date', align: 'left'},
        {text: 'Name', value: 'name', align: 'left'},
        {text: 'Location', value: 'locations', align: 'left'},
        {text: '', value: '', align: 'left', sortable: 'false'}
      ]
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
