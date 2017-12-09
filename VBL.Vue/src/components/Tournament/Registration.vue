<template>
  <v-stepper v-model="currentStep" vertical>
    <v-stepper-step 
      step="1"
      :complete="currentStep > 1"
      :editable="currentStep > 1"
      >
      Select your division
    </v-stepper-step>
    <v-stepper-content step="1">
      <v-card flat>
        <v-card-text>
          <h3>Select your division</h3>
          <v-radio-group v-model="registration.division">
            <v-radio v-for="(division,i) in tourney.divisions" :label="division.name" :value="division" :key="i"></v-radio>
          </v-radio-group>
        </v-card-text>
      </v-card>
      <v-btn color="color2" dark @click.native="currentStep += 1">Continue</v-btn>
    </v-stepper-content>

    <template v-for="(player,i) in registration.players">
      <v-stepper-step :step="i+2" :key="i">
        Player {{i+1}}
      </v-stepper-step>
      <v-stepper-content :step="i+2" :key="i">
        <v-card flat>
          <v-card-text>
            <v-container>
              <v-layout>
                <v-flex xs12 md4>
                  <v-text-field
                    name="firstName"
                    label="First Name"
                    v-model="player.firstName"
                  ></v-text-field>
                </v-flex>
                <v-flex xs12 md4>
                  <v-text-field
                    name="lastName"
                    label="Last Name"
                    v-model="player.lastName"
                  ></v-text-field>
                </v-flex>
              </v-layout>
              <v-layout>
                <v-flex xs12 md4>
                  <v-text-field
                    name="phone"
                    label="Phone"
                    v-model="player.phone"
                    mask="phone"
                  ></v-text-field>
                </v-flex>
                <v-flex xs12 md4>
                  <v-text-field
                    name="email"
                    label="Email"
                    v-model="player.email"
                  ></v-text-field>
                </v-flex>
              </v-layout>
              <v-layout>
                <v-flex xs12 md4>
                  <v-text-field
                    name="city"
                    label="City"
                    v-model="player.city"
                  ></v-text-field>
                </v-flex>
                <v-flex xs12 md4>
                  <v-select
                    v-bind:items="states"
                    v-model="player.state"
                    label="State"
                    single-line
                    autocomplete
                    item-text="name"
                    item-value="abbreviation"
                    return-object
                    :hint="player.state ? `${player.state.name}, ${player.state.abbreviation}` : ''"
                    persistent-hint
                  ></v-select>
                </v-flex>
              </v-layout>
            </v-container>
          </v-card-text>
        </v-card>
        <v-btn color="color2" dark @click.native="currentStep += 1">Continue</v-btn>
        <v-btn flat @click.native="currentStep -= 1">Back</v-btn>
      </v-stepper-content>
    </template>


    </v-stepper-items>
  </v-stepper>  
</template>

<script>
import States from '../../json/states.json'
export default {
  props: ['tourney', 'registration'],
  data () {
    return {
      states: States,
      currentStep: 1,
      selectedDivision: null
    }
  },

  watch: {
    'registration.division': function () {
      this.registration.initPlayers()
    }
  }
}
</script>

<style>

</style>
