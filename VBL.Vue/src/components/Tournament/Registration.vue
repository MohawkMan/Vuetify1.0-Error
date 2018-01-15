<template>
  <v-stepper v-if="!tourney.externalRegistrationUrl" v-model="currentStep" vertical>
    <!-- Division -->
    <v-stepper-step 
      :step="1"
      :complete="!!registration.division"
      :editable="!!registration.division && !processing"
      >
      Select your division
    </v-stepper-step>
    <v-stepper-content :step="1">
      <v-card color="grey lighten-4">
        <v-card-text>
          <h3>Select your division</h3>
          <v-radio-group v-model="registration.division">
            <v-radio v-for="(division,i) in tourney.divisions" :label="division.name" :value="division" :key="i"></v-radio>
          </v-radio-group>
        </v-card-text>
      </v-card>
      <v-btn color="color2 white--text" @click.native="onNext" :disabled="!registration.division">Continue</v-btn>
    </v-stepper-content>
    <!-- Players -->
    <template v-for="(player,i) in registration.players">
      <v-stepper-step 
        :step="i+2"
        :key="i"
        :complete="player.valid"
        :editable="player.valid && !processing"
      >
        Player {{i+1}}
      </v-stepper-step>
      <v-stepper-content :step="i+2" :key="i">
        <v-card color="grey lighten-4">
          <v-card-text class="pt-0">
            <registration-fields
              v-if="registration.division"
              :fields="registration.division.registrationFields.fields"
              :requiredFields="registration.division.registrationFields.requiredFields"
              :player="player"
              @next="onNext"
              @back="onBack"
            >
            </registration-fields>
          </v-card-text>
        </v-card>
        <v-btn color="color2 white--text" @click.native="onNext" :disabled="!player.valid">Continue</v-btn>
        <v-btn flat @click.native="onBack">Back</v-btn>
      </v-stepper-content>
    </template>
    <!-- AddOn and Payment -->
    <v-stepper-step 
      :step="addOnStep"
      :complete="addOnComplete"
      :editable="addOnComplete && !processing"
      >
      Suggested For You
    </v-stepper-step>
    <v-stepper-content :step="addOnStep">
      <registration-addon v-if="addOn" :addOn="addOn"></registration-addon>
      <v-btn color="color2" dark @click.native="onNext">Continue</v-btn>
      <v-btn flat @click.native="onBack">Back</v-btn>
    </v-stepper-content>
    <!-- Review -->
    <v-stepper-step 
      :step="reviewStep"
      :complete="registration.confirmed"
      :editable="currentStep > reviewStep && !processing"
      >
      Review
    </v-stepper-step>
    <v-stepper-content :step="reviewStep">
      <v-card color="grey lighten-4">
          <!-- Division Review -->
          <v-toolbar dense color="color4">
            <v-toolbar-title>
              Division
            </v-toolbar-title>
          </v-toolbar>
          <v-container fluid>
            <v-layout row wrap>
              <v-flex xs12>
                {{registration.division ? registration.division.divisionsString : ''}}
              </v-flex>
            </v-layout>
          </v-container>
          <!-- Players Review -->
          <template v-for="(player,i) in registration.players">
            <registration-fields-review
            :key="i"
            :player="player"
            :i="i+1"
            bgColor="grey lighten-4"
            >
            </registration-fields-review>
          </template>
          <!-- Cart Review -->
          <v-toolbar dense color="color4">
            <v-toolbar-title>
              Cart Items
            </v-toolbar-title>
          </v-toolbar>
          <v-container fluid>
            <v-layout row>
              <v-flex xs2 class="text-xs-center">Qty</v-flex>
              <v-flex xs6>Item</v-flex>
              <v-flex xs4 class="text-xs-right">Price</v-flex>
            </v-layout>
            <v-layout row>
              <v-flex xs2 class="text-xs-center">1</v-flex>
              <v-flex xs6>Entry Fee</v-flex>
              <v-flex xs4 class="text-xs-right">{{entryFee | usDollars}}</v-flex>
            </v-layout>
            <v-layout row v-if="addOn && addOn.qty">
              <v-flex xs2 class="text-xs-center">{{addOn.qty}}</v-flex>
              <v-flex xs6 class="hideOverflow">{{addOn.name}}</v-flex>
              <v-flex xs4 class="text-xs-right">{{addOn.subtotal | usDollars}}</v-flex>
            </v-layout>
            <v-layout>
              <v-flex xs8 class="text-xs-right"><strong>Total:</strong></v-flex>
              <v-flex xs4 class="text-xs-right"><strong>{{total | usDollars}}</strong></v-flex>
            </v-layout>
          </v-container>
      </v-card>
      <v-checkbox
        label="All information is accurate"
        v-model="registration.confirmed"
      ></v-checkbox>
      <v-btn color="color3 white--text" @click.native="onNext" :disabled="!registration.confirmed">Continue</v-btn>
      <v-btn flat @click.native="onBack">Back</v-btn>
    </v-stepper-content>
    <v-stepper-step
      :step="paymentStep"
    >Payment</v-stepper-step>
    <v-stepper-content :step="paymentStep">
        <v-card color="grey lighten-4">
          <v-card-text class="pt-0">
            <v-radio-group v-model="registration.paymentType" column>
              <v-radio
                v-if="registration.division && registration.division.currentRegistrationWindow.canPayAtEvent"
                label="Pay at the tournament" 
                value="event"></v-radio>
              <v-radio
                v-if="registration.division && registration.division.currentRegistrationWindow.canProcessPayment"
                label="Pay now" 
                value="process"></v-radio>
            </v-radio-group>
          </v-card-text>
        </v-card>
        <v-btn
          color="color3 white--text"
          @click.native="register"
          :disabled="!registration.valid"
          :loading="processing"
        >Complete</v-btn>
        <v-btn flat @click.native="onBack" :disabled="processing">Back</v-btn>      
    </v-stepper-content>
    <v-dialog v-model="successDialog" max-width="350">
      <v-card>
        <v-card-title class="headline color3 white--text">
          Boom! You are registered!
        </v-card-title>
        <v-card-text>
          See ya on the beach.
        </v-card-text>
        <v-card-text>
          See ya on the beach.
        </v-card-text>
      </v-card>
    </v-dialog>
  </v-stepper>
  <v-container v-else fill-height>
    <v-layout row wrap align-center>
      <v-flex xs8 offset-xs2>
        <v-layout row wrap text-xs-center class="ma-5">
          <v-flex xs12>
            <h2>This tournament's registration is on an external website</h2>
          </v-flex>
          <v-flex xs12>
            <v-btn color="color3" dark @click="exreg(tourney.externalRegistrationUrl)">
              Continue to registration
              <v-icon class="ml-3">open_in_new</v-icon>
            </v-btn>
          </v-flex>
        </v-layout>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import RegFields from './RegistrationFields.vue'
