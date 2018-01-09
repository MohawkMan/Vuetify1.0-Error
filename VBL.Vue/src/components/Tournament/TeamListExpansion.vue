<template>
  <v-card>
    <v-card-title>
      <v-expansion-panel popout>
        <v-expansion-panel-content
          v-for="(division,i) in _divisions" 
          :key="i" 
          ripple
          class="color5"
          :value="i === 0"
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
  computed: {
    _divisions () {
      return this.divisions.sort((a, b) => {
        var nameA = a.divisionsString.toUpperCase()
        var nameB = b.divisionsString.toUpperCase()
        if (nameA < nameB) {
          return 1
        }
        if (nameA > nameB) {
          return -1
        }

        // names must be equal
        return 0
      })
    }
  },
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
