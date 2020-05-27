<template>
  <div class="vault-save-dropdown">
    <button
      class="btn btn-info dropdown-toggle"
      data-toggle="dropdown"
      aria-haspopup="true"
      aria-expanded="false"
    >Keep in a vault?</button>
    <div class="dropdown-menu p-1">
      <VaultListDropdown v-for="vault in vaults" :key="vault.id" :vaultListItem="vault" />
      <div class="dropdown-divider"></div>
      <form @submit.prevent="createVaultAndSave()">
        <div class="form-group">
          <label for="makeVaultFormName">Create a new vault</label>
          <input
            type="text"
            class="form-control"
            id="makeVaultFormName"
            placeholder="Vault Name"
            v-model="newVault.name"
          />
          <button type="submit" class="btn btn-primary mt-1">Create & Save</button>
        </div>
      </form>
    </div>
  </div>
</template>


<script>
import VaultListDropdown from "./VaultListDropdown.vue";
export default {
  name: "vault-save-dropdown",
  data() {
    return {
      newVault: {}
    };
  },
  props: ["keepData"],
  computed: {
    vaults() {
      return this.$store.state.userVaults;
    }
  },
  methods: {
    createVaultAndSave() {
      console.log("Creating vault and saving " + this.keepData.id);
      this.newVault = {};
    }
  },
  components: { VaultListDropdown }
};
</script>


<style scoped>
</style>