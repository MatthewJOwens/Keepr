<template>
  <div class="home container-fluid">
    <div class="row">
      <div class="col-10 offset-1">
        <h1>Welcome Home</h1>
        <div class="card-columns">
          <Keep v-for="keep in keeps" :keepData="keep" :key="keep.id">Keep component not loaded.</Keep>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "home",
  computed: {
    user() {
      return this.$store.state.user;
    }
  },
  methods: {
    logout() {
      this.$store.dispatch("logout");
    }
  }
};
</script>
