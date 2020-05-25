<template>
  <div class="home container-fluid">
    <div class="row">
      <div class="col-10 offset-1">
        <div class="card-columns my-2">
          <Keep
            v-for="keep in publicKeeps"
            :keepData="keep"
            :key="keep.id"
          >Keep component not loaded.</Keep>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import Keep from "../components/Keep.vue";
export default {
  name: "home",
  computed: {
    user() {
      return this.$store.state.user;
    },
    publicKeeps() {
      return this.$store.state.publicKeeps;
    }
  },
  mounted() {
    this.$store.dispatch("getPublic");
  },
  methods: {
    logout() {
      this.$store.dispatch("logout");
    }
  },
  components: { Keep }
};
</script>