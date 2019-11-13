<template>
  <div class="home">
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

@Component
export default class Login extends Vue {}
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