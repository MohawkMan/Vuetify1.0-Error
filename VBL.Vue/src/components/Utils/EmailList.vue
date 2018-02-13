<template>
  <v-list two-line>
    <v-list-tile v-for="(email,i) in list" :key="i">
      <v-list-tile-action>
        <v-icon color="color1" v-if="i === 0">
          mail
        </v-icon>
      </v-list-tile-action>
      <v-list-tile-content>
        <v-list-tile-title>
          {{email.address}}
        </v-list-tile-title>
        <v-list-tile-sub-title>
          {{email.propsString}}
        </v-list-tile-sub-title>
      </v-list-tile-content>
      <v-list-tile-action @click="selectEmail(email)">
        <v-btn flat icon ripple color="color3">
          <v-icon>settings</v-icon>
        </v-btn>
      </v-list-tile-action>
    </v-list-tile>
    <v-list-tile v-if="emailList.length == 0">
      <v-list-tile-action>
        <v-icon color="color1">
          mail
        </v-icon>
      </v-list-tile-action>
      <v-list-tile-content>
        <v-list-tile-title>
          No email address on file
        </v-list-tile-title>
      </v-list-tile-content>
      <v-list-tile-action>
        <v-icon color="color3">add_circle</v-icon>
      </v-list-tile-action>
    </v-list-tile>

    <email-editor :email="selectedEmail" :listCount="emailList.length" @cancel="onCancel"></email-editor>

  </v-list>
</template>

<script>
import Email from '../../classes/Email'
import Editor from './EmailEditDialog.vue'

export default {
  props: ['emailList'],
  data () {
    return {
      selectedEmail: null
    }
  },
  computed: {
    list () {
      return this.emailList.map(p => new Email(p))
    },
    mode () {
      return this.selectedEmail.id > 0 ? 'add' : 'edit'
    }
  },
  methods: {
    selectEmail (email) {
      this.selectedEmail = new Email(email)
    },
    onCancel () {
      this.selectedEmail = null
    },
    add () {
      this.selectedEmail = new Email()
    }
  },
  components: {
    'email-editor': Editor
  }
}
</script>

<style>

</style>
