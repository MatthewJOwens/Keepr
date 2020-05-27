<template>
  <div class="dashboard container-fluid">
    <h1>WELCOME TO YOUR DASHBOARD</h1>
    <!-- <h4>public: {{ publicKeeps }} user: {{ userKeeps }}</h4> -->
    <button class="btn btn-sm btn-primary" type="button" @click="vaultsView()" v-if="!showVaults">
      <span>Vaults</span>
    </button>
    <button class="btn btn-sm btn-primary" type="button" @click="keepsView()" v-if="!showKeeps">
      <span>Keeps</span>
    </button>
    <div v-show="showVaults">
      {{$auth.user.name}}'s Vaults:
      <div class="row">
        <div class="col-10 offset-1">
          <div class="card-columns">
            <div
              class="card border-primary mb-3"
              v-for="vault in userVaults"
              :key="vault.id"
              @click="chooseVault(vault)"
            >
              <div class="card-body text-primary">
                <h5 class="card-title">{{vault.name}}</h5>
                <p class="card-text">{{vault.description}}</p>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div v-show="showKeeps">
      <!-- TODO take user name and split on space and only take first word -->
      {{$auth.user.name}}'s Keeps:
      <div class="row">
        <div class="col-10 offset-1">
          <div class="card-columns my-2">
            <Keep v-for="keep in userKeeps" :key="keep.id" :keepData="keep" :vaults="userVaults" />
          </div>
        </div>
        <div class="col-1 p-0 d-flex justify-content-end align-items-start">
          <CreateKeep />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import CreateKeep from "../components/CreateKeep.vue";
import Keep from "../components/Keep.vue";
export default {
  name: "dashboard",
  data() {
    return {
      showKeeps: false,
      showVaults: true
    };
  },
  mounted() {
    this.$store.dispatch("getUserKeeps");
    this.$store.dispatch("getUserVaults");
  },
  computed: {
    userKeeps() {
      return this.$store.state.userKeeps;
    },
    userVaults() {
      return this.$store.state.userVaults;
    },
    vaultKeeps() {
      return this.$store.state.vaultKeeps;
    }
  },
  methods: {
    keepsView() {
      this.showVaults = false;
      this.showKeeps = true;
    },
    vaultsView() {
      this.showKeeps = false;
      this.showVaults = true;
    },
    chooseVault(vault) {
      console.log(vault.name);
      this.$store.dispatch("getVaultKeeps", vault.id);
      this.showVaults = false;
      this.$router.push("vaults/" + vault.id);
    }
  },
  components: { Keep, CreateKeep }
};
</script>

<style></style>
