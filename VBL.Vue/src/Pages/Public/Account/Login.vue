<template>
  <v-container grid-list-md>
    <page-title :title="'Welcome to Volleyball Life'"></page-title>
    <v-layout row wrap>
      <v-flex xs12 sm6 offset-sm3>
        <v-card>
          <v-card-text>
            <v-layout row wrap>
              <v-flex text-xs-center>
                <h2>Sign in using</h2>
              </v-flex>
            </v-layout>
            <v-layout row wrap justify-center>
              <v-flex xs2 text-xs-center fill-height>
                <v-avatar class="blue">
                  <span class="white--text">F</span>
                </v-avatar>
              </v-flex>
              <v-flex xs2 text-xs-center>
                <v-avatar class="blue">
                  <span class="white--text">T</span>
                </v-avatar>
              </v-flex>
              <v-flex xs2 text-xs-center>
                <v-avatar class="red">
                  <span class="white--text">g</span>
                </v-avatar>
              </v-flex>
            </v-layout>
            <v-layout row wrap my-4>
              <v-flex text-xs-center xs12 sm8 offset-sm2>
                <h2 class="lined"><span>Or</span></h2>
              </v-flex>
            </v-layout>
            <v-layout row wrap>
              <v-flex xs12 sm6 offset-sm3 text-xs-center>
                <v-form v-model="valid" ref="form" lazy-validation>
                  <v-text-field
                    label="Email, phone, or username"
                    v-model="username"
                    :rules="[v => !!v || 'Phone, email, or username is required']"
                    required
                  ></v-text-field>
                  <v-text-field
                    label="Enter a password"
                    v-model="password"
                    hint="At least 8 characters"
                    :append-icon="hidePassword ? 'visibility' : 'visibility_off'"
                    :append-icon-cb="() => (hidePassword = !hidePassword)"
                    :type="hidePassword ? 'password' : 'text'"
                    required
                    :rules="[v => !!v || 'Password is required']"
                  >
                  </v-text-field>
                  <v-btn :disabled="loading" color="color3" @click="login">
                  Log In
                  </v-btn>
                </v-form>
              </v-flex>
            </v-layout>
            <v-layout row wrap text-xs-center mt-5>
              <v-flex xs12>
                <h4>New to Volleyball Life?</h4>
              </v-flex>
              <v-flex xs12 text-xs-center>
                <v-btn :disabled="loading" router to="/join">
                  Register
                </v-btn>
              </v-flex>
            </v-layout>
          </v-card-text>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import * as actions from '../../../store/ActionTypes'
import { mapGetters } from 'vuex'

export default {
  data: () => ({
    username: '',
    password: '',
    hidePassword: true,
    valid: false
  }),
  computed: {
    ...mapGetters([
      'loading'
    ])
  },
  methods: {
    login () {
      if (this.$refs.form.validate()) {
        // do the post
        let user = {
          username: this.username,
          password: this.password
        }
        this.$store.dispatch(actions.LOGIN, { user })
          .then(() => {
            let goto = this.$route.query.returnURL || 'me'
            this.$router.push(goto)
          })
      }
    }
  }
}
</script>
<style scoped>
h2.lined {
  border-bottom: 1px solid grey;
  line-height: 0.1em;
}
h2.lined span {
  padding: 0 10px;
  background: #fff
}
</style>
