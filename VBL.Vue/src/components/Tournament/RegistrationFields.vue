<template>
  <v-container>
    <v-layout row wrap>
      <v-flex xs12 sm6 md4>
        <v-text-field
          name="vblId"
          label="Quick Register Id"
          v-model="player.vblId"
          hint="VolleyballLife Quick Register Id"
          persistent-hint
        ></v-text-field>
      </v-flex>
    </v-layout>
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
      <v-flex xs12 sm6 md4 v-if="_fields.includes('phone')">
        <v-text-field
          name="phone"
          label="Phone"
          v-model="player.phone"
          mask="phone"
          validate-on-blur
          :required="requiredFields.includes('phone')"
          :rules="requiredFields.includes('phone') ? [
            () => $v.player.phone.required || 'A phone number is required',
            () => $v.player.phone.minLength || 'A valid phone number is required',
            () => $v.player.phone.maxLength || 'A valid phone number is required'
          ] : []"
        ></v-text-field>
      </v-flex>
      <v-flex xs12 sm6 md4 v-if="_fields.includes('email')">
        <v-text-field
          name="email"
          label="Email"
          v-model="player.email"
          validate-on-blur
          :required="requiredFields.includes('email')"
          :rules="requiredFields.includes('email') ? [
            () => $v.player.email.required || 'An email is required',
            () => $v.player.email.email || 'A valid email is required'
          ] : []"
        ></v-text-field>
      </v-flex>
    </v-layout>
    <!-- City State -->
    <v-layout row wrap v-if="_fields.includes('cityState')">
      <v-flex xs12 sm6 md4>
        <v-text-field
          name="city"
          label="City"
          v-model="player.city"
          validate-on-blur
          :required="requiredFields.includes('cityState')"
          :rules="requiredFields.includes('cityState') ? [() => $v.player.city.required || 'A city is required'] : []"
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
          validate-on-blur
          :required="requiredFields.includes('cityState')"
          :rules="requiredFields.includes('cityState') ? [() => $v.player.state.required || 'A state is required'] : []"
        ></v-select>
      </v-flex>
    </v-layout>
    <!-- DOB -->
    <v-layout row wrap v-if="_fields.includes('dob')">
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
            :required="requiredFields.includes('dob')"
            :rules="requiredFields.includes('dob') ? [() => $v.player.dob.required || 'A birthdate is required'] : []"
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
    <!-- Club -->
    <v-layout row wrap v-if="_fields.includes('club')">
      <v-flex xs12 sm6 md4 ref="datePickerFlex">
        <v-text-field
          name="club"
          label="Club"
          v-model="player.club"
          hint="What club are you representing?"
          persistent-hint
          validate-on-blur
          :required="requiredFields.includes('club')"
          :rules="requiredFields.includes('club') ? [() => $v.player.club.required || 'A club is required'] : []"
        ></v-text-field>
      </v-flex>
    </v-layout>
    <!-- AAU/AVP -->
    <v-layout row wrap>
      <v-flex xs12 sm6 md4 v-if="_fields.includes('aau')">
        <v-text-field
          name="aau"
          label="AAU Number"
          v-model="player.aau"
          hint="Not required but used for seeding"
          persistent-hint
          validate-on-blur
          :required="requiredFields.includes('aau')"
          :rules="requiredFields.includes('aau') ? [() => $v.player.aau.required || 'An AAU Number is required'] : []"
        ></v-text-field>
      </v-flex>
      <v-flex xs12 sm6 md4 v-if="_fields.includes('avp')">
        <v-text-field
          name="avp"
          label="AVP Number"
          v-model="player.avp"
          hint="Not required but used for seeding"
          persistent-hint
          validate-on-blur
          :required="requiredFields.includes('avp')"
          :rules="requiredFields.includes('avp') ? [() => $v.player.avp.required || 'An AVP number is required'] : []"
        ></v-text-field>
      </v-flex>
    </v-layout>
    <!-- CBVA/USAV -->
    <v-layout row wrap>
      <v-flex xs12 sm6 md4 v-if="_fields.includes('usav')">
        <v-text-field
          name="usav"
          label="USAV Number"
          v-model="player.usav"
          hint="Not required but used for seeding"
          persistent-hint
          validate-on-blur
          :required="requiredFields.includes('usav')"
          :rules="requiredFields.includes('usav') ? [() => $v.player.usav.required || 'A USAV number is required'] : []"
        ></v-text-field>
      </v-flex>
      <v-flex xs12 sm6 md4 v-if="_fields.includes('cbva')">
        <v-text-field
          name="cbva"
          label="CBVA Number"
          v-model="player.cbva"
          hint="Not required but used for seeding"
          persistent-hint
          validate-on-blur
          :required="requiredFields.includes('cbva')"
          :rules="requiredFields.includes('cbva') ? [() => $v.player.cbva.required || 'A CBVA number is required'] : []"
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
  validations () {
    return {
      player: {
        firstName: { required },
        lastName: { required },
        phone: this.requiredFields.includes('phone') ? { required, numeric, minLength: minLength(10), maxLength: maxLength(10) } : {},
        email: { required, email },
        dob: { required },
        city: { required },
        state: { required }
      }
    }
  },
  data () {
    return {
      states: States,
      dobPicker: false
    }
  },
  computed: {
    valid () { return !this.$v.$invalid },
    _fields () {
      return this.fields.concat(this.requiredFields)
    }
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
