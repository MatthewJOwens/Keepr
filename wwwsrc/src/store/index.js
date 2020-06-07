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
    userVaults: [],
    vaultKeeps: []
  },
  mutations: {
    setPublicKeeps(state, publicKeeps) {
      state.publicKeeps = publicKeeps
    },
    setUserKeeps(state, userKeeps) {
      state.userKeeps = userKeeps
    },
    setUserVaults(state, userVaults) {
      state.userVaults = userVaults
    },
    setVaultKeeps(state, vaultKeeps) {
      state.vaultKeeps = vaultKeeps
    },
    spliceKeep(state, keepId) {
      let data = state.userKeeps.findIndex(keep => keep.id == keepId)
      state.userKeeps.splice(data, 1)
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
    async getVaultKeeps({ commit, dispatch }, vaultId) {
      try {
        let res = await api.get("vaults/" + vaultId + "/keeps")
        commit("setVaultKeeps", res.data)
      } catch (error) {
        console.error(error)
      }
    },
    async createKeep({ commit, dispatch }, newKeep) {
      try {
        let res = await api.post("keeps", newKeep)
        if (newKeep.isPrivate == true) {
          if (newKeep.fromRoute != 'dashboard') {
            router.push({ name: "dashboard", params: {} })
          }
          dispatch("getUserKeeps")
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
          commit("spliceKeep", keepToDelete.id)
        }

      } catch (error) {
        console.error(error)
      }
    },
    async editKeep({ commit, dispatch }, keepToEdit) {
      try {
        let res = api.put("keeps/" + keepToEdit.id, keepToEdit)
        // need to do anything else? do something with the results?
      } catch (error) {
        console.error(error)
      }
    },
    async getUserVaults({ commit, dispatch }) {
      try {
        let res = await api.get("vaults/")
        commit('setUserVaults', res.data)
      } catch (error) {
        console.error(error)
      }
    },
    // first save to a vault
    async saveToVault({ commit, dispatch }, vaultKeep) {
      // Okay, had to make a vaultkeep to take in keep and vault I'm saving to
      // make sure to make vaultKeep in method
      // i have the vault ID, so just create a vaultKeep with vaultId and keepId
      try {
        let res = await api.post("vaultKeeps", vaultKeep)
        // do I need to do anything with the results? go to dashboard and view them?
        // maybe later
        // for now, increment Keeps count on front end
      } catch (error) {
        console.error(error)
      }
    },
    async createVaultAndSave({ commit, dispatch }, vaultKeep) {
      try {
        // post the new vault name and description to vaults db, await response
        // backend can't parse all the info into a Vault model or a VaultKeep model, so make a new object
        let firstInfo = {}
        let secondInfo = {}
        firstInfo.name = vaultKeep.name;
        firstInfo.description = vaultKeep.description;
        let res1 = await api.post("vaults", firstInfo);
        // after response, if successful, set vaultKeep's vaultId with result and make vaultKeep db entry
        if (res1) {
          secondInfo.vaultId = res1.data.id
          secondInfo.keepId = vaultKeep.keepId
        }
        let res2 = await api.post("vaultKeeps", secondInfo)
        //await that response, then push to the dashboard to see entry?
        // if (res2) {
        //   router.push({ name: "dashboard", params: {} })
        // }
      } catch (error) {
        console.error(error)
      }
    },
    async removeFromVault({ commit, dispatch }, vk) {
      try {
        await api.delete("vaultKeeps/" + vk.vaultKeepId)
        dispatch("getVaultKeeps", vk.vaultId)
      } catch (error) {
        console.error(error)
      }
    },
    async deleteVault({ commit, dispatch }, id) {
      try {
        await api.delete("vaults/" + id)
        router.push({ name: "dashboard" })
      } catch (error) {
        console.error(error)
      }
    }
  }
});
