<template>
  <v-stepper v-model="currentStep" vertical>
    <!-- Division -->
    <v-stepper-step 
      :step="1"
      :complete="currentStep > 1"
      :editable="currentStep > 1"
      >
      Select your division
    </v-stepper-step>
    <v-stepper-content :step="1">
      <v-card flat>
        <v-card-text>
          <h3>Select your division</h3>
          <v-radio-group v-model="registration.division">
            <v-radio v-for="(division,i) in tourney.divisions" :label="division.name" :value="division" :key="i"></v-radio>
          </v-radio-group>
        </v-card-text>
      </v-card>
      <v-btn color="color2" dark @click.native="onNext">Continue</v-btn>
    </v-stepper-content>
    <!-- Players -->
    <template v-for="(player,i) in registration.players">
      <v-stepper-step :step="i+2" :key="i">
        Player {{i+1}}
      </v-stepper-step>
      <v-stepper-content :step="i+2" :key="i">
        <v-card flat>
          <v-card-text>
            <registration-fields
              v-if="registration.division"
              :fields="registration.division.registrationFields"
              :player="player"
              @next="onNext"
              @back="onBack"
            >
            </registration-fields>
          </v-card-text>
        </v-card>
      </v-stepper-content>
    </template>
    <!-- Payment -->
    <v-stepper-step 
      :step="paymentStep"
      :complete="currentStep > paymentStep"
      :editable="currentStep > paymentStep"
      >
      Payment Info
    </v-stepper-step>
    <v-stepper-content :step="paymentStep">
      <v-card flat>
        <v-card-text>
          Payment Info
        </v-card-text>
      </v-card>
      <v-btn color="color2" dark @click.native="onNext">Continue</v-btn>
      <v-btn flat @click.native="onBack">Back</v-btn>
    </v-stepper-content>
    <!-- Review -->
    <v-stepper-step 
      :step="reviewStep"
      :complete="currentStep > reviewStep"
      :editable="currentStep > reviewStep"
      >
      Review
    </v-stepper-step>
    <v-stepper-content :step="reviewStep">
      <v-card flat>
        <v-card-text>
                <v-card>
                  <v-toolbar dense color="color3">
                    <v-toolbar-title>
                      Division: {{registration.division ? registration.division.divisionsString : ''}}
                    </v-toolbar-title>
                  </v-toolbar>
                </v-card>
                <template v-for="(player,i) in registration.players">
                  <registration-fields-review
                  :key="i"
                  :player="player"
                  :i="i+1"
                  >
                  </registration-fields-review>
                </template>

        </v-card-text>
      </v-card>
      <v-btn color="color2" dark @click.native="onNext">Continue</v-btn>
      <v-btn flat @click.native="onBack">Back</v-btn>
    </v-stepper-content>


    </v-stepper-items>
  </v-stepper>  
</template>

<script>
import RegFields from './RegistrationFields.vue'
import RegFieldsReview from './RegistrationFieldsReview.vue'

export default {
  props: ['tourney', 'registration'],
  data () {
    return {
      divisionStep: 1,
      currentStep: 1,
      selectedDivision: null
    }
  },
  computed: {
    paymentStep () {
      return this.registration.players.length + 2
    },
    reviewStep () {
      return this.paymentStep + 1
    }
  },
  methods: {
    onNext () {
      this.currentStep += 1
    },
    onBack () {
      this.currentStep -= 1
    }
  },
  components: {
    'registration-fields': RegFields,
    'registration-fields-review': RegFieldsReview
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
