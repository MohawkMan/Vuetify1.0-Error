<template>
  <v-container>
    <!-- Name -->
    <v-layout row wrap>
      <v-flex xs12 sm6 md4>
        <v-text-field
          name="firstName"
          label="First Name"
          v-model="player.firstName"
          required
          :rules="[() => $v.player.firstName.required || 'A first name is required']"
        ></v-text-field>
      </v-flex>
      <v-flex xs12 sm6 md4>
        <v-text-field
          name="lastName"
          label="Last Name"
          v-model="player.lastName"
          required
          :rules="[() => $v.player.lastName.required || 'A last name is required']"
        ></v-text-field>
      </v-flex>
    </v-layout>
    <!-- Phone/Email -->
    <v-layout row wrap>
      <v-flex xs12 sm6 md4>
        <v-text-field
          name="phone"
          label="Phone"
          v-model="player.phone"
          mask="phone"
          required
          :rules="[
            () => $v.player.phone.required || 'A phone number is required',
            () => $v.player.phone.minLength || 'A valid phone number is required',
            () => $v.player.phone.maxLength || 'A valid phone number is required'
          ]"
        ></v-text-field>
      </v-flex>
      <v-flex xs12 sm6 md4>
        <v-text-field
          name="email"
          label="Email"
          v-model="player.email"
          required
          :rules="[
            () => $v.player.email.required || 'An email is required',
            () => $v.player.email.email || 'A valid email is required'
          ]"
        ></v-text-field>
      </v-flex>
    </v-layout>
    <!-- City State -->
    <v-layout row wrap>
      <v-flex xs12 sm6 md4>
        <v-text-field
          name="city"
          label="City"
          v-model="player.city"
          required
          :rules="[() => $v.player.city.required || 'A city is required']"
        ></v-text-field>
      </v-flex>
      <v-flex xs12 sm6 md4>
        <v-select
          label="State"
          v-bind:items="states"
          item-text="name"
          item-value="abbreviation"
          v-model="player.state"
          single-line
          autocomplete
          :hint="player.state"
          persistent-hint
          required
          :rules="[() => $v.player.state.required || 'A state is required']"
        ></v-select>
      </v-flex>
    </v-layout>
    <!-- DOB -->
    <v-layout row wrap>
      <v-flex xs12 sm6 md4 ref="datePickerFlex">
        <v-menu
          ref="dateMenu"
          lazy
          :close-on-content-click="false"
          v-model="dobPicker"
          transition="scale-transition"
          offset-y
          full-width
          :nudge-right="40"
          max-width="290px"
          min-width="290px"
          @input="test"
        >
          <v-text-field
            ref="dateTextField"
            slot="activator"
            label="Birthdate"
            readonly
            v-model="player.dobFormatted"
            required
            :rules="[() => $v.player.dob.required || 'A birthdate is required']"
            @blur="player.dob = parseDate(player.dobFormatted)"
          ></v-text-field>
          <v-date-picker
            ref="datePicker"
            no-title
            scrollable
            actions
            v-model="player.dob"
            @input="player.dobFormatted = formatDate($event)"
          >
            <template slot-scope="{ save, cancel }">
              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn flat color="primary" @click="cancel">Cancel</v-btn>
                <v-btn flat color="primary" @click="save">OK</v-btn>
              </v-card-actions>
            </template>
          </v-date-picker>
        </v-menu>
      </v-flex>
    </v-layout>
    <!-- CBVA/USAV -->
    <v-layout row wrap>
      <v-flex xs12 sm6 md4>
        <v-text-field
          name="cbva"
          label="CBVA Number"
          v-model="player.cbva"
          hint="Not required but used for seeding"
          persistent-hint
        ></v-text-field>
      </v-flex>
      <v-flex xs12 sm6 md4>
        <v-text-field
          name="usav"
          label="USAV Number"
          v-model="player.usav"
          hint="Not required but used for seeding"
          persistent-hint
        ></v-text-field>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import States from '../../json/states.json'
import { validationMixin } from 'vuelidate'
import { required, numeric, minLength, maxLength, email } from 'vuelidate/lib/validators'

export default {
  props: ['fields', 'requiredFields', 'player'],
  mixins: [validationMixin],
  validations: {
    player: {
      firstName: { required },
      lastName: { required },
      phone: { required, numeric, minLength: minLength(10), maxLength: maxLength(10) },
      email: { required, email },
      dob: { required },
      city: { required },
      state: { required }
    }
  },
  data () {
    return {
      states: States,
      dobPicker: false
    }
  },
  computed: {
    valid () { return !this.$v.$invalid }
  },
  methods: {
    formatDate (date) {
      if (!date) return null

      const [year, month, day] = date.split('-')
      return `${month}/${day}/${year}`
    },
    parseDate (date) {
      if (!date) return null

      const [month, day, year] = date.split('/')
      return `${year}-${month.padStart(2, '0')}-${day.padStart(2, '0')}`
    },
    test () {
      // console.log(this.$refs.datePicker)
      // this.$refs.datePicker.activePicker = 'YEAR'
    }
  },
  watch: {
    '$v.$invalid': function () {
      this.player.valid = !this.$v.$invalid
      this.$v.$invalid ? this.$emit('invalid') : this.$emit('valid')
    }
  }
}
</script>

<style>

</style>
