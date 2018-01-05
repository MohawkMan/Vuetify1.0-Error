<template>
  <v-list two-line>
    <v-list-tile v-for="(phone,i) in list" :key="i">
      <v-list-tile-action>
        <v-icon color="color1" v-if="i === 0">
          local_phone
        </v-icon>
      </v-list-tile-action>
      <v-list-tile-content>
        <v-list-tile-title>
          {{phone.number | usPhone }}
        </v-list-tile-title>
        <v-list-tile-sub-title>
          {{phone.propsString}}
        </v-list-tile-sub-title>
      </v-list-tile-content>
      <v-list-tile-action @click="selectPhone(phone)">
        <v-btn flat icon ripple color="color3">
          <v-icon>settings</v-icon>

        </v-btn>
      </v-list-tile-action>
    </v-list-tile>
    <v-list-tile v-if="phonelist.length == 0">
      <v-list-tile-action>
        <v-icon color="color1">
          local_phone
        </v-icon>
      </v-list-tile-action>
      <v-list-tile-content>
        <v-list-tile-title>
          No phone number on file
        </v-list-tile-title>
      </v-list-tile-content>
      <v-list-tile-action>
        <v-icon color="color3">add_circle</v-icon>
      </v-list-tile-action>
    </v-list-tile>

    <phone-editor :phone="selectedPhone" :listCount="phonelist.length" @cancel="onCancel"></phone-editor>

  </v-list>
</template>

<script>
import Phone from '../../classes/Phone'
import Editor from './PhoneEditDialog.vue'

export default {
  props: ['phonelist'],
  data () {
    return {
      selectedPhone: null
    }
  },
  computed: {
    list () {
      return this.phonelist.map(p => new Phone(p))
    },
    mode () {
      return this.selectedPhone.id > 0 ? 'add' : 'edit'
    }
  },
  methods: {
    selectPhone (phone) {
      this.selectedPhone = new Phone(phone)
    },
    onCancel () {
      this.selectedPhone = null
    },
    addPhone () {
      this.selectedPhone = new Phone()
    }
  },
  components: {
    'phone-editor': Editor
  }
}
</script>

<style>

</style>
