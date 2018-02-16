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
        <v-flex xs12 v-if="sanctioningBody.toLowerCase() === 'aau'">
          <strong>AAU #: </strong>{{player.aau || 'NA'}}
        </v-flex>
        <v-flex xs12 v-if="sanctioningBody.toLowerCase() === 'avp'">
          <strong>AVP #: </strong>{{player.avp || 'NA'}}
        </v-flex>
        <v-flex xs12 v-if="sanctioningBody.toLowerCase() === 'cbva'">
          <strong>CBVA #: </strong>{{player.cbva || 'NA'}}
        </v-flex>
        <v-flex xs12 v-if="sanctioningBody.toLowerCase() === 'usav'">
          <strong>USAV #: </strong>{{player.usav || 'NA'}}
        </v-flex>
        <v-flex xs12 v-if="_fields.includes('phone')">
          <strong>Phone: </strong>{{player.phone | usPhone}}
        </v-flex>
        <v-flex xs12 v-if="_fields.includes('email')">
          <strong>Email: </strong>{{player.email}}
        </v-flex>
        <v-flex xs12 v-if="_fields.includes('dob')">
          <strong>Birthdate: </strong>{{player.dobFormatted}}
        </v-flex>
        <v-flex xs12 v-if="_fields.includes('cityState')">
          <strong>City, St: </strong>{{player.city}}, {{player.state}}
        </v-flex>
        <v-flex xs12 v-if="_fields.includes('club')">
          <strong>Club: </strong>{{player.club || 'NA'}}
        </v-flex>
        <v-flex xs12 v-if="_fields.includes('aau') && sanctioningBody.toLowerCase() !== 'aau'">
          <strong>AAU #: </strong>{{player.aau || 'NA'}}
        </v-flex>
        <v-flex xs12 v-if="_fields.includes('avp') && sanctioningBody.toLowerCase() !== 'avp'">
          <strong>AVP #: </strong>{{player.avp || 'NA'}}
        </v-flex>
        <v-flex xs12 v-if="_fields.includes('cbva') && sanctioningBody.toLowerCase() !== 'cbva'">
          <strong>CBVA #: </strong>{{player.cbva || 'NA'}}
        </v-flex>
        <v-flex xs12 v-if="_fields.includes('usav') && sanctioningBody.toLowerCase() !== 'usav'">
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
      <v-layout row wrap v-if="sanctioningBody.toLowerCase() === 'aau'">
        <v-flex xs2 text-xs-right>
          <strong>AAU #:</strong>
        </v-flex>
        <v-flex xs10>
          {{player.aau || 'NA'}}
        </v-flex>
      </v-layout>
      <v-layout row wrap v-if="sanctioningBody.toLowerCase() === 'avp'">
        <v-flex xs2 text-xs-right>
          <strong>AVP #:</strong>
        </v-flex>
        <v-flex xs10>
          {{player.avp || 'NA'}}
        </v-flex>
      </v-layout>
      <v-layout row wrap v-if="sanctioningBody.toLowerCase() === 'cbva'">
        <v-flex xs2 text-xs-right>
          <strong>CBVA #:</strong>
        </v-flex>
        <v-flex xs10>
          {{player.cbva || 'NA'}}
        </v-flex>
      </v-layout>
      <v-layout row wrap v-if="sanctioningBody.toLowerCase() === 'usav'">
        <v-flex xs2 text-xs-right>
          <strong>USAV #:</strong>
        </v-flex>
        <v-flex xs10>
          {{player.usav || 'NA'}}
        </v-flex>
      </v-layout>
      <v-layout row wrap v-if="_fields.includes('phone')">
        <v-flex xs2 text-xs-right>
          <strong>Phone:</strong>
        </v-flex>
        <v-flex xs10>
          {{player.phone | usPhone}}
        </v-flex>
      </v-layout>
      <v-layout row wrap v-if="_fields.includes('email')">
        <v-flex xs2 text-xs-right>
          <strong>Email:</strong>
        </v-flex>
        <v-flex xs10>
          {{player.email}}
        </v-flex>
      </v-layout>
      <v-layout row wrap v-if="_fields.includes('dob')">
        <v-flex xs2 text-xs-right>
          <strong>Birthdate:</strong>
        </v-flex>
        <v-flex xs10>
          {{player.dobFormatted}}
        </v-flex>
      </v-layout>
      <v-layout row wrap v-if="_fields.includes('cityState')">
        <v-flex xs2 text-xs-right>
          <strong>City, St:</strong>
        </v-flex>
        <v-flex xs10>
          {{player.city}}, {{player.state}}
        </v-flex>
      </v-layout>
      <v-layout row wrap v-if="_fields.includes('aau') && sanctioningBody.toLowerCase() !== 'aau'">
        <v-flex xs2 text-xs-right>
          <strong>AAU #:</strong>
        </v-flex>
        <v-flex xs10>
          {{player.aau || 'NA'}}
        </v-flex>
      </v-layout>
      <v-layout row wrap v-if="_fields.includes('avp') && sanctioningBody.toLowerCase() !== 'avp'">
        <v-flex xs2 text-xs-right>
          <strong>AVP #:</strong>
        </v-flex>
        <v-flex xs10>
          {{player.avp || 'NA'}}
        </v-flex>
      </v-layout>
      <v-layout row wrap v-if="_fields.includes('cbva') && sanctioningBody.toLowerCase() !== 'cbva'">
        <v-flex xs2 text-xs-right>
          <strong>CBVA #:</strong>
        </v-flex>
        <v-flex xs10>
          {{player.cbva || 'NA'}}
        </v-flex>
      </v-layout>
      <v-layout row wrap v-if="_fields.includes('usav') && sanctioningBody.toLowerCase() !== 'usav'">
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
  props: ['fields', 'requiredFields', 'player', 'sanctioningBodyId', 'i', 'bgColor'],
  data () {
    return {
    }
  },
  computed: {
    _fields () {
      return this.fields.concat(this.requiredFields)
    },
    sanctioningBody () {
      return this.sanctioningBodyId ? this.sanctioningBodyId.toLowerCase().startsWith('avp') ? 'AVP' : this.sanctioningBodyId : ''
    }
  }
}
</script>

<style>

</style>
