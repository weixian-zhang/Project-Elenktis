import Vue from "vue";
import BootstrapVue from "bootstrap-vue";
import msalPlugin from 'vue-msal/lib/plugin';

import App from "./App.vue";
import router from "./router";
import store from "./store";

// Bootstrap styles
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'

// Custom styles
import "@/assets/css/opensans.css";
import "@/assets/css/elenktis.css";

Vue.config.productionTip = false;

Vue.use(BootstrapVue)
Vue.use(msalPlugin, {
  auth: {
    clientId: process.env.VUE_APP_MSAL_CLIENT_ID,
    tenantId: process.env.VUE_APP_MSAL_TENANT_ID,
    redirectUri: "http://localhost:8080/",
    onToken: function (ctx: any, error: any, response: any) {
      if (response) {
        console.log(response);
      }
    }
  },
  framework: {
    globalMixin: true
  }
});

new Vue({
  router,
  store,
  render: h => h(App),
}).$mount("#elenktis");