<template>
  <v-card>
    <v-card-title>
      <div :id="mapName"></div>
    </v-card-title>
  </v-card>
</template>

<script>
export default {
  props: ['name'],
  data: function () {
    return {
      mapName: this.name + "-map",
    }
  },
  mounted () {
    const element = document.getElementById(this.mapName)
    const options = {
      zoom: 14,
      center: new google.maps.LatLng(51.501527,-0.1921837)
    }
    const map = new google.maps.Map(element, options);  }
}
</script>

<style>

</style>
