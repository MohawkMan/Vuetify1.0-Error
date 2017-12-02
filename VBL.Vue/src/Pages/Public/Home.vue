<template>
    <v-app>
        <div>
            <p>Dashboard</p>
        </div>
        <v-container>
            <v-layout row>
                <v-flex>
                    <v-card>
                        <v-card-text>
                            Results: {{ result }}<br>
                            Auth: {{ isAuthenticated }}<br>
                        </v-card-text>
                        <v-card-actions>
                            <v-btn @click="fetch('test1')">
                                Fetch Test 1
                            </v-btn>
                            <v-btn @click="fetch('test2')">
                                Fetch Test 2
                            </v-btn>
                            <v-btn @click="login">
                                Sign In
                            </v-btn>
                        </v-card-actions>
                    </v-card>
                </v-flex>
            </v-layout>
        </v-container>
    </v-app>
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
