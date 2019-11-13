<template>
  <div class="login">
    <div class="navbar-banner">
      <div class="navbar-banner-text">Swift Policy Controller</div>
    </div>
    <div class="container login-container">
      <div class="row justify-content-md-center">
        <div class="col-md-auto">
          <h1 class="login-title">Login</h1>
          <h2 class="login-lead">To get started, login with Swift Office.</h2>
          <button class="btn btn-success btn-block login-btn" @click="this.login">Login with Swift Office</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import auth from "@/store/modules/auth";

@Component
export default class Login extends Vue {
  async login() {
    auth.login().then(() => {
      console.log(this.$msal.data.user);
      console.log(this.$msal.data.graph);
      console.log(this.$msal.data.isAuthenticated);
      console.log(this.$msal.data.accessToken);
    });
  }
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