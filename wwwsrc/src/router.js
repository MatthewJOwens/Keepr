import Vue from "vue";
import Router from "vue-router";
// @ts-ignore
import Home from "./views/Home.vue";
// @ts-ignore
import MainVue from "./views/MainVue.vue";
// @ts-ignore
import Dashboard from "./views/Dashboard.vue";
import { authGuard } from "@bcwdev/auth0-vue";

Vue.use(Router);

export default new Router({
  routes: [
    {
      path: "/",
      name: "home",
      component: Home
    },
    {
      path: "/dashboard",
      name: "dashboard",
      component: Dashboard,
      beforeEnter: authGuard
    },
    {
      path: "/main",
      name: "mainVue",
      component: MainVue,
      beforeEnter: authGuard
    },
    {
      path: "/vaults/:id",
      name: "vault",
      // @ts-ignore
      component: () => import(/*webpackChunkName: "vault" */ './views/Vault.vue'),
      beforeEnter: authGuard
    }
  ]
});
