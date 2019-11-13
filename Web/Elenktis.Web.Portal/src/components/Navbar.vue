<template>
  <b-navbar class="portal-navbar py-0">
    <b-navbar-brand href="#">Swift Policy Controller</b-navbar-brand>
    <b-collapse id="nav-collapse" is-nav>
      <b-navbar-nav class="ml-auto">
        <b-nav-item-dropdown class="portal-dropdown" v-bind:text="this.$msal.data.user.name" right no-caret>
          <b-dropdown-text>
            <div class="portal-dropdown-name">{{ this.$msal.data.user.name }}</div>
            <div class="portal-dropdown-email">{{ this.$msal.data.user.userName }}</div>
          </b-dropdown-text>
          <b-dropdown-divider></b-dropdown-divider>
          <b-dropdown-item href="#">Logout</b-dropdown-item>
        </b-nav-item-dropdown>
      </b-navbar-nav>
    </b-collapse>
  </b-navbar>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import auth from "@/store/modules/auth";

@Component
export default class Navbar extends Vue{};
</script>