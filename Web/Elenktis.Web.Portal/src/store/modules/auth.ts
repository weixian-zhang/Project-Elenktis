import Vue from "vue";
import { Module, VuexModule, Action, getModule } from "vuex-module-decorators";
import store from '@/store';

@Module({ namespaced: true, name: "auth", store, dynamic: true })
class AuthModule extends VuexModule {
  // State
  authenticated: Boolean = false;

  // Getters
  get isAuthenticated() {
    return this.authenticated;
  }

  // Mutations

  // Actions
  @Action
  login() {
    return new Promise((resolve, reject) => {
      Vue.prototype.$msal.signIn();
      resolve()
    })
  }
}

export default getModule(AuthModule);