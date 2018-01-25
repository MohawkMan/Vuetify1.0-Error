<template>
  
</template>

<script>
import Organization from '../../../classes/Organization'
export default {
  data () {
    return {
      organization: null
    }
  },
  created () {
    this.organization = new Organization()
  }
}
</script>

<style>

</style>
