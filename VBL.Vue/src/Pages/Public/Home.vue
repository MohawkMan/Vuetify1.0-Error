<template>
  <v-container grid-list-md>
    <v-layout row wrap>
      <v-flex xs12>
        Welcome to VolleyballLife.com
        <p>Welcome to VolleyballLife.com</p>
        <p>Welcome to VolleyballLife.com</p>
        <p>Welcome to VolleyballLife.com</p>
        <p>Welcome to VolleyballLife.com</p>
        <p>Welcome to VolleyballLife.com</p>
        <p>Welcome to VolleyballLife.com</p>
        <p>Welcome to VolleyballLife.com</p>
        <p>Welcome to VolleyballLife.com</p>
        <p>Welcome to VolleyballLife.com</p>
        <p>Welcome to VolleyballLife.com</p>
      </v-flex>
      <v-flex xs12>
        <v-parallax src="/static/sunset-volleyball4.jpg"></v-parallax>
      </v-flex>
      <v-flex xs12>
        <p>Welcome to VolleyballLife.com</p>
        <p>Welcome to VolleyballLife.com</p>
        <p>Welcome to VolleyballLife.com</p>
        <p>Welcome to VolleyballLife.com</p>
        <p>Welcome to VolleyballLife.com</p>
        <p>Welcome to VolleyballLife.com</p>
        <p>Welcome to VolleyballLife.com</p>
        <p>Welcome to VolleyballLife.com</p>
        <p>Welcome to VolleyballLife.com</p>
        <p>Welcome to VolleyballLife.com</p>
        <p>Welcome to VolleyballLife.com</p>
        <p>Welcome to VolleyballLife.com</p>
        <p>Welcome to VolleyballLife.com</p>
        <p>Welcome to VolleyballLife.com</p>
        <p>Welcome to VolleyballLife.com</p>
        <p>Welcome to VolleyballLife.com</p>
        <p>Welcome to VolleyballLife.com</p>
        <p>Welcome to VolleyballLife.com</p>
        <p>Welcome to VolleyballLife.com</p>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import * as actions from '../../store/ActionTypes'

export default {
  data: () => ({
    result: ''
  }),
  computed: {
    isAuthenticated () {
      return this.$auth.isAuthenticated()
    }
  },
  methods: {
    fetch (endpoint) {
      this.axios.get('https://localhost:44351/api/Account/' + endpoint).then((resp) => {
        this.result = resp.data
      })
    },
    login () {
      let user = {
        username: '2146748568',
        password: 'volley13'
      }
      this.$store.dispatch(actions.LOGIN, { user })
    }
  }
}
// Cap Lock: http://jsfiddle.net/Mottie/a6nhqvv0/
</script>
