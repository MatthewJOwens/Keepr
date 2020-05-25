import Vue from "vue";
import Vuex from "vuex";
import Axios from "axios";
import router from "../router";

Vue.use(Vuex);

let baseUrl = location.host.includes("localhost")
  ? "https://localhost:5001/"
  : "/";

let api = Axios.create({
  baseURL: baseUrl + "api/",
  timeout: 3000,
  withCredentials: true
});

export default new Vuex.Store({
  state: {
    publicKeeps: [],
    userKeeps: [],
  },
  mutations: {
    setPublicKeeps(state, publicKeeps) {
      state.publicKeeps = publicKeeps
    },
    setUserKeeps(state, userKeeps) {
      state.userKeeps = userKeeps
    }
  },
  actions: {
    setBearer({ }, bearer) {
      api.defaults.headers.authorization = bearer;
    },
    resetBearer() {
      api.defaults.headers.authorization = "";
    },
    async getPublicKeeps({ commit, dispatch }) {
      try {
        let res = await api.get("keeps")
        commit("setPublicKeeps", res.data)
      } catch (error) {
        console.error(error)
      }
    },
    async getUserKeeps({ commit, dispatch }) {
      try {
        // breaks convention, best way?
        let res = await api.get("keeps/user")
        commit("setUserKeeps", res.data)
      } catch (error) {
        console.error(error)
      }
    },
    async createKeep({ commit, dispatch }, newKeep) {
      try {
        let res = await api.post("keeps", newKeep)
        if (newKeep.isPrivate == true) {
          router.push({ name: "dashboard", params: {} })
        } else {
          dispatch("getPublicKeeps")
        }
      } catch (error) {
        console.error(error)
      }
    },
    async deleteKeep({ commit, dispatch }, keepToDelete) {
      try {
        let res = api.delete("keeps/" + keepToDelete.id)
        if (res) {
          console.log("Successfully deleted.")
          // do I need to do anything with vaultkeeps?
        }
        if (keepToDelete.isPrivate == false) {
          router.push({ name: "mainVue", params: {} })
        } else if (keepToDelete.isPrivate == true) {
          router.push({ name: "dashboard", params: {} })
        }
      } catch (error) {
        console.error(error)
      }
    }
  }
});
