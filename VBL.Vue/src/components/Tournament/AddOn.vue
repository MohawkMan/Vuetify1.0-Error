<template>
  <v-card color="grey lighten-4">
    <v-card-title class="pt-0">
      <v-container fluid grid-list-md>
        <v-layout row wrap>
          <v-flex xs12 class="headline grey--text">
            You may also like
          </v-flex>
        </v-layout>
        <v-layout row wrap>
          <v-flex xs2>
            <img :src="addOn.imageUrl" max-width="100%">
          </v-flex>
          <v-flex xs10>
            <v-container fluid grid-list-md>
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
                <v-flex md2 sm3>
                  <v-select
                    :items="qtyItems"
                    label="Quantity"
                  ></v-select>
                </v-flex>
              </v-layout>
            </v-container>
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
    return {}
  },
  computed: {
    qtyItems () {
      return Array.from(Array(this.addOn.quantityInStock + 1), (v, i) => i)
    }
  }
}
</script>

<style>

</style>
