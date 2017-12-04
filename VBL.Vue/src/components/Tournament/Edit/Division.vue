<template>
  <v-card>
    <v-toolbar dark color="color2">
      <v-btn icon @click.native="cancel" dark small color="red darken-3">
        <v-icon>close</v-icon>
      </v-btn>        
      <v-toolbar-title>{{title}}</v-toolbar-title>
      <v-spacer></v-spacer>
      <v-toolbar-items>
        <v-btn dark flat @click.native.stop="save">
          <v-icon class="mr-2">save</v-icon>
          Save
        </v-btn>
      </v-toolbar-items>
    </v-toolbar>
    <v-card-text>
      <v-form>
        <v-container grid-list-lg>
          <v-layout row wrap>
            <v-flex xs12 sm10 offset-sm1>
              <!-- Start What -->
              <v-card>
                <v-toolbar dense flat>
                  <v-toolbar-title>
                    Division Details
                  </v-toolbar-title>
                </v-toolbar>
                <v-container>
                  <v-layout row wrap>
                    <v-flex xs6 sm4>
                      <v-select
                        v-model="division.ageType"
                        :items="ageTypeOptions"
                        item-text="name"
                        prepend-icon="wc"
                        item-value="id"
                        label="Age Group"
                        autocomplete
                        required
                        return-object>
                      </v-select>
                   </v-flex>
                    <v-flex xs6 sm4>
                      <v-select
                        v-model="division.gender"
                        :items="genderOptions"
                        item-text="name"
                        prepend-icon="wc"
                        item-value="id"
                        label="Gender"
                        autocomplete
                        required
                        return-object
                      >
                      </v-select>
                    </v-flex>
                    <v-flex xs6 sm4>
                      <v-select
                        v-model="division.division"
                        :items="divisionOptions"
                        item-text="name"
                        item-value="id"
                        prepend-icon="assignment"
                        label="Division"
                        autocomplete
                        required
                        return-object	
                      >
                      </v-select>
                    </v-flex>
                    <v-flex xs6 sm4>
                      <v-text-field
                        label="Max Teams"
                        single-line
                        hide-details
                        v-model="division.maxTeams"
                      ></v-text-field>
                    </v-flex>
                    <v-flex xs6 sm4>
                      <v-text-field
                        label="Min Teams"
                        single-line
                        hide-details
                        v-model="division.minTeams"
                      ></v-text-field>
                    </v-flex>
                  </v-layout>
                </v-container>
              </v-card>
              <!-- Start When  -->
              <v-card>
                <v-toolbar dense flat>
                  <v-toolbar-title>
                    When?
                  </v-toolbar-title>
                </v-toolbar>
                <v-container>
                  <v-layout row wrap>
                    <v-flex xs12 sm4>
                      <v-menu
                        lazy
                        :close-on-content-click="false"
                        v-model="date"
                        transition="scale-transition"
                        offset-y
                        full-width
                        :nudge-right="40"
                        max-width="290px"
                        min-width="290px"
                      >
                        <v-text-field
                          slot="activator"
                          label="Tournament Date"
                          prepend-icon="event"
                          readonly
                          v-model="division.days[0].date"
                          required
                        ></v-text-field>
                        <v-date-picker no-title scrollable actions v-model="division.days[0].date">
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
                    <v-flex xs6 sm4>
                      <v-menu
                        lazy
                        :close-on-content-click="false"
                        v-model="checkin"
                        transition="scale-transition"
                        offset-y
                        full-width
                        :nudge-right="40"
                        max-width="290px"
                        min-width="290px"
                      >
                        <v-text-field
                          slot="activator"
                          label="Check-in Time"
                          prepend-icon="access_time"
                          readonly
                          v-model="division.days[0].checkInTime"
                        ></v-text-field>
                        <v-time-picker actions v-model="division.days[0].checkInTime">
                          <template slot-scope="{ save, cancel }">
                            <v-card-actions>
                              <v-spacer></v-spacer>
                              <v-btn flat color="primary" @click="cancel">Cancel</v-btn>
                              <v-btn flat color="primary" @click="save">OK</v-btn>
                            </v-card-actions>
                          </template>
                        </v-time-picker>
                      </v-menu>
                    </v-flex>
                    <v-flex xs6 sm4>
                      <v-menu
                        lazy
                        :close-on-content-click="false"
                        v-model="play"
                        transition="scale-transition"
                        offset-y
                        full-width
                        :nudge-right="40"
                        max-width="290px"
                        min-width="290px"
                      >
                        <v-text-field
                          slot="activator"
                          label="Play Start Time"
                          prepend-icon="access_time"
                          readonly
                          v-model="division.days[0].playTime"
                        ></v-text-field>
                        <v-time-picker actions v-model="division.days[0].playTime">
                          <template slot-scope="{ save, cancel }">
                            <v-card-actions align-center justify-center>
                              <v-btn flat color="primary" @click="cancel">Cancel</v-btn>
                              <v-btn flat color="primary" @click="save">OK</v-btn>
                            </v-card-actions>
                          </template>
                        </v-time-picker>
                      </v-menu>
                    </v-flex>                   
                  </v-layout>
                </v-container>
              </v-card>
              <!-- Start Where  -->
              <v-card>
                <v-toolbar dense flat>
                  <v-toolbar-title>
                    Where?
                  </v-toolbar-title>
                </v-toolbar>
                <v-container>
                  <v-layout row wrap>
                    <v-flex xs12 sm4>
                      <v-select
                        :items="locationOptions"
                        item-text="name"
                        item-value="id"
                        label="Location"
                        prepend-icon="place"
                        autocomplete
                        return-object
                        required
                        v-model="division.location"
                      ></v-select>
                    </v-flex>
                  </v-layout>
                </v-container>
              </v-card>
              <!-- Start Registration  -->
              <v-card>
                <v-toolbar dense flat>
                  <v-toolbar-title>
                    Registration
                  </v-toolbar-title>
                </v-toolbar>
                <template v-for="(item,i) in division.registrationWindows" >
                  <registration-window 
                    :key="i" 
                    :window="item"
                    :beforeDate="division.days[0].date"></registration-window>
                  </template>
              </v-card>
            </v-flex>
          </v-layout>
        </v-container>
      </v-form>
    </v-card-text>
  </v-card>
</template>

<script>
import { mapGetters } from 'vuex'
import RegWindow from './RegistrationWindow.vue'

export default {
  props: ['division'],
  data () {
    return {
      date: false,
      checkin: false,
      play: false
    }
  },
  computed: {
    ...mapGetters([
      'ageTypeOptions',
      'genderOptions',
      'divisionOptions',
      'locationOptions'
    ]),
    title () {
      return this.division && this.division.id > 0 ? 'Edit Division' : 'Add New Division'
    },
    shortText () {
      let bp = this.$vuetify.breakpoint.name
      if (bp === 'xs') {
        return true
      }
      return false
    }
  },
  methods: {
    save () {
      this.$emit('save')
    },
    cancel () {
      this.$emit('cancel')
    }
  },
  components: {
    'registration-window': RegWindow
  }
}
</script>

<style>

</style>
