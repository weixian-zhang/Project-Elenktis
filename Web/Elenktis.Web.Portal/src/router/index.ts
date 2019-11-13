import Vue from "vue";

import VueRouter, { Route } from "vue-router";
import Home from "../views/dashboard/Home.vue";
import Login from "../views/auth/Login.vue";

Vue.use(VueRouter);

const routes = [
  {
    path: "/",
    name: "home",
    component: Home,
  },
  {
    path: "/login",
    name: "login",
    component: Login,
  }
];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes
});

router.beforeEach((to: Route, from: Route, next) => {
  if (to.path == "/login") {
    if (router.app.$msal.isAuthenticated()) next({ path: "/" })
    else next();
  } else {
    if (!router.app.$msal.isAuthenticated()) next({ path: "/login" })
    next();
  }
});

export default router;
