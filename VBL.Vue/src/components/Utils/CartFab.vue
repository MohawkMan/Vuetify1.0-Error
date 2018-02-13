<template>
    <v-badge color="red" overlap v-if="cart.items.length > 0" class="link" bottom left>
    <span slot="badge">{{cart.items.length}}</span>
    <v-avatar class="color3" @click="goToCart">
      <v-icon>shopping_cart</v-icon>
    </v-avatar>
  </v-badge>
</template>

<script>
export default {
  computed: {
    cart () {
      return this.$store.getters.cart
    }
  },
  methods: {
    goToCart () {
      this.$router.push({name: 'checkout'})
    }
  }
}
</script>

<style scoped>
  .link {
    cursor: pointer;
  }
</style>
