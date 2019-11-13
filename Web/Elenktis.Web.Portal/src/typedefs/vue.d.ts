import Vue from "vue";
import { MSAL } from 'vue-msal/lib/src/main'

// Module augmentation to add MSAL to Vue's global instance properties
// so we can do something like vm.$msal.xx where vm is the Vue instance
// https://vuejs.org/v2/guide/typescript.html#Augmenting-Types-for-Use-with-Plugins
declare module "vue/types/vue" {
  interface Vue {
    $msal: MSAL
  }
}