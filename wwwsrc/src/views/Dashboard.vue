<template>
  <div class="dashboard container-fluid">
    <h1>WELCOME TO YOUR DASHBOARD</h1>
    <!-- <h4>public: {{ publicKeeps }} user: {{ userKeeps }}</h4> -->
    <button class="btn btn-sm btn-secondary" type="button" @click="toggleVaultsKeeps()">
      <span v-if="showKeeps">Vaults</span>
      <span v-if="showVaults">Keeps</span>
    </button>
    <div v-show="showVaults">
      {{$auth.user.name}}'s Vaults:
      <div class="row">
        <div class="col-10 offset-1">{{userVaults}}</div>
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
      showKeeps: true,
      showVaults: false
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
    }
  },
  methods: {
    toggleVaultsKeeps() {
      this.showKeeps = !this.showKeeps;
      this.showVaults = !this.showVaults;
    }
  },
  components: { Keep, CreateKeep }
};
</script>

<style></style>
