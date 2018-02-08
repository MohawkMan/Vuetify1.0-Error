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
      <!-- Copy Button -->
      <v-btn
        v-if="currentTournament"
        fab
        dark
        small
        color="teal darken-4"
        @click.stop="copyTournament"
      >
        <v-icon>content_copy</v-icon>
      </v-btn>
      <!-- ADD Button -->
      <v-btn
        fab
        dark
        small
        color="teal"
        @click.stop="addTournament"
      >
        <v-icon>add</v-icon>
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
    </v-speed-dial>
    <!-- EDIT DIALOG START -->
    <simple-editor 
      v-if="userIsAdmin"
      :tournamentIn="tournament" 
      :open="editDialog" 
      @closeClick="editDialog = false" 
    ></simple-editor>
    <!-- EDIT DIALOG END -->
    <!-- REGISTRATION UPLOAD DIALOG START -->
    <registration-uploader 
      v-if="userIsAdmin && currentTournament && currentTournament.isEditable"
      :open="uploadDialog" 
      :tournament="currentTournament"
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
import SDK from '../VBL'

export default {
  props: ['currentTournament'],
  data () {
    return {
      dialOpen: false,
      editDialog: false,
      uploadDialog: false,
      tournament: null
    }
  },
  computed: {
    userIsAdmin () {
      return this.$store.getters.user && this.$route.params.username && this.$store.getters.user.isPageAdmin(this.$route.params.username)
    }
  },
  methods: {
    editTournament () {
      this.tournament = this.currentTournament
      this.editDialog = true
    },
    copyTournament () {
      const sdk = new SDK(this.axios)
      sdk.tournament.getTournamentCopyById(this.currentTournament.id)
      .then((response) => {
        this.tournament = response.data
        this.editDialog = true
      })
      .catch((error) => {
        console.log(error)
      })
    },
    addTournament () {
      this.tournament = null
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
