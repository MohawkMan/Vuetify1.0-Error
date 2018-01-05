<template>
    <v-dialog
      v-model="dialog"
      max-width="400"
      persistent
    >
      <v-card v-if="editablePhone">
        <v-toolbar dark color="color2">
          <v-toolbar-title v-if="mode === 'edit'">{{editablePhone.number | usPhone}}</v-toolbar-title>
          <v-toolbar-title v-else>New Phone</v-toolbar-title>
          <v-spacer></v-spacer>
          <v-btn icon dark small @click.native="cancel">
            <v-icon>close</v-icon>
          </v-btn>
        </v-toolbar>
        <v-card-title>
          <v-layout>
            <v-flex>
              <v-text-field
                v-if="mode === 'add'"
                label="Number"
                v-model="editablePhone.number"
                mask="phone"
                required
                :rules="[
                  () => $v.editablePhone.number.required || 'A phone number is required',
                  () => $v.editablePhone.number.minLength || 'A valid phone number is required',
                  () => $v.editablePhone.number.maxLength || 'A valid phone number is required'
                ]"
              ></v-text-field>
            </v-flex>
          </v-layout>
          <v-list two-line>
            <v-list-tile avatar>
              <v-list-tile-action>
                <v-checkbox v-model="editablePhone.isPrimary" :disabled="!canEditPrimary"></v-checkbox>
              </v-list-tile-action>
              <v-list-tile-content>
                <v-list-tile-title>Primary</v-list-tile-title>
                <v-list-tile-sub-title>Use this as your primary number</v-list-tile-sub-title>
              </v-list-tile-content>
            </v-list-tile>
            <v-list-tile avatar>
              <v-list-tile-action>
                <v-checkbox v-model="editablePhone.isPublic"></v-checkbox>
              </v-list-tile-action>
              <v-list-tile-content>
                <v-list-tile-title>Public</v-list-tile-title>
                <v-list-tile-sub-title>Visible to the public</v-list-tile-sub-title>
              </v-list-tile-content>
            </v-list-tile>
            <v-list-tile avatar>
              <v-list-tile-action>
                <v-checkbox v-model="editablePhone.isSMS"></v-checkbox>
              </v-list-tile-action>
              <v-list-tile-content>
                <v-list-tile-title>SMS Enabled</v-list-tile-title>
                <v-list-tile-sub-title>Phone can receive text messages</v-list-tile-sub-title>
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
import Phone from '../../classes/Phone'
import vbl from '../../VolleyballLife'
import { validationMixin } from 'vuelidate'
import { required, numeric, minLength, maxLength } from 'vuelidate/lib/validators'
import * as mutations from '../../store/MutationTypes'

export default {
  props: ['phone', 'listCount'],
  mixins: [validationMixin],
  validations: {
    editablePhone: {
      number: { required, numeric, minLength: minLength(10), maxLength: maxLength(10) }
    }
  },
  data () {
    return {
      dialog: false,
      editablePhone: null,
      saving: false
    }
  },
  computed: {
    saveable () {
      return !this.saving && this.mode === 'add' ? !this.$v.$invalid : this.dirty
    },
    dirty () {
      return JSON.stringify(this.phone) !== JSON.stringify(this.editablePhone)
    },
    mode () {
      return this.editablePhone.id === 0 ? 'add' : 'edit'
    },
    canEditPrimary () {
      return this.mode === 'add' ? this.listCount > 0 : this.listCount > 1
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
      this.axios.post(vbl.user.phone, this.editablePhone)
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
    phone (newVal, oldVal) {
      if (newVal) {
        this.editablePhone = new Phone(newVal)
        this.dialog = true
        this.$v.editablePhone.number.$reset()
      }
    }
  }
}
</script>

<style>

</style>
