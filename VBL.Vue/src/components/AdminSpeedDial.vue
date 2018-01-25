<template>
  <div>
    <v-speed-dial
      v-if="userIsAdmin"
      v-model="dialOpen"
      bottom
      right
      fixed
      direction="top"
      transition="slide-y-reverse-transition"
    >
      <!-- Primary button -->
      <v-btn
        slot="activator"
        color="color2"
        dark
        fab
        hover
        v-model="dialOpen"
        >
        <v-icon>settings</v-icon>
        <v-icon>close</v-icon>
      </v-btn>
      <!-- Edit Button -->
      <v-btn
        v-if="currentTournament && currentTournament.isEditable"
        fab
        dark
        small
        color="green"
        @click.stop="editTournament"
      >
        <v-icon>edit</v-icon>
      </v-btn>
      <!-- Reg Upload Button -->
      <v-btn
        v-if="currentTournament && currentTournament.isEditable"
        fab
        dark
        small
        color="indigo"
        @click.stop="uploadDialog = true"
      >
        <v-icon>cloud_upload</v-icon>
      </v-btn>
      <v-btn
        fab
        dark
        small
        color="teal"
        @click.stop="addTournament"
      >
        <v-icon>add</v-icon>
      </v-btn>
    </v-speed-dial>
    <!-- EDIT DIALOG START -->
    <simple-editor 
      v-if="userIsAdmin"
      :tournamentDto="tourney" 
      :open="editDialog" 
      @closeClick="editDialog = false" 
    ></simple-editor>
    <!-- EDIT DIALOG END -->
    <!-- REGISTRATION UPLOAD DIALOG START -->
    <registration-uploader 
      v-if="userIsAdmin && currentTournament && currentTournament.isEditable"
      :open="uploadDialog" 
      :tourney="tourney"
      @close="uploadDialog = false"
      @complete="uploadDialog = false"
    ></registration-uploader>
    <!-- REGISTRATION UPLOAD DIALOG END -->
  </div>
</template>

<script>
// import Tournament from '../classes/Tournament'
import RegistrationUploader from '../components/Tournament/RegistrationUploader.vue'
import EditorSimple from '../components/Tournament/Edit/Simple.vue'

export default {
  props: ['currentTournament'],
  data () {
    return {
      dialOpen: false,
      editDialog: false,
      uploadDialog: false,
      tourney: null
    }
  },
  computed: {
    userIsAdmin () {
      return this.$store.getters.user && this.$route.params.username && this.$store.getters.user.isPageAdmin(this.$route.params.username)
    }
  },
  methods: {
    editTournament () {
      this.tourney = this.currentTournament
      this.editDialog = true
    },
    addTournament () {
      this.tourney = null
      this.editDialog = true
    }
  },
  components: {
    'simple-editor': EditorSimple,
    'registration-uploader': RegistrationUploader
  }
}
</script>

<style>

</style>
