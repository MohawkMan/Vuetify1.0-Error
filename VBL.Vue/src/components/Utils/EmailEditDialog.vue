<template>
    <v-dialog
      v-model="dialog"
      max-width="400"
      persistent
    >
      <v-card v-if="editableEmail">
        <v-toolbar dark color="color2">
          <v-toolbar-title v-if="mode === 'edit'">{{editableEmail.address}}</v-toolbar-title>
          <v-toolbar-title v-else>New Email</v-toolbar-title>
          <v-spacer></v-spacer>
          <v-btn icon dark small @click.native="cancel">
            <v-icon>close</v-icon>
          </v-btn>
        </v-toolbar>
        <v-card-title>
          <v-layout v-if="mode === 'add'">
            <v-flex>
              <v-text-field
                label="Address"
                v-model="editableEmail.address"
                required
                :rules="[
                  () => $v.editableEmail.address.required || 'An email address is required',
                  () => $v.editableEmail.address.email || 'A valid email address is required'
                ]"
              ></v-text-field>
            </v-flex>
          </v-layout>
          <v-layout row v-if="!editableEmail.isVerified">
            <v-flex text-xs-center xs12>
              This email has not been verified
            </v-flex>
          </v-layout>
          <v-layout row v-if="!editableEmail.isVerified">
            <v-flex xs12>You must verify the email before you can edit the settings</v-flex>
          </v-layout>
          <v-list two-line>
            <v-list-tile avatar>
              <v-list-tile-action>
                <v-checkbox v-model="editableEmail.isPrimary" :disabled="!canEditPrimary"></v-checkbox>
              </v-list-tile-action>
              <v-list-tile-content>
                <v-list-tile-title>Primary</v-list-tile-title>
                <v-list-tile-sub-title>Use this as your primary address</v-list-tile-sub-title>
              </v-list-tile-content>
            </v-list-tile>
            <v-list-tile avatar>
              <v-list-tile-action>
                <v-checkbox v-model="editableEmail.isPublic" :disabled="!editableEmail.isVerified"></v-checkbox>
              </v-list-tile-action>
              <v-list-tile-content>
                <v-list-tile-title>Public</v-list-tile-title>
                <v-list-tile-sub-title>Visible to the public</v-list-tile-sub-title>
              </v-list-tile-content>
            </v-list-tile>
          </v-list>
          <v-layout row text-xs-center>
            <v-flex>
              <v-btn
                :disabled="!saveable"
                @click="save"
                :loading="saving"
                ripple
                color="color3 white--text"
                class="pl-0"
              >
                <v-icon class="mr-2 ml-0">save</v-icon>
                Save
              </v-btn>
            </v-flex>
          </v-layout>

        </v-card-title>
      </v-card>
    </v-dialog>
</template>

<script>
import Email from '../../classes/Email'
import SDK from '../../VBL'
import { validationMixin } from 'vuelidate'
import { required, email } from 'vuelidate/lib/validators'
import * as mutations from '../../store/MutationTypes'

export default {
  props: ['email', 'listCount'],
  mixins: [validationMixin],
  validations: {
    editableEmail: {
      address: { required, email }
    }
  },
  data () {
    return {
      dialog: false,
      editableEmail: null,
      saving: false
    }
  },
  computed: {
    saveable () {
      return !this.saving && this.mode === 'add' ? !this.$v.$invalid : this.dirty
    },
    dirty () {
      return JSON.stringify(this.email) !== JSON.stringify(this.editableEmail)
    },
    mode () {
      return this.editableEmail.id === 0 ? 'add' : 'edit'
    },
    canEditPrimary () {
      return this.listCount > 1 && this.editableEmail.isVerified
    }
  },
  methods: {
    cancel () {
      this.dialog = false
      this.$v.$reset()
      this.$emit('cancel')
    },
    save () {
      if (!this.saveable) return
      this.saving = true
      const sdk = new SDK(this.axios)
      sdk.user.postEmail(this.editableEmail)
        .then((response) => {
          console.log(response)
          this.$store.commit(mutations.SET_USER, response.data)
          this.saving = false
          this.dialog = false
        })
        .catch((error) => {
          console.log(error)
        })
    }
  },
  watch: {
    email (newVal, oldVal) {
      if (newVal) {
        this.editableEmail = new Email(newVal)
        this.dialog = true
        this.$v.editableEmail.address.$reset()
      }
    }
  }
}
</script>

<style>

</style>
