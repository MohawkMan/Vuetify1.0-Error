<template>
  <v-container>
    <page-title title="My Profile"></page-title>
    <v-layout row wrap>
      <v-flex xs12 sm8 offset-sm2>
        <h1>{{fullname}}</h1>
        <v-card id="create">
          <v-toolbar dark color="color2">
            <v-toolbar-title>Contact Info</v-toolbar-title>
          </v-toolbar>
          <v-container fluid grid-list-md class="p6">
            <v-layout child-flex wrap>
              <v-flex xs12>
                <!-- Phone List -->
                <phone-list :phonelist="user.phones" ref="phoneList"></phone-list>
                <v-divider inset></v-divider>
                <!-- Email List -->
                <v-list v-if="user.emails.length > 0">
                  <v-list-group v-for="(item,i) in user.emails" :key="i">
                    <v-list-tile slot="item">
                      <v-list-tile-action>
                        <v-icon color="color1" v-if="i === 0">
                          mail
                        </v-icon>
                      </v-list-tile-action>
                      <v-list-tile-content>
                        <v-list-tile-title>
                          {{item.address }}
                          <v-icon color="success">public</v-icon>
                          <v-icon color="success">sms</v-icon>
                        </v-list-tile-title>
                      </v-list-tile-content>
                      <v-list-tile-action>
                        <v-icon color="color3">settings</v-icon>
                      </v-list-tile-action>
                    </v-list-tile>
                    <v-list-tile>
                      <v-list-tile-content>
                        <v-list-tile-title>
                          <v-checkbox v-model="item.public" :label="'Public'" hide-details d-inline-block></v-checkbox>
                          Public
                        </v-list-tile-title>
                        <v-list-tile-sub-title>Testing</v-list-tile-sub-title>
                      </v-list-tile-content>
                    </v-list-tile>
                  </v-list-group>
                </v-list>
                <v-list v-else>
                    <v-list-tile>
                      <v-list-tile-action>
                        <v-icon color="color1">
                          mail
                        </v-icon>
                      </v-list-tile-action>
                      <v-list-tile-content>
                        <v-list-tile-title>
                          No email address on file
                        </v-list-tile-title>
                      </v-list-tile-content>
                      <v-list-tile-action>
                        <v-icon color="color3">add_circle</v-icon>
                      </v-list-tile-action>
                    </v-list-tile>
                </v-list>
                <v-divider inset></v-divider>
                <!-- Address List -->
                <v-list v-if="user.emails.length > 0" class="mb-3">
                  <v-list-group v-for="(item,i) in user.emails" :key="i">
                    <v-list-tile slot="item">
                      <v-list-tile-action>
                        <v-icon color="color1" v-if="i === 0">
                          location_on
                        </v-icon>
                      </v-list-tile-action>
                      <v-list-tile-content>
                        <v-list-tile-title>
                          {{item.address }}
                          <v-icon color="success">public</v-icon>
                          <v-icon color="success">sms</v-icon>
                        </v-list-tile-title>
                      </v-list-tile-content>
                      <v-list-tile-action>
                        <v-icon color="color3">settings</v-icon>
                      </v-list-tile-action>
                    </v-list-tile>
                    <v-list-tile>
                      <v-list-tile-content>
                        <v-list-tile-title>
                          <v-checkbox v-model="item.public" :label="'Public'" hide-details d-inline-block></v-checkbox>
                          Public
                        </v-list-tile-title>
                        <v-list-tile-sub-title>Testing</v-list-tile-sub-title>
                      </v-list-tile-content>
                    </v-list-tile>
                  </v-list-group>
                </v-list>
                <v-list v-else class="mb-3">
                    <v-list-tile>
                      <v-list-tile-action>
                        <v-icon color="color1">
                          location_on
                        </v-icon>
                      </v-list-tile-action>
                      <v-list-tile-content>
                        <v-list-tile-title>
                          No address on file
                        </v-list-tile-title>
                      </v-list-tile-content>
                      <v-list-tile-action>
                        <v-icon color="color3">add_circle</v-icon>
                      </v-list-tile-action>
                    </v-list-tile>
                </v-list>
              </v-flex>
            </v-layout>
          </v-container>
          <v-speed-dial
            v-model="fab"
            bottom
            left
            direction="right"
          >
            <v-btn 
              slot="activator" 
              :color="fab ? 'red darken-4' : 'color4'" 
              dark
              fab
              hover
              v-model="fab"
            >
              <v-icon>add</v-icon>
              <v-icon>close</v-icon>
            </v-btn>
            <v-btn small @click="addPhone">
              <v-icon>local_phone</v-icon>
              Add a Phone
            </v-btn>
            <v-btn small>
              <v-icon>mail</v-icon>
              Add an email
            </v-btn>
            <v-btn small>
              <v-icon>location_on</v-icon>
              Add an Address
            </v-btn>
          </v-speed-dial>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
  
</template>

<script>
import { mapGetters } from 'vuex'
import PhoneList from '../../../components/Utils/PhoneList.vue'

export default {
  data () {
    return {
      fab: false
    }
  },
  computed: {
    ...mapGetters([
      'user',
      'loading'
    ]),
    fullname () {
      let middle = this.user.middleName ? ' ' + this.user.middleName + ' ' : ' '
      return this.user.firstName + middle + this.user.lastName
    }
  },
  components: {
    'phone-list': PhoneList
  },
  methods: {
    addPhone () {
      this.$refs.phoneList.addPhone()
    }
  }
}
</script>

<style scoped>
#create .speed-dial {
  position: absolute;
}

#create .btn--floating {
  position: relative;
}

.p6 {
  padding-bottom: 64px;
}
</style>
