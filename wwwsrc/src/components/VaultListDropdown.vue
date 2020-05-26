<template>
  <div class="vault-list-dropdown">
    <div class="dropdown-item" href="#">{{vaultListItem.name}}</div>
  </div>
</template>


<script>
export default {
  name: "vault-list-dropdown",
  props: ["vaultListItem"],
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