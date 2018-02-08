<template>
  <v-container>
    <v-layout row>
      <v-flex xs12 sm10 offset-sm1>
        <v-card>
          <v-toolbar color="color2 white--text">
            <v-toolbar-title>Share You Feedback With Us</v-toolbar-title>
          </v-toolbar>
          <v-card-text>
            <v-layout row>
              <v-flex>
                <h2>We would love to hear your thoughts, concerns or problems with anything so we can improve for you!</h2>
              </v-flex>
            </v-layout>
            <v-layout row>
              <v-flex>
                <v-text-field
                  label="Your name"
                  v-model="name"
                  required
                  :rules="[
                    () => $v.name.required || 'A name is required'
                  ]"
                ></v-text-field>
              </v-flex>
            </v-layout>
            <v-layout row>
              <v-flex>
                <v-text-field
                  label="Your email address"
                  v-model="email"
                  required
                  :rules="[
                    () => $v.email.required || 'An email is required',
                    () => $v.email.email || 'A valid email is required'
                  ]"
                ></v-text-field>
              </v-flex>
            </v-layout>
            <v-layout row>
              <v-flex>
                <v-text-field
                  label="Subject"
                  v-model="subject"
                  required
                  :rules="[
                    () => $v.subject.required || 'A subject is required'
                  ]"
                ></v-text-field>
              </v-flex>
            </v-layout>
            <v-layout row>
              <v-flex>
                <v-text-field
                  label="Message"
                  v-model="message"
                  required
                  :rules="[
                    () => $v.message.required || 'A message is required'
                  ]"
                  multi-line
                ></v-text-field>
              </v-flex>
            </v-layout>
            <v-layout>
              <v-flex>
                <v-btn
                  color="color3 white--text"
                  :disabled="!valid"
                  @click="submit"
                >
                  Submit Feedback
                </v-btn>
                <v-btn @click="reset">Reset</v-btn>
              </v-flex>
            </v-layout>
          </v-card-text>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import { validationMixin } from 'vuelidate'
import { required, email } from 'vuelidate/lib/validators'
import SDK from '../../VBL'

export default {
  mixins: [validationMixin],
  validations () {
    return {
      name: { required },
      email: { required, email },
      subject: { required },
      message: { required }
    }
  },
  data () {
    return {
      name: null,
      email: null,
      subject: null,
      message: null,
      thanks: true
    }
  },
  computed: {
    valid () { return !this.$v.$invalid }
  },
  methods: {
    submit () {
      const sdk = new SDK(this.axios)
      sdk.putFeedback({
        name: this.name,
        email: this.email,
        subject: this.subject,
        message: this.message
      }).then((response) => {
        this.reset()
        this.$router.push({name: 'feedback-thanks'})
      })
      .catch((error) => {
        console.log(error)
      })
    },
    reset () {
      this.name = null
      this.email = null
      this.subject = null
      this.message = null
      this.$v.name.$reset()
    }
  }
}
</script>

<style>

</style>
