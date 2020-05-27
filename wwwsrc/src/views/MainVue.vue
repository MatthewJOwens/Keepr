<template>
  <div class="mainVue container-fluid">
    <div class="row">
      <div class="col-10 offset-1">
        <div class="card-columns my-2">
          <Keep
            v-for="keep in publicKeeps"
            :keepData="keep"
            :key="keep.id"
            :vaults="userVaults"
          >Keep component not loaded.</Keep>
        </div>
      </div>
      <div class="col-1 p-0 d-flex justify-content-end align-items-start">
        <CreateKeep />
      </div>
    </div>
  </div>
</template>


<script>
import Keep from "../components/Keep.vue";
import CreateKeep from "../components/CreateKeep.vue";
export default {
  name: "mainVue",
  data() {
    return {};
  },
  computed: {
    publicKeeps() {
      return this.$store.state.publicKeeps;
    },
    userVaults() {
      return this.$store.state.userVaults;
    }
  },
  mounted() {
    this.$store.dispatch("getPublicKeeps");
    this.$store.dispatch("getUserVaults");
  },
  methods: {},
  components: { Keep, CreateKeep }
};
</script>


<style scoped>
</style>