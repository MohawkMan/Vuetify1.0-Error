<template>
  <v-container>
    <page-title :title="'VolleyballLife Registration'"></page-title>
    <v-layout row wrap>
      <v-flex xs12 sm6 offset-sm3>
        <v-card>
          <v-card-title class="primary white--text">
            <h2>Create New User</h2>
          </v-card-title>
          <v-card-text>
            <v-form v-model="valid" ref="form" lazy-validation>
              <v-text-field
                label="Enter you email"
                v-model="email"
                :error-messages="emailErrors"
                @input="$v.email.$touch()"
                @blur="$v.email.$touch()"
                required
              ></v-text-field>
              <v-text-field
                label="Enter a password"
                v-model="password"
                hint="At least 8 characters"
                min="8"
                :append-icon="hidePassword ? 'visibility' : 'visibility_off'"
                :append-icon-cb="() => (hidePassword = !hidePassword)"
                :type="hidePassword ? 'password' : 'text'"
                :error-messages="passwordErrors"
                @input="$v.password.$touch()"
                @blur="$v.password.$touch()"
                required
              >
              </v-text-field>
              <v-text-field
                label="Confirm your password"
                v-model="passwordConfirm"
                :append-icon="hidePasswordConfirm ? 'visibility' : 'visibility_off'"
                :append-icon-cb="() => (hidePasswordConfirm = !hidePasswordConfirm)"
                :type="hidePasswordConfirm ? 'password' : 'text'"
                :error-messages="passwordConfirmErrors"
                @input="$v.passwordConfirm.$touch()"
                @blur="$v.passwordConfirm.$touch()"
                required
              >
              </v-text-field>
              <v-layout>
                <v-flex text-xs-center>
                  <v-btn
                  color="color3"
                  :disabled="!valid">
                  Join
              </v-btn>
                </v-flex>
              </v-layout>
            </v-form>
          </v-card-text>
       </v-card>
      </v-flex>
    </v-layout>
  </v-container>  
</template>

<script>
import { validationMixin } from 'vuelidate'
import { required, minLength, email, sameAs } from 'vuelidate/lib/validators'

export default {
  mixins: [validationMixin],
  validations: {
    email: { required, email },
    password: { required, minLength: minLength(8) },
    passwordConfirm: { sameAsPassword: sameAs('password') }
  },
  data: () => ({
    email: '',
    password: '',
    hidePassword: true,
    passwordConfirm: '',
    hidePasswordConfirm: true
  }),
  computed: {
    emailErrors () {
      const errors = []
      if (!this.$v.email.$dirty) return errors
      !this.$v.email.email && errors.push('Must be valid e-mail')
      !this.$v.email.required && errors.push('E-mail is required')
      return errors
    },
    passwordErrors () {
      const errors = []
      if (!this.$v.password.$dirty) return errors
      !this.$v.password.required && errors.push('A password is required')
      !this.$v.password.minLength && errors.push('Your password must be at least 8 characters')
      return errors
    },
    passwordConfirmErrors () {
      const errors = []
      if (!this.$v.passwordConfirm.$dirty) return errors
      !this.$v.passwordConfirm.sameAsPassword && errors.push('Your passwords don\'t match')
      return errors
    },
    valid () { return !this.$v.$invalid }
  },
  methods: {
    submit () {
      if (this.$refs.form.validate()) {
        // do the post
        alert('valid')
      }
    }
  }
}
</script>
