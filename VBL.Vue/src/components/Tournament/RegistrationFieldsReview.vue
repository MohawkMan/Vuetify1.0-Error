<template>
  <v-card>
    <v-toolbar dense color="color4">
      <v-toolbar-title>
        Player {{i}}
      </v-toolbar-title>
    </v-toolbar>
    <v-card-title>
      <v-container>
        <v-layout row>
          <v-flex xs1 text-xs-right>
            <strong>Name:</strong>
          </v-flex>
          <v-flex xs11>
            {{player.fullname}}
          </v-flex>
        </v-layout>
        <v-layout row>
          <v-flex xs1 text-xs-right>
            <strong>Phone:</strong>
          </v-flex>
          <v-flex xs11>
            {{player.phone | usPhone}}
          </v-flex>
        </v-layout>
        <v-layout row>
          <v-flex xs1 text-xs-right>
            <strong>Email:</strong>
          </v-flex>
          <v-flex xs11>
            {{player.email}}
          </v-flex>
        </v-layout>
        <v-layout row>
          <v-flex xs1 text-xs-right>
            <strong>Birthdate:</strong>
          </v-flex>
          <v-flex xs11>
            {{player.dobFormatted}}
          </v-flex>
        </v-layout>
        <v-layout row>
          <v-flex xs1 text-xs-right>
            <strong>City, St:</strong>
          </v-flex>
          <v-flex xs11>
            {{player.city}} {{player.state ? player.state.abbreviation.abbreviation : ''}}
          </v-flex>
        </v-layout>
        <v-layout row>
          <v-flex xs1 text-xs-right>
            <strong>CBVA #:</strong>
          </v-flex>
          <v-flex xs11>
            {{player.cbva || 'NA'}}
          </v-flex>
        </v-layout>
        <v-layout row>
          <v-flex xs1 text-xs-right>
            <strong>USAV #:</strong>
          </v-flex>
          <v-flex xs11>
            {{player.usav || 'NA'}}
          </v-flex>
        </v-layout>
      </v-container>
    </v-card-title>
  </v-card>
</template>

<script>

export default {
  props: ['fields', 'requiredFields', 'player', 'i'],
  data () {
    return {
    }
  }
}
</script>

<style>

</style>
