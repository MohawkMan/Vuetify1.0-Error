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
  <v-container v-else grid-list-sm>
    <v-layout row>
      <v-flex xs12 sm12 md10 xl8 offset-md1 offset-xl2>
        <v-card v-if="organization.photos.length < 2" color="grey darken-2" height="300px">
          <v-card-media
            v-if="organization.photos.length > 0"
            :src="organization.photos[0].url"
            height="300px"
          >
          </v-card-media>
        </v-card>
        <v-carousel v-else 
          hide-delimiters	
          style="height: 300px;">
          <v-carousel-item
            v-for="(photo,i) in organization.photos"
            v-bind:key="i"
            v-bind:src="photo.url"
            transition="fade"
            reverseTransition="fade"
          >
          </v-carousel-item>
        </v-carousel>
        <v-card class="logo-card" color="white" height="50px">
            <v-layout align-center>
              <v-flex text-xs-center>
                <h1>{{organization.name}}</h1>
              </v-flex>
            </v-layout>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import vbl from '../../VolleyballLife'
export default {
  props: ['username'],
  data () {
    return {
      loading: false,
      organization: null
    }
  },
  methods: {
    loadOrganization () {
      if (this.loading) return
      this.loading = true
      this.axios.get(vbl.organization.get(this.username))
      .then((response) => {
        this.organization = response.data
        this.loading = false
      })
      .catch((error) => {
        console.log(error)
      })
    }
  },
  watch: {
    username (newVal, oldVal) {
      console.log('watch')
      this.loadOrganization()
    }
  },
  created () {
    console.log('created')
    this.loadOrganization()
  }
}
</script>

<style>
.logo-card {
  top: -25px;
  left: 15px;
  width: 50%
}
</style>
