<template>
  <v-menu
    lazy
    :close-on-content-click="false"
    v-model="dobPicker"
    transition="scale-transition"
    offset-y
    full-width
    :nudge-right="40"
    max-width="290px"
    min-width="290px"
  >
    <v-text-field
      slot="activator"
      label="Birthdate"
      readonly
      v-model="player.dobFormatted"
      required
      :rules="[() => $v.player.dob.required || 'A birthdate is required']"
      @blur="player.dob = parseDate(player.dobFormatted)"
    ></v-text-field>
    <v-date-picker
      no-title
      scrollable
      actions
      v-model="player.dob"
      @input="player.dobFormatted = formatDate($event)"
    >
      <template slot-scope="{ save, cancel }">
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn flat color="primary" @click="cancel">Cancel</v-btn>
          <v-btn flat color="primary" @click="save">OK</v-btn>
        </v-card-actions>
      </template>
    </v-date-picker>
  </v-menu>
</template>

<script>
export default {

}
</script>

<style>

</style>
