<template>
  <v-card color="grey lighten-4">
    <v-card-title class="pt-0">
      <v-container fluid grid-list-sm>
        <v-layout row wrap>
          <v-flex xs12 class="headline grey--text">
            You may also like
          </v-flex>
        </v-layout>
        <v-layout row wrap>
          <v-flex xs4 text-xs-center>
            <img :src="addOn.imageUrl" style="max-width: 100%">
          </v-flex>
          <v-flex xs8>
            <v-container fluid grid-list-sm>
              <v-layout row wrap>
                <v-flex>
                  <h3 class="primary--text">{{addOn.name}}</h3>
                  <hr>
                  Price: {{addOn.price | usDollars}}
                </v-flex>
              </v-layout>
              <v-layout row wrap>
                <v-flex>
                  {{addOn.description}}
                </v-flex>
              </v-layout>
              <v-layout row wrap>
                <v-flex>
                  <v-select
                    v-model="qty"
                    :items="qtyItems"
                    label="Quantity"
                    hide-details	
                  ></v-select>
                </v-flex>
              </v-layout>
            </v-container>
          </v-flex>
        </v-layout>
        <v-layout row wrap>
          <v-flex xs12 text-xs-center>
            <v-btn 
              color="color3" 
              :disabled="!qty"
              @click="addToCart">
              <v-icon>add_shopping_cart</v-icon>
              Add to Cart
            </v-btn>
          </v-flex>
          <v-flex xs12 text-xs-center class="pt-0 color1--text">
            <strong>{{this.addOn.qty || 0 }} in cart</strong>
            <v-icon
              v-if="this.addOn.qty > 0"
              color="red darken-2"
              style="cursor: pointer"
              @click="clearCart"
            >cancel</v-icon>
          </v-flex>
        </v-layout>
      </v-container>
    </v-card-title>
  </v-card>
</template>

<script>
export default {
  props: ['addOn'],
  data () {
    return {
      qty: null
    }
  },
  computed: {
    qtyItems () {
      return Array.from(Array(this.addOn.quantityInStock + 1), (v, i) => i)
    }
  },
  methods: {
    addToCart () {
      if (!this.qty) return
      this.addOn.qty += this.qty
      this.qty = null
    },
    clearCart () {
      this.addOn.qty = 0
    }
  }
}
</script>

<style>

</style>
