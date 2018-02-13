<template>
  <v-container fill-height v-if="loading">
    <v-layout row wrap align-center>
      <v-flex xs8 offset-xs2>
        <v-layout row wrap text-xs-center>
          <v-flex xs12>
            <h3>Loading</h3>
          </v-flex>
          <v-flex xs12>
            <v-progress-linear v-bind:indeterminate="true"></v-progress-linear>
          </v-flex>
        </v-layout>
      </v-flex>
    </v-layout>
  </v-container>
  <v-container fill-height v-else-if="purchaseComplete">
    <v-layout row wrap align-center>
      <v-flex xs8 offset-xs2>
        <v-layout row wrap text-xs-center>
          <v-flex xs12 class="display-2">
            <h3>Thank you!</h3>
          </v-flex>
          <v-flex xs12 class="headline">
            Your purchase is complete!
          </v-flex>
          <v-flex xs12 class="headline">
            Your confirmation code is: {{confirmation}}
          </v-flex>
          <v-flex xs12 class="headline">
            You should be receiving a receipt at {{emailReceipt}} shortly
          </v-flex>
          <template v-if="registrationEmails.length > 0">
            <v-flex xs12 class="headline">
              We have also sent tournament registration emails to:
            </v-flex>
            <v-flex xs12 v-for="(e,i) in registrationEmails" :key="i" class="headline">
              {{e}}
            </v-flex>
          </template>
        </v-layout>
      </v-flex>
    </v-layout>
  </v-container>
  <v-container v-else grid-list-md>
    <v-layout row wrap>
      <v-flex xs12 sm10 offset-sm1>
        <v-card>
          <v-toolbar dark color="color3">
            <v-toolbar-title><v-icon class="mr-3">shopping_basket</v-icon>Your Cart</v-toolbar-title>
          </v-toolbar>
          <v-card-text>
            <v-list two-line>
              <template v-for="(item,i) in cart.items">
                <v-list-tile :key="i+'a'">
                  <v-list-tile-avatar>
                    <v-icon>assignment_turned_in</v-icon>
                  </v-list-tile-avatar>
                  <v-list-tile-content>
                    <v-list-tile-title>{{item.name}}</v-list-tile-title>
                    <v-list-tile-sub-title>{{item.description}}</v-list-tile-sub-title>
                  </v-list-tile-content>
                  <v-list-tile-content class="align-end">
                    {{item.amount | usDollars}}
                  </v-list-tile-content>
                  <v-list-tile-action>
                    <v-btn icon ripple @click.stop="deleteClick(item)">
                      <v-icon>delete</v-icon>
                    </v-btn>
                  </v-list-tile-action>
                </v-list-tile>
                <v-divider :key="i+'b'"></v-divider>
              </template>
              <v-list-tile v-if="cart.items.length > 0">
                <v-list-tile-content class="align-end">Total: {{total | usDollars}}</v-list-tile-content>
                <v-list-tile-action></v-list-tile-action>
              </v-list-tile>
              <v-list-tile v-else>
                <v-list-tile-content class="align-center">There are no items in your cart</v-list-tile-content>
              </v-list-tile>
            </v-list>
          </v-card-text>
        </v-card>
      </v-flex>
    </v-layout>
    <v-layout row  v-if="cart.items.length > 0">
      <v-flex xs12 sm10 offset-sm1>
        <v-card>
          <v-toolbar dark color="color3">
            <v-toolbar-title>
              <v-icon class="mr-3">lock</v-icon>
              Secure Payment
            </v-toolbar-title>
          </v-toolbar>
          <v-card-text class="text-xs-center">
            <v-layout row>
              <v-flex xs12 md6 offset-md3>
                <v-select
                  v-model="emailReceipt"
                  label="Email receipt to:"
                  :items="playerEmails"
                  combobox
                  class="eSelect"
                  hide-details
                  required
                  :rules="[
                    () => $v.emailReceipt.required || 'An email is required',
                    () => $v.emailReceipt.email || 'A valid email is required'
                  ]"
                ></v-select>
              </v-flex>
            </v-layout>

            <v-layout row>
              <v-flex text-xs-left xs12 md6 offset-md3>
                <label class="color1--text">Credit or Debit Card</label>
                <stripe-card class='stripe-card'
                  :class='{ complete }'
                  stripe='pk_live_0Egtaqiqypfs3i3fcOCSls49'
                  :options='stripeOptions'
                  @change='complete = $event.complete'
                />
              </v-flex>
            </v-layout>

            <v-layout row v-if="!!paymentError" class="error--text">
              <v-flex  xs12 md6 offset-md3>
                <strong>Oops we have an error</strong>
              </v-flex>
            </v-layout>
            <v-layout row v-if="!!paymentError" class="error--text">
              <v-flex  xs12 md6 offset-md3>
                {{paymentError}}
              </v-flex>
            </v-layout>
            <v-layout row>
              <v-flex xs12 md6 offset-md3>
                <v-btn
                  color="color2 white--text" 
                  class="ml-0" 
                  :disabled="!complete || !valid"
                  :loading="processing"
                  @click="getPaymentToken"
                  >Submit payment
                </v-btn>
              </v-flex>
            </v-layout>
          </v-card-text>
        </v-card>
      </v-flex>
    </v-layout>
    <v-dialog max-width="300px" v-if="selectedItem" v-model="deleteDialog">
      <v-card>
        <v-card-title class="headline">Are you sure?</v-card-title>
        <v-card-text>Are you sure you want to remove this {{selectedItem.name}} from the cart</v-card-text>
        <v-card-actions>
          <v-layout justify-center>
            <v-btn flat @click.stop="deleteSelected">remove</v-btn>
            <v-btn flat @click.stop="deleteDialog = false">cancel</v-btn>
          </v-layout>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-container>
