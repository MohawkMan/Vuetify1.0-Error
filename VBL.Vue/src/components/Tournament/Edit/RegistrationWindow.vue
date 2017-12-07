<template>
  <v-container>
    <!-- Start Row 1  -->
    <v-layout row wrap>
      <v-flex xs6 sm3>
        <v-text-field
          v-model="window.fee"
          label="Entry Fee"
          type="number"
          prepend-icon="attach_money"
          required
          @blur="formatCurrency"
        ></v-text-field>
      </v-flex>
      <v-flex xs6 sm3>
        <v-radio-group v-model="window.feeIsPerTeam" row>
          <v-radio label="Per Team" :value="true" ></v-radio>
          <v-radio label="Per Player" :value="false"></v-radio>
        </v-radio-group>
      </v-flex>
    </v-layout>
    <!-- Start Row 2  -->
    <v-layout row wrap>
      <v-flex xs6 sm3>
        <v-menu
          lazy
          :close-on-content-click="false"
          v-model="regStart"
          transition="scale-transition"
          offset-y
          full-width
          :nudge-right="40"
          max-width="290px"
          min-width="290px"
          hide-details
        >
          <v-text-field
            slot="activator"
            label="Registration Open Date"
            prepend-icon="event"
            readonly
            v-model="window.startDateFormatted"
            @blur="window.startDate = parseDate(window.startDateFormatted)"
          ></v-text-field>
          <v-date-picker 
            no-title
            scrollable
            actions
            v-model="window.startDate"
            :allowed-dates="regAllowedOpen"
            @input="window.startDateFormatted = formatDate($event)"
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
      </v-flex>
      <v-flex xs6 sm3>
        <v-menu
          lazy
          :close-on-content-click="false"
          v-model="regStartT"
          transition="scale-transition"
          offset-y
          full-width
          :nudge-right="40"
          max-width="290px"
          min-width="290px"
        >
          <v-text-field
            slot="activator"
            label="Registration Open Time"
            prepend-icon="access_time"
            readonly
            v-model="window.startTime"
          ></v-text-field>
          <v-time-picker actions v-model="window.startTime">
            <template slot-scope="{ save, cancel }">
              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn flat color="primary" @click="cancel">Cancel</v-btn>
                <v-btn flat color="primary" @click="save">OK</v-btn>
              </v-card-actions>
            </template>
          </v-time-picker>
        </v-menu>
      </v-flex>
      <v-flex xs6 sm3>
        <v-menu
          lazy
          :close-on-content-click="false"
          v-model="regEnd"
          transition="scale-transition"
          offset-y
          full-width
          :nudge-right="40"
          max-width="290px"
          min-width="290px"
          hide-details
        >
          <v-text-field
            slot="activator"
            label="Registration Close Date"
            prepend-icon="event"
            readonly
            v-model="window.endDateFormatted"
            @blur="window.endDate = parseDate(window.endDateFormatted)"
          ></v-text-field>
          <v-date-picker
            no-title
            scrollable
            actions
            v-model="window.endDate"
            @input="window.endDateFormatted = formatDate($event)"
            :allowed-dates="regAllowedClose">
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
      </v-flex>
      <v-flex xs6 sm3>
        <v-menu
          lazy
          :close-on-content-click="false"
          v-model="regEndT"
          transition="scale-transition"
          offset-y
          full-width
          :nudge-right="40"
          max-width="290px"
          min-width="290px"
        >
          <v-text-field
            slot="activator"
            label="Registration Close Time"
            prepend-icon="access_time"
            readonly
            v-model="window.endTime"
          ></v-text-field>
          <v-time-picker actions v-model="window.endTime">
            <template slot-scope="{ save, cancel }">
              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn flat color="primary" @click="cancel">Cancel</v-btn>
                <v-btn flat color="primary" @click="save">OK</v-btn>
              </v-card-actions>
            </template>
          </v-time-picker>
        </v-menu>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import moment from 'moment'

export default {
  props: ['window', 'beforeDate'],
  data () {
    return {
      regStart: false,
      regStartT: false,
      regEnd: false,
      regEndT: false
    }
  },
  computed: {
  },
  methods: {
    formatCurrency () {
      this.window.fee = parseFloat(this.window.fee).toFixed(2)
    },
    regAllowedOpen (date) {
      if (!this.beforeDate) return true
      return moment(date) <= moment(this.beforeDate)
    },
    regAllowedClose (date) {
      if (!this.beforeDate) return true
      if (!this.window.startDate) return true
      return moment(date) <= moment(this.beforeDate) && moment(date) >= moment(this.window.startDate)
    },
    formatDate (date) {
      if (!date) return null

      const [year, month, day] = date.split('-')
      return `${month}/${day}/${year}`
    },
    parseDate (date) {
      if (!date) return null

      const [month, day, year] = date.split('/')
      return `${year}-${month.padStart(2, '0')}-${day.padStart(2, '0')}`
    }
  }
}
</script>

