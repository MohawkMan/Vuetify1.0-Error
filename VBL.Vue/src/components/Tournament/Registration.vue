<template>
  <v-stepper v-if="!tournament.externalRegistrationUrl || me" v-model="currentStep" vertical>
    <!-- Division -->
    <v-stepper-step 
      :step="1"
      :complete="!!registration.division"
      :editable="!!registration.division"
      >
      Select your division
    </v-stepper-step>
    <v-stepper-content :step="1">
      <v-card color="grey lighten-4">
        <v-card-text>
          <h3>Select your division</h3>
          <v-radio-group v-model="registration.division">
            <v-radio v-for="(division,i) in tournament.divisions" :label="division.name" :value="division" :key="i"></v-radio>
          </v-radio-group>
        </v-card-text>
      </v-card>
      <v-btn color="color2 white--text" @click.native="onNext" :disabled="!registration.division">Continue</v-btn>
    </v-stepper-content>
    <!-- Players -->
    <template v-for="(player,i) in registration.players">
      <v-stepper-step 
        :step="i+2"
        :key="i+'step'"
        :complete="player.valid"
        :editable="player.valid"
      >
        Player {{i+1}}
      </v-stepper-step>
      <v-stepper-content :step="i+2" :key="i+'stepContent'">
        <v-card color="grey lighten-4">
          <v-card-text class="pt-0">
            <registration-fields
              v-if="registration.division"
              :fields="registration.division.registrationFields.fields"
              :requiredFields="registration.division.registrationFields.requiredFields"
              :player="player"
              :sanctioningBodyId="registration.division.sanctioningBodyId"
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
    <!-- Review -->
    <v-stepper-step 
      :step="reviewStep"
      :complete="registration.confirmed"
      :editable="currentStep > reviewStep"
      >
      Review
    </v-stepper-step>
    <v-stepper-content :step="reviewStep">
      <v-card color="grey lighten-4">
          <!-- Players Review -->
          <template v-for="(player,i) in registration.players">
            <registration-fields-review
            :key="i"
            :fields="registration.division.registrationFields.fields"
            :requiredFields="registration.division.registrationFields.requiredFields"
            :player="player"
            :sanctioningBodyId="registration.division.sanctioningBodyId"
            :i="i+1"
            bgColor="grey lighten-4"
            >
            </registration-fields-review>
          </template>
          <!-- Tournament/Division Review -->
          <v-toolbar dense color="color4">
            <v-toolbar-title>
              Registration
            </v-toolbar-title>
          </v-toolbar>
          <v-container fluid class="hidden-sm-and-up">
            <v-layout row wrap>
              <v-flex xs12>
                <strong>Tournament: </strong>{{tournament.name}} 
              </v-flex>
            </v-layout>
            <v-layout row wrap>
              <v-flex xs12>
                <strong>Date: </strong>{{tournament.startDateDisplay}} 
              </v-flex>
            </v-layout>
            <v-layout row wrap>
              <v-flex xs12>
                <strong>Division: </strong>{{registration.division ? registration.division.divisionsString : ''}}
              </v-flex>
            </v-layout>
            <v-layout row wrap>
              <v-flex xs12>
                <strong>Entry Fee: </strong>{{entryFee | usDollars}}
              </v-flex>
            </v-layout>
          </v-container>
          <v-container fluid class="hidden-xs-only">
            <v-layout row wrap>
              <v-flex xs2 text-xs-right>
                <strong>Tournament:</strong>
              </v-flex>
              <v-flex xs10>
                {{tournament.name}}
              </v-flex>
            </v-layout>
            <v-layout row wrap>
              <v-flex xs2 text-xs-right>
                <strong>Date:</strong>
              </v-flex>
              <v-flex xs10>
                {{tournament.startDateDisplay}}
              </v-flex>
            </v-layout>
            <v-layout row wrap>
              <v-flex xs2 text-xs-right>
                <strong>Division:</strong>
              </v-flex>
              <v-flex xs10>
                {{registration.division ? registration.division.divisionsString : ''}}
              </v-flex>
            </v-layout>
            <v-layout row wrap>
              <v-flex xs2 text-xs-right>
                <strong>Entry Fee:</strong>
              </v-flex>
              <v-flex xs10>
                {{entryFee | usDollars}}
              </v-flex>
            </v-layout>
          </v-container>
      </v-card>
      <v-checkbox
        label="All information is accurate"
        v-model="registration.confirmed"
      ></v-checkbox>
      <v-btn color="color3 white--text" @click.native="addToCart" :disabled="!registration.confirmed">Add To Cart</v-btn>
      <v-btn flat @click.native="onBack">Back</v-btn>
    </v-stepper-content>
    <v-dialog v-model="cartDialog" max-width="350">
      <v-card>
        <v-card-title class="headline color3--text text-xs-center">
          Your registration has been added to the cart!
        </v-card-title>
        <v-card-actions class="text-xs-center">
          <v-layout justify-center>
            <v-btn color="color2" dark :to="{name: 'checkout'}">Check Out Now</v-btn>
            <v-btn color="color1" dark @click.stop="cartDialog=false">Add Another</v-btn>
          </v-layout>
        </v-card-actions>
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
            <v-btn color="color3" dark @click="exreg(tournament.externalRegistrationUrl)">
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
import CartItem from '../../classes/CartItem'
import * as mutations from '../../store/MutationTypes'

export default {
  props: ['tournament', 'registration'],
  data () {
    return {
      currentStep: 1,
      cartDialog: false
    }
  },
  computed: {
    me () {
      return this.$store.getters.user && this.$store.getters.user.id === 1
    },
    reviewStep () {
      return this.registration.players.length + 2
    },
    entryFee () {
      return this.registration.division ? this.registration.division.currentEntryFee : 0
    },
    dto () {
      return {
        registration: this.registration.dto,
        paymentToken: {
          id: this.token.id,
          card_id: this.token.card.id,
          card_last4: this.token.card.last4,
          card_brand: this.token.card.brand,
          client_ip: this.token.client_ip
        }
      }
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
    addToCart () {
      const item = new CartItem()
      item.organization = this.registration.organization
      item.name = 'Tournament Registration'
      item.description = `${this.tournament.name} ${this.registration.division.divisionsString} ${this.registration._teamName}`
      item.amount = this.entryFee
      item.registration = this.registration.dto
      this.$store.commit(mutations.ADD_CART_ITEM, item)
      this.cartDialog = true
      this.reset()
      this.$emit('addedToCart')
    },
    exreg (url) {
      window.open(url)
    },
    reset () {
      this.currentStep = 1
      this.addOnComplete = false
    }
  },
  components: {
    'registration-fields': RegFields,
    'registration-fields-review': RegFieldsReview
  },
  watch: {
    'registration.division': function (newVal, oldVal) {
      if (newVal) {
        this.registration.initPlayers()
        this.partnerString = 'partner'
        if (this.registration.division.numOfPlayers > 2) {
          this.partnerString = 'teammates'
        }
      }
    },
    currentStep: function (newVal, oldVal) {
      this.$ga.event('Registration', `Step ${newVal}`, this.$route.name, newVal)
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