import RegFieldsReview from './RegistrationFieldsReview.vue'
import AddOn from './AddOn.vue'
import vbl from '../../VolleyballLife'

export default {
  props: ['tourney', 'registration'],
  data () {
    return {
      currentStep: 1,
      processing: false,
      addOnComplete: false,
      successDialog: false
    }
  },
  computed: {
    addOnStep () {
      return this.registration.players.length + 2
    },
    reviewStep () {
      return this.addOnStep + 1
    },
    paymentStep () {
      return this.reviewStep + 1
    },
    addOn () {
      return this.registration.division
      ? this.registration.division.currentRegistrationWindow
      ? this.registration.division.currentRegistrationWindow.addOn
      : null
      : null
    },
    entryFee () {
      return this.registration.division ? this.registration.division.currentEntryFee : 0
    },
    total () {
      return this.addOn ? this.addOn.subtotal + this.entryFee : this.entryFee
    }
  },
  methods: {
    onNext () {
      if (this.currentStep === this.addOnStep) {
        this.addOnComplete = true
      }
      this.currentStep += 1
    },
    onBack () {
      this.currentStep -= 1
    },
    register () {
      this.processing = true
      console.log(this.registration.dto)
      this.axios.put(vbl.tournament.register, this.registration.dto)
        .then((response) => {
          var tourney = response.data
          console.log(tourney)
          this.$emit('registered')
          this.processing = false
        })
        .catch((response) => {
          console.log(`Error => response: ${response}`)
          this.processing = false
        })
    },
    exreg (url) {
      window.open(url)
    }
  },
  components: {
    'registration-fields': RegFields,
    'registration-fields-review': RegFieldsReview,
    'registration-addon': AddOn
  },
  watch: {
    'registration.division': function () {
      this.registration.initPlayers()
    }
  }
}
</script>

<style>
.hideOverflow {
  overflow: hidden;
  text-overflow: ellipsis;
  white-space: nowrap;
}
</style>
