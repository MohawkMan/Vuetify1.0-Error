<template>
  <v-container>
    <v-layout row wrap v-if="true">
      <v-flex xs12 sm6 md4>
        <v-text-field
          v-if="false"
          name="vblId"
          label="Quick Register Id"
          v-model="player.vblId"
          hint="VolleyballLife Quick Register Id"
          persistent-hint
        ></v-text-field>
      </v-flex>
    </v-layout>
    <!-- Sanctioning Body -->
    <v-layout row wrap v-if="!!sanctioningBodyId">
      <!-- AAU -->
      <v-flex xs12 sm6 md4 v-if="sanctioningBody.toLowerCase() === 'aau'">
        <v-text-field
          name="aau"
          label="AAU Number"
          v-model="player.aau"
          :hint="requiredFields.includes('aau') ? '' : 'Not required but used for seeding'"
          persistent-hint
          validate-on-blur
          :required="requiredFields.includes('aau')"
          :rules="requiredFields.includes('aau') ? [() => $v.player.aau.required || 'An AAU Number is required'] : []"
          @blur="sbVerifyCheck"
        ></v-text-field>
      </v-flex>
      <!-- AVP -->
      <v-flex xs12 sm6 md4 v-if="sanctioningBody.toLowerCase() === 'avp'">
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
      <!-- USAV -->
      <v-flex xs12 sm6 md4 v-if="sanctioningBody.toLowerCase() === 'usav'">
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
      <!-- CBVA -->
      <v-flex xs12 sm6 md4 v-if="sanctioningBody.toLowerCase() === 'cbva'">
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
          @blur="sbVerifyCheck"
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
      <v-flex xs12 sm6 md4>
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
          v-if="false"
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
        <v-text-field
          ref="dateTextField"
          label="Birthdate"
          v-model="dob"
          mask="##/##/####"
          :required="requiredFields.includes('dob')"
          :rules="requiredFields.includes('dob') ? [() => $v.player.dob.required || 'A birthdate is required'] : []"
          @change="setDob"
          validate-on-blur
        ></v-text-field>
      </v-flex>
    </v-layout>
    <!-- Club -->
    <v-layout row wrap v-if="_fields.includes('club')">
      <v-flex xs12 sm6 md4>
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
    <v-layout row wrap>
      <!-- AAU -->
      <v-flex xs12 sm6 md4 v-if="_fields.includes('aau') && sanctioningBody.toLowerCase() !== 'aau'">
        <v-text-field
          name="aau"
          label="AAU Number"
          v-model="player.aau"
          :hint="requiredFields.includes('aau') ? '' : 'Not required but used for seeding'"
          persistent-hint
          validate-on-blur
          :required="requiredFields.includes('aau')"
          :rules="requiredFields.includes('aau') ? [() => $v.player.aau.required || 'An AAU Number is required'] : []"
        ></v-text-field>
      </v-flex>
      <!-- AVP -->
      <v-flex xs12 sm6 md4 v-if="_fields.includes('avp') && sanctioningBody.toLowerCase() !== 'avp'">
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
      <!-- USAV -->
      <v-flex xs12 sm6 md4 v-if="_fields.includes('usav') && sanctioningBody.toLowerCase() !== 'usav'">
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
      <!-- CBVA -->
      <v-flex xs12 sm6 md4 v-if="_fields.includes('cbva') && sanctioningBody.toLowerCase() !== 'cbva'">
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
    <v-layout row wrap v-if="verifying">
      <v-flex xs12 sm6 md4 class="color1--text">
        <v-btn icon color="color1 white--text" :loading="true">
          <v-icon>check_circle</v-icon>
        </v-btn>
        Verifying {{ sanctioningBody }} Number
      </v-flex>
    </v-layout>
    <template v-else-if="aau.dto.id">
      <v-layout row wrap v-if="aau.valid">
        <v-flex xs12 sm6 md4 class="green--text">
          <v-btn icon color="green white--text">
            <v-icon>check_circle</v-icon>
          </v-btn>
          {{ sanctioningBody }} Number Confirmed
        </v-flex>
      </v-layout>
      <v-layout row wrap v-if="!aau.valid">
        <v-flex xs12 sm6 md4 class="red--text">
          <v-btn icon color="red white--text">
            <v-icon>error</v-icon>
          </v-btn>
          Invalid {{ sanctioningBody }} Number
        </v-flex>
      </v-layout>
      <v-layout row wrap v-if="!aau.valid">
        <v-flex xs12 sm6 md4>
          <v-alert outline color="error" icon="warning" :value="true">
            The {{sanctioningBody}} number and last name you provided do not match. 
            Please double check the number and the spelling of the last name.
            If you believe both are correct or you do not have the correct information now
            please check the box below.
          </v-alert>
        </v-flex>
      </v-layout>
      <v-layout row wrap v-if="!aau.valid">
        <v-flex xs12>
          <v-checkbox 
            v-model="skipVerification" 
            :label="`I will provide a valid ${sanctioningBody} number at the tournamen check-in`"
            @change="setValid"
          ></v-checkbox>
        </v-flex>
      </v-layout>
    </template>
  </v-container>