</template>

<script>
import sum from 'lodash.sum'
import { Card, createToken } from 'vue-stripe-elements-plus'
import * as mutations from '../../store/MutationTypes'
import SDK from '../../VBL'
import CartItem from '../../classes/CartItem'
import { validationMixin } from 'vuelidate'
import { required, email } from 'vuelidate/lib/validators'

export default {
  mixins: [validationMixin],
  validations () {
    return {
      emailReceipt: { required, email }
    }
  },
  data () {
    return {
      deleteDialog: false,
      selectedItem: null,
      paymentError: null,
      processing: false,
      token: null,
      complete: false,
      focus: false,
      stripeOptions: {
      },
      emailReceipt: null,
      purchaseComplete: false,
      registrationEmails: [],
      confirmation: null
    }
  },
  computed: {
    valid () { return !this.$v.$invalid },
    cart () {
      return this.$store.getters.cart
    },
    loading () {
      return this.$store.getters.loading
    },
    playerEmails () {
      var list = []
      this.cart.items.forEach((item) => {
        item = new CartItem(item)
        list = list.concat(item.playerEmails)
      })
      return list
    },
    total () {
      return sum(this.cart.items.map(m => m.amount))
    },
    bag () {
      return {
        id: 0,
        organizationId: this.cart.organization.id,
        items: this.cart.items,
        paymentToken: {
          id: this.token.id,
          card_id: this.token.card.id,
          card_last4: this.token.card.last4,
          card_brand: this.token.card.brand,
          client_ip: this.token.client_ip
        },
        total: this.total,
        emailReceiptTo: this.emailReceipt
      }
    }
  },
  methods: {
    deleteClick (item) {
      this.selectedItem = item
      this.deleteDialog = true
    },
    deleteSelected () {
      if (this.selectedItem) {
        this.$store.commit(mutations.REMOVE_CART_ITEM, this.selectedItem.id)
      }
      this.deleteDialog = false
    },
    getPaymentToken () {
      this.paymentError = null
      this.processing = true
      createToken()
        .then((data) => {
          console.log(data.token)
          this.token = data.token
          this.processCart()
        })
        .catch((result) => {
          console.log(result)
          this.paymentError = result.error.message
          this.processing = false
        })
    },
    processCart () {
      console.log(this.bag)
      const sdk = new SDK(this.axios)
      sdk.cart.process(this.bag)
        .then((response) => {
          console.log(response.data)
          this.confirmation = response.data
          this.registrationEmails = this.playerEmails
          this.purchaseComplete = true
          this.$store.commit(mutations.CLEAR_CART)
          this.processing = false
        })
        .catch((error) => {
          console.log(error.response.data)
          this.paymentError = error.response.data.message
          this.processing = false
        })
    }
  },
  components: {
    'stripe-card': Card
  }
}
</script>

<style>
.eSelect {
  background-color: white;
  border-radius: 4px;
}
.stripe-card {
  background-color: white;
  height: 40px;
  padding: 10px 12px;
  border-radius: 4px;
  border: 1px solid #152a69;
  box-shadow: 0 1px 3px 0 #e6ebf1;
  -webkit-transition: box-shadow 150ms ease;
  transition: box-shadow 150ms ease;
}
.stripe-card.complete {
  border-color: green;
}
.stripe-card--focus {
  box-shadow: 0 1px 3px 0 #0e6fcf;
}

.stripe-card--invalid {
  border-color: #fa755a;
}

.stripe-card--webkit-autofill {
  background-color: #fefde5 !important;
}
</style>
