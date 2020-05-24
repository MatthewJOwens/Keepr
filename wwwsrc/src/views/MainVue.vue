<template>
  <div class="mainVue container-fluid">
    <div class="row">
      <div class="col-10 offset-1">
        <h1>Welcome Home</h1>
        <div class="card-columns">
          <Keep v-for="keep in keeps" :keepData="keep" :key="keep.id">Keep component not loaded.</Keep>
        </div>
      </div>
      <div class="col-1">
        <CreateKeep />
      </div>
    </div>
  </div>
</template>


<script>
export default {
  name: "mainVue",
  data() {
    return {};
  },
  computed: {},
  methods: {},
  components: {}
};
</script>


<style scoped>
</style>