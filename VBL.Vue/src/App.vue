<template>
  <v-app id="vbl">
    <v-content>
      <v-container fill-height>
        <v-layout row wrap align-center>
          <v-flex xs8 offset-xs2>
            <v-layout row wrap text-xs-center>
              <v-flex xs12>
                <v-alert outline color="error" icon="warning" value="true">
                  Yayyyyy ! started!!!
                </v-alert>
              </v-flex>
            </v-layout>
          </v-flex>
        </v-layout>
      </v-container>
    </v-content>
  </v-app>
</template>

<script>
  export default {
    data: () => ({

    })
  }
</script>
