<template>
  <div class="vault container-fluid">
    <div class="row mt-3">
      <div class="col-1">
        <button
          type="button"
          class="rounded-circle btn btn-danger text-light shadow-sm border-0 m-2"
          @click="deleteVault()"
        >
          <i class="far fa-trash-alt"></i>
        </button>
      </div>
      <div class="col-10">
        <div class="card-columns">
          <Keep v-for="keep in vaultKeeps" :key="keep.id" :keepData="keep" :vaults="userVaults" />
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import Keep from "../components/Keep.vue";
export default {
  name: "vault",
  data() {
    return {};
  },
  mounted() {
    this.$store.dispatch("getUserVaults");
    // this.$store.dispatch("getVaultKeeps", this.$route.params.id)
  },
  computed: {
    userVaults() {
      return this.$store.state.userVaults;
    },
    vaultKeeps() {
      return this.$store.state.vaultKeeps;
    }
  },
  methods: {
    deleteVault() {
      let confirm = window.confirm(
        "Are you sure you wish to delete this vault? \nThis cannot be undone."
      );
      if (confirm) {
        this.$store.dispatch("deleteVault", this.$route.params.id);
      }
    }
  },
  components: { Keep }
};
</script>


<style scoped>
</style>