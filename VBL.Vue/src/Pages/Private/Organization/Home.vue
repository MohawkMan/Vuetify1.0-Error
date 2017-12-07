<template>
  <v-container>
    <page-title :title="'Admin Dashboard'"></page-title>
    <v-layout row>
      <v-flex xs12 sm10 offset-sm1>
        <v-card>
          <v-toolbar dark color="color2">
            <v-toolbar-title>{{ pageInfo.name }}</v-toolbar-title>
          </v-toolbar>
          <v-container grid-list-lg>
            <v-layout row wrap>
              <v-flex xs12>
                <v-card color="color3" dark>
                  <v-card-title>
                    <div>
                      <div class="headline">Next Tournament</div>
                      <div>Next Tournament Info</div>
                    </div>
                  </v-card-title>
                  <v-card-actions>
                    <v-btn flat dark>Manage</v-btn>
                  </v-card-actions>
                </v-card>
              </v-flex>
              <v-flex xs12>
                <v-btn to="tournaments/create">
                  Create New Tournament
                </v-btn>
              </v-flex>
            </v-layout>
          </v-container>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import { mapGetters } from 'vuex'

export default {
  props: ['username'],
  data () {
    return {

    }
  },
  computed: {
    ...mapGetters([
      'user',
      'loading'
    ]),
    pageInfo () {
      return this.user.pages.find((page) => {
        return page.userName === this.username
      })
    }
  }
}
</script>

<style>
.headline {
    font-size: 24px!important;
    font-weight: 400;
    line-height: 32px!important;
    letter-spacing: normal!important;
}
</style>
