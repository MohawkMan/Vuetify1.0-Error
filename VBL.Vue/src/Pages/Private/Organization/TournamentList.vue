<template>
  <v-container>
    <v-layout row>
      <v-flex>
        {{ username }} Tournaments
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
export default {
  props: ['username']
}
</script>

<style>

</style>
