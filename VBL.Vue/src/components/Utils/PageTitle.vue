<template>
    <transition name="underline">
        <v-layout row wrap class="text-xs-center">
            <v-flex>
                <transition name="underline" appear>
                    <h1>{{ title }}</h1>
                </transition>
            </v-flex>
        </v-layout>
    </transition>
</template>

<script>
export default {
  props: [
    'title'
  ]
}
</script>

<style scoped>
    .underline-enter-active {
        transition: 0s;
    }
    .underline-enter-active:after {
        width: 0;
    }
    h1:after {
        background-color: #85cddb;
        height: 3px;
        content: '';
        position: relative;
        display: block;
        margin: 15px auto;
        width: 150px;
        transition: 1.5s cubic-bezier(.87,-.41,.19,1.44);
   }
</style>

