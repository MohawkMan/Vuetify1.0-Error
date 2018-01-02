<template>
  <v-container fill-height>
    <v-layout row wrap align-center>
      <v-flex xs8 offset-xs2>
        <v-layout row wrap text-xs-center>
          <v-flex xs12>
            <h3>{{mode}}</h3>
          </v-flex>
          <v-flex xs12 v-if="mode==='Loading'">
            <v-progress-linear v-bind:indeterminate="true"></v-progress-linear>
          </v-flex>
          <v-flex xs12>
            <v-alert
              color="success"
              icon="check_circle"
              :value="mode==='Confirmed'"
              transition="scale-transition"
              >
              Thank you! Your email is now confirmed.
            </v-alert>
          </v-flex>
          <v-flex xs12>
            <v-alert
              color="error"
              icon="warning"
              :value="mode==='Error'"
              transition="scale-transition"
              >
              {{errorText}}
            </v-alert>
          </v-flex>
        </v-layout>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import vbl from '../../VolleyballLife'

export default {
  props: ['emailId', 'token'],
  data () {
    return {
      mode: 'Loading',
      errorText: ''
    }
  },
  methods: {
    confirmEmail () {
      this.axios.post(vbl.email.confirm(this.emailId), {access_token: this.token})
        .then((response) => {
          if (response.data) {
            this.mode = 'Confirmed'
          } else {
            this.errorText = 'Invalid Token'
            this.mode = 'Error'
          }
        })
        .catch((response) => {
          console.log(response.data)
          this.errorText = 'We apologize but there was a communication error. Please try refreashing the page'
          this.mode = 'Error'
        })
    }
  },
  created () {
    this.confirmEmail()
  }
}
</script>

<style>

</style>
