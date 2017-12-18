<template>
  <v-card>
    <v-card-title>
      <v-expansion-panel popout>
        <v-expansion-panel-content
          v-for="(division,i) in divisions" 
          :key="i" 
          ripple
          class="color5"
          :value="division.id === expandId"
          >
          <div slot="header" :id="`division${division.id}`">{{division.divisionsString}}</div>
          <v-card>
            <team-list :division="division">
            </team-list>
          </v-card>
        </v-expansion-panel-content>
      </v-expansion-panel>
    </v-card-title>
  </v-card>
</template>

<script>
import Teams from '../../components/Tournament/TeamList.vue'

export default {
  props: ['divisions', 'expandId'],
  components: {
    'team-list': Teams
  }
}
</script>

<style>
.color5 {
  border-top: 1px solid rgba(0,0,0,0.12) !important;
}
</style>
