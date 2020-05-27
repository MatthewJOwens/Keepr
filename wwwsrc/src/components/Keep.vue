// individual Keeps -- card and modal
// keep count, share count, view count
// counts highlight on hover
// image and name on main part
// image, name, and description on modal
// modal data-target="#modal-{{keepData.id}}", I think
<template>
  <div class="keep card" @mouseenter="showOverlay = true" @mouseleave="showOverlay = false">
    <!-- <div class="position-absolute text-white" style="top: 0" v-if="showOverlay">
      <div class="btn-group dropdown">
        <VaultSaveDropdown :keepData="keepData" :vaults="vaults" />
      </div>
    </div>-->
    <!-- TODO On Hover (@mouseover, @mouseleave) save to vault dropdown -->
    <!-- dropdown lists vaults and add new vault option -->
    <!-- put in its own component with v-if="isAuthenticated"? -->
    <img
      class="card-img-top"
      :src="keepData.img"
      :alt="keepData.name"
      data-toggle="modal"
      :data-target="'#modal-' + keepData.id"
      @click="upViewCount()"
    />
    <div
      class="card-body"
      data-toggle="modal"
      :data-target="'#modal-' + keepData.id"
      @click="upViewCount()"
    >
      <h5 class="card-title">{{keepData.name}}</h5>
    </div>
    <div class="card-footer row d-flex justify-content-around m-0">
      <!-- TODO highlight on hover -->
      <div class="col p-0">
        <i
          class="fab fa-korvue hover"
          data-toggle="modal"
          :data-target="'#toVaultModal-' + keepData.id"
        ></i>
        {{keepData.keeps}}
      </div>
      <!-- TODO bring up share menu? -->
      <div class="col p-0">
        <i class="fas fa-share hover"></i>
        {{keepData.shares}}
      </div>
      <div class="col p-0">
        <i
          class="far fa-eye hover"
          data-toggle="modal"
          :data-target="'#modal-' + keepData.id"
          @click="upViewCount()"
        ></i>
        {{keepData.views}}
      </div>
    </div>
    <div class="modal fade" :id="'modal-' + keepData.id" tabindex="-1" role="dialog">
      <div class="modal-dialog modal-dialog-centered" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <img class="img-fluid" :src="keepData.img" :alt="keepData.name" data-dismiss="modal" />
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body text-center">
            <h3>{{keepData.name}}</h3>
            <p class="card-text">{{keepData.description}}</p>
          </div>
          <div class="modal-footer row d-flex justify-content-around m-0 text-center">
            <!-- TODO maybe replace these with FA icons? -->
            <div class="col p-0">Kept:{{keepData.keeps}}</div>
            <div class="col p-0">Shared:{{keepData.shares}}</div>
            <div class="col p-0">Viewed:{{keepData.views}}</div>
            <div class="col-12" v-if="this.$auth.isAuthenticated">
              <button
                type="button"
                class="btn btn-block btn-info"
                data-dismiss="modal"
                data-toggle="modal"
                :data-target="'#toVaultModal-' + keepData.id"
              >Keep in a vault?</button>
            </div>
            <div class="col-12" v-if="this.$auth.isAuthenticated && keepData.isPrivate == true">
              <div
                v-if="keepData.userId == this.$auth.user.email || keepData.userId == this.$auth.user.sub"
              >
                <button class="btn btn-danger btn-block" @click="deleteKeep()">Delete Keep</button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
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
import VaultSaveDropdown from "./VaultSaveDropdown.vue";
export default {
  name: "keep",
  props: ["keepData", "vaults"],
  data() {
    return {
      showOverlay: false,
      newVault: {},
      vaultKeep: {}
    };
  },
  computed: {},
  methods: {
    deleteKeep() {
      console.log("Delete ", this.keepData.id);
      let confirm = window.confirm("Are you sure you want to delete this?");
      if (confirm) {
        console.log("Delorted");
        this.$store.dispatch("deleteKeep", this.keepData);
      }
    },
    upViewCount() {
      this.keepData.views++;
      this.$store.dispatch("editKeep", this.keepData);
    },
    upKeptCount() {
      this.keepData.keeps++;
      this.$store.dispatch("editKeep", this.keepData);
    },
    upShareCount() {
      this.keepData.shares++;
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
      this.vaultKeep.vaultId = event.target.vaultSelection.value;
      this.vaultKeep.keepId = this.keepData.id;
      //  save to vault
      this.$store.dispatch("saveToVault", this.vaultKeep);
      //  Keep count ++
      this.upKeptCount();
      $("#toVaultModal-" + this.keepData.id).modal("hide");
    }
  },
  components: { VaultSaveDropdown }
};
</script>


<style scoped>
.hover:hover {
  color: red !important;
}
</style>