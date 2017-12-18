<template>
  <v-card :color="bgColor">
    <v-toolbar dense color="color4">
      <v-toolbar-title>
        Player {{i}}
      </v-toolbar-title>
    </v-toolbar>
    <v-container fluid class="hidden-sm-and-up">
      <v-layout row wrap>
        <v-flex xs12>
          <strong>Name: </strong>{{player.fullname}}
        </v-flex>
        <v-flex xs12>
          <strong>Phone: </strong>{{player.phone | usPhone}}
        </v-flex>
        <v-flex xs12>
          <strong>Email: </strong>{{player.email}}
        </v-flex>
        <v-flex xs12>
          <strong>Birthdate: </strong>{{player.dobFormatted}}
        </v-flex>
        <v-flex xs12>
          <strong>City, St: </strong>{{player.city}}, {{player.state}}
        </v-flex>
        <v-flex xs12>
          <strong>CBVA #: </strong>{{player.cbva || 'NA'}}
        </v-flex>
        <v-flex xs12>
          <strong>USAV #: </strong>{{player.usav || 'NA'}}
        </v-flex>
      </v-layout>
    </v-container>
    <v-container fluid class="hidden-xs-only">
      <v-layout row wrap>
        <v-flex xs2 text-xs-right>
          <strong>Name:</strong>
        </v-flex>
        <v-flex xs10>
          {{player.fullname}}
        </v-flex>
      </v-layout>
      <v-layout row wrap>
        <v-flex xs2 text-xs-right>
          <strong>Phone:</strong>
        </v-flex>
        <v-flex xs10>
          {{player.phone | usPhone}}
        </v-flex>
      </v-layout>
      <v-layout row wrap>
        <v-flex xs2 text-xs-right>
          <strong>Email:</strong>
        </v-flex>
        <v-flex xs10>
          {{player.email}}
        </v-flex>
      </v-layout>
      <v-layout row wrap>
        <v-flex xs2 text-xs-right>
          <strong>Birthdate:</strong>
        </v-flex>
        <v-flex xs10>
          {{player.dobFormatted}}
        </v-flex>
      </v-layout>
      <v-layout row wrap>
        <v-flex xs2 text-xs-right>
          <strong>City, St:</strong>
        </v-flex>
        <v-flex xs10>
          {{player.city}}, {{player.state}}
        </v-flex>
      </v-layout>
      <v-layout row wrap>
        <v-flex xs2 text-xs-right>
          <strong>CBVA #:</strong>
        </v-flex>
        <v-flex xs10>
          {{player.cbva || 'NA'}}
        </v-flex>
      </v-layout>
      <v-layout row wrap>
        <v-flex xs2 text-xs-right>
          <strong>USAV #:</strong>
        </v-flex>
        <v-flex xs10>
          {{player.usav || 'NA'}}
        </v-flex>
      </v-layout>
    </v-container>
  </v-card>
</template>

<script>

export default {
  props: ['fields', 'requiredFields', 'player', 'i', 'bgColor'],
  data () {
    return {
    }
  }
}
</script>

<style>

</style>
