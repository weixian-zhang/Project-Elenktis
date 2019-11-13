<template>
  <div class="home">
    <b-navbar class="portal-navbar py-0">
      <b-navbar-brand href="#">Swift Policy Controller</b-navbar-brand>
      <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>
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
    <div class="container login-container">
      <div class="row justify-content-md-center">
        <div class="col-md-auto">
          <h1 class="login-title">Home</h1>
          <!-- <h2 class="login-lead">User : {{ this.$msal.data.user }}</h2> -->
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import auth from "@/store/modules/auth";
import "@/components/Navbar";

@Component
export default class Login extends Vue {
}
</script>

<style scoped>
  .login-container {
    max-width: 400px;
  }
  .login-title {
    font-size: 19px;
    font-weight: 600;
    margin-top: 48px;
    line-height: 1.4;
  }
  .login-lead {
    font-size: 15px;
    line-height: 1.4;
    margin-bottom: 32px;
  }
  .login-btn {
    font-size: 14px;
  }
</style>