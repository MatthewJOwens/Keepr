<template>
  <div class="dashboard container-fluid">
    <h1>WELCOME TO YOUR DASHBOARD</h1>
    <!-- <h4>public: {{ publicKeeps }} user: {{ userKeeps }}</h4> -->
    <!-- TODO take user name and split on space and only take first word -->
    {{$auth.user.name}}'s Keeps:
    <div class="row">
      <div class="col-10 offset-1">
        <div class="card-columns my-2">
          <Keep v-for="keep in userKeeps" :key="keep.id" :keepData="keep" />
        </div>
      </div>
      <div class="col-1 d-flex justify-content-end align-items-start">
        <CreateKeep />
      </div>
    </div>
  </div>
</template>

<script>
import CreateKeep from "../components/CreateKeep.vue";
import Keep from "../components/Keep.vue";
export default {
  name: "dashboard",
  data: {
    return: {}
  },
  mounted() {
    this.$store.dispatch("getUserKeeps");
  },
  computed: {
    userKeeps() {
      return this.$store.state.userKeeps;
    }
  },
  methods: {},
  components: { Keep, CreateKeep }
};
</script>

<style></style>
