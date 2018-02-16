<template>
  <v-card>
    <v-toolbar dark color="color2">
      <v-toolbar-title>{{title}}</v-toolbar-title>
      <v-spacer></v-spacer>
      <v-toolbar-items>
        <v-btn dark flat @click.native.stop="save">
          <v-icon class="mr-2">save</v-icon>
          Save
        </v-btn>
      </v-toolbar-items>
    </v-toolbar>
    <v-card-text>
      <v-form>
        <v-container grid-list-lg>
          <v-layout row wrap>
            <v-flex xs12 sm6>
              <v-select
                v-model="division.ageType"
                :items="ageTypeOptions"
                item-text="name"
                item-value="id"
                label="Age Group"
                autocomplete
                required
                return-object>
              </v-select>
            </v-flex>
            <v-flex xs12 sm6>
              <v-select
                v-model="division.gender"
                :items="genders"
                item-text="name"
                item-value="id"
                label="Gender"
                autocomplete
                required
                return-object
              >
              </v-select>
            </v-flex>
            <v-flex xs12 sm6>
              <v-select
                v-model="division.division"
                :items="divisions"
                item-text="name"
                item-value="id"
                label="Division"
                autocomplete
                required
                return-object	
              >
              </v-select>
            </v-flex>
            <v-flex xs12 sm6>
              <v-select
                v-model="division.sanctioningBodyId"
                :items="sanctionOptions"
                label="Sanctioning Body"
                autocomplete
                clearable
              >
              </v-select>
            </v-flex>
          </v-layout>
          <v-layout row wrap>
            <v-flex xs12 sm6>
              <v-text-field
                label="Min Teams"
                v-model="division.minTeams"
              ></v-text-field>
            </v-flex>
            <v-flex xs12 sm6>
              <v-text-field
                label="Max Teams"
                v-model="division.maxTeams"
              ></v-text-field>
            </v-flex>
          </v-layout>
          <v-layout row wrap justify-center>
            <v-btn color="color3 white--text" @click.native.stop="save">
              <v-icon class="mr-2">save</v-icon>
              Save
            </v-btn>
            <v-btn class="red--text text--darken-4" @click.native.stop="cancel">
              <v-icon class="mr-2">close</v-icon>
              Cancel
            </v-btn>
          </v-layout>
        </v-container>
      </v-form>
    </v-card-text>
  </v-card>
</template>

<script>
import { mapGetters } from 'vuex'
import RegWindow from './RegistrationWindow.vue'
import * as actions from '../../../store/ActionTypes'

export default {
  props: ['division'],
  data () {
    return {
      date: false,
      checkin: false,
      play: false
    }
  },
  computed: {
    ...mapGetters([
      'ageTypeOptions',
      'genderOptions',
      'divisionOptions',
      'locationOptions'
    ]),
    sanctionOptions () {
      return ['AAU', 'AVP Tier 4', 'AVP Next']
    },
    genders () {
      if (!this.division.ageType) return this.genderOptions

      return this.genderOptions.filter((opt) => { return opt.ageTypeId === this.division.ageType.id })
    },
    divisions () {
      if (!this.division.ageType) return this.divisionOptions

      return this.divisionOptions.filter((opt) => { return opt.ageTypeId === this.division.ageType.id })
    },
    title () {
      return this.division && this.division.id > 0 ? 'Edit Division' : 'Add New Division'
    },
    shortText () {
      let bp = this.$vuetify.breakpoint.name
      if (bp === 'xs') {
        return true
      }
      return false
    }
  },
  methods: {
    save () {
      this.$emit('save')
    },
    cancel () {
      this.$emit('cancel')
    },
    selectAge (ageTypeId) {
      console.log('selectAge Id: ' + ageTypeId)
      if (this.division.ageType && this.division.ageType.Id === ageTypeId) return

      this.division.ageType = this.ageTypeOptions.find((opt) => { return opt.id === ageTypeId })
    },
    formatDate (date) {
      if (!date) return null

      const [year, month, day] = date.split('-')
      return `${month}/${day}/${year}`
    },
    parseDate (date) {
      if (!date) return null

      const [month, day, year] = date.split('/')
      return `${year}-${month.padStart(2, '0')}-${day.padStart(2, '0')}`
    }
  },
  watch: {
    'division.ageType': {
      handler: function (val) {
        var vm = this
        if (vm.division.gender && vm.division.gender.ageTypeId !== val.id) {
          vm.division.gender = null
        }
        if (vm.division.division && vm.division.division.ageTypeId !== val.id) {
          vm.division.division = null
        }
      },
      deep: true
    },
    'division.gender': {
      handler: function (val) {
        this.selectAge(val.ageTypeId)
      },
      deep: true
    },
    'division.division': {
      handler: function (val) {
        this.selectAge(val.ageTypeId)
      },
      deep: true
    }
  },
  components: {
    'registration-window': RegWindow
  },
  created () {
    this.$store.dispatch(actions.LOAD_SELECT_OPTIONS)
  }
}
</script>

<style>

</style>