</template>

<script>
import States from '../../json/states.json'
import { validationMixin } from 'vuelidate'
import { required, numeric, minLength, maxLength, email } from 'vuelidate/lib/validators'
import SDK from '../../VBL'

export default {
  props: ['fields', 'requiredFields', 'player', 'sanctioningBodyId'],
  mixins: [validationMixin],
  validations () {
    return {
      player: {
        firstName: { required },
        lastName: { required },
        phone: this.requiredFields.includes('phone') ? { required, numeric, minLength: minLength(10), maxLength: maxLength(10) } : {},
        email: this.requiredFields.includes('email') ? { required, email } : {},
        city: this.requiredFields.includes('cityState') ? { required } : {},
        state: this.requiredFields.includes('cityState') ? { required } : {},
        dob: this.requiredFields.includes('dob') ? { required } : {},
        club: this.requiredFields.includes('club') ? { required } : {},
        aau: this.requiredFields.includes('aau') ? { required } : {},
        avp: this.requiredFields.includes('avp') ? { required } : {},
        usav: this.requiredFields.includes('usav') ? { required } : {},
        cbva: this.requiredFields.includes('cbva') ? { required } : {}
      }
    }
  },
  data () {
    return {
      states: States,
      dobPicker: false,
      verifying: false,
      aau: {
        dto: {
          id: '',
          lastname: '',
          zipcode: '',
          dob: '',
          by: 'lastname'
        },
        valid: false
      },
      skipVerification: false
      dob: null
    }
  },
  computed: {
    valid () { return !this.$v.$invalid },
    _fields () {
      return this.fields.concat(this.requiredFields)
    },
    verified () {
      if (!this.sanctioningBody) return true
      if (this.verifying) return false
      return this.aau.valid || this.skipVerification
    },
    sanctioningBody () {
      return this.sanctioningBodyId ? this.sanctioningBodyId.toLowerCase().startsWith('avp') ? 'AVP' : this.sanctioningBodyId : ''
    }
  },
  methods: {
    setDob () {
      if (!this.dob || this.dob.length !== 8) {
        this.player.dobFormatted = null
        this.player.dob = null
      } else {
        const month = this.dob.substring(0, 2)
        const day = this.dob.substring(2, 4)
        const year = this.dob.substring(4, 8)
        this.player.dobFormatted = `${month}/${day}/${year}`
        this.player.dob = `${year}-${month}-${day}`
      }
      this.$v.player.dob.$touch()
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
    },
    sbVerifyCheck () {
      console.log('calling sbVerifyCheck')
      if (!this.sanctioningBody) {
        console.log('No Sanctioning Body')
        return
      }
      if (this.sanctioningBody.toLowerCase() === 'aau') {
        console.log('Sanctioning Body is AAU')
        if (this.player.aau && this.player.lastName) {
          console.log('Has AAU and LastName')
          if (this.player.aau !== this.aau.dto.id || this.player.lastName !== this.aau.dto.lastname) {
            console.log('AAU or LastName has changed')
            this.reset()
            this.aauValidate()
          }
        }
      }
    },
    reset () {
      this.skipVerification = false
      this.aau.valid = false
      this.aau.dto.id = this.player.aau
      this.aau.dto.lastname = this.player.lastName
    },
    aauValidate () {
      this.verifying = true
      const sdk = new SDK(this.axios)
      sdk.verifyAAU(this.aau.dto)
        .then((response) => {
          this.aau.valid = response.data
          this.verifying = false
        })
        .catch((error) => {
          console.log(error.response.data)
          this.verifying = false
        })
    },
    setValid () {
      this.player.valid = !this.$v.$invalid && this.verified
    }
  },
  watch: {
    '$v.$invalid': function () {
      this.setValid()
    },
    verified: function () {
      this.setValid()
    }
  }
}
</script>

<style>

</style>
