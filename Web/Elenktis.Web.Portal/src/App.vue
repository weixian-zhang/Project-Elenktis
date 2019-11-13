<template>
  <div id="elenktis">
    <Navbar></Navbar>
    <router-view />
  </div>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import Navbar from "@/components/Navbar";

@Component({
  components: {
    Navbar
  }
})
export default class App extends Vue {}
</script>