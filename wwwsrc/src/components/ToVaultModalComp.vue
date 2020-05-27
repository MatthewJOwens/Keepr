<template>
  <div class="to-vault-modal-comp">
    <div class="modal fade" :id="'toVaultModal-' + keepData.id" tabindex="-1" role="dialog">
      <div class="modal-dialog modal-dialog-centered" role="document">
        <div class="modal-content">
          <div class="modal-header justify-content-center">
            <img
              class="img-fluid"
              style="max-height: 50vh"
              :src="keepData.img"
              :alt="keepData.name"
            />
          </div>
          <div class="modal-body">
            <div class="row">
              <div class="col-6">
                Save to an existing vault...
                <form @submit.prevent="saveToVault()">
                  <div class="input-group">
                    <select
                      class="custom-select"
                      name="vaultSelection"
                      id="vaultSelect"
                      aria-label="Vault selection"
                    >
                      <option selected>Choose...</option>
                      <option
                        v-for="vault in vaults"
                        :key="'option'+vault.id"
                        :value="vault.id"
                      >{{vault.name}}</option>
                    </select>
                    <div class="input-group-append">
                      <button class="btn btn-outline-info" type="submit">Save</button>
                    </div>
                  </div>
                </form>
              </div>
              <div class="col-6 border-left border-secondary">
                ...or create a new one.
                <form @submit.prevent="createVaultAndSave()">
                  <div class="form-group">
                    <input
                      type="text"
                      class="form-control"
                      :id="'vaultModalName'+ keepData.id"
                      v-model="newVault.name"
                      placeholder="Vault Name"
                    />
                  </div>
                  <div class="form-group">
                    <input
                      type="text"
                      class="form-control"
                      :id="'vaultModalDesc'+ keepData.id"
                      v-model="newVault.description"
                      placeholder="Vault Description"
                    />
                  </div>
                  <button class="btn btn-outline-info" type="submit">Create & Save</button>
                </form>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
export default {
  name: "to-vault-modal-comp",
  data() {
    return {
      newVault: {}
    };
  },
  props: ["keepData", "vaults"],
  computed: {},
  methods: {
    upKeptCount() {
      this.keepData.keeps++;
      this.$store.dispatch("editKeep", this.keepData);
    },

    createVaultAndSave() {
      console.log(
        "Creating vault " +
          this.newVault.name +
          " and saving Keep " +
          this.keepData.id
      );
      //save the various variables to vaultkeep
      this.vaultKeep.name = this.newVault.name;
      this.vaultKeep.description = this.newVault.description;
      this.vaultKeep.keepId = this.keepData.id;
      // need this.vaultKeep.vaultId but have to get that from the return from the first request
      //  create and save to vault
      this.$store.dispatch("createVaultAndSave", this.vaultKeep);
      //  Keep count ++
      this.upKeptCount();
      $("#createKeepModal").modal("hide");
    },
    saveToVault() {
      console.log(event.target.vaultSelection.value);
      //save variables to vaultkeep
      this.vaultKeep.vaultId = parseInt(event.target.vaultSelection.value);
      this.vaultKeep.keepId = this.keepData.id;
      //  save to vault
      this.$store.dispatch("saveToVault", this.vaultKeep);
      //  Keep count ++
      this.upKeptCount();
      $("#toVaultModal-" + this.keepData.id).modal("hide");
    }
  },
  components: {}
};
</script>


<style scoped>
</style>