<template>
  <div class="vault container-fluid">
    <div class="row">
      <div class="col-10 offset-1">
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
  methods: {},
  components: { Keep }
};
</script>


<style scoped>
</style>