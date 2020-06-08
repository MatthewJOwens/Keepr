<template>
  <div class="keep card" @mouseenter="showOverlay = true" @mouseleave="showOverlay = false">
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
      <div class="col-4 p-0">
        <i
          class="fab fa-korvue"
          :class="{hoverIcon: !isHome}"
          data-toggle="modal"
          :data-target="'#toVaultModal-' + keepData.id"
        ></i>
        {{keepData.keeps}}
      </div>
      <!-- TODO bring up share menu? -->
      <div class="col-4 p-0">
        <i class="fas fa-share hoverIcon"></i>
        {{keepData.shares}}
      </div>
      <div class="col-4 p-0">
        <i
          class="far fa-eye hoverIcon"
          data-toggle="modal"
          :data-target="'#modal-' + keepData.id"
          @click="upViewCount()"
        ></i>
        {{keepData.views}}
      </div>
      <div v-if="isVault" class="col-12">
        <button class="btn btn-warning btn-block btn-sm" @click="removeFromVault()">
          <small>Remove from Vault</small>
        </button>
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
            <!-- NOTE maybe replace these with FA icons? -->
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
                v-if="!isHome"
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
    <ToVaultModalComp v-if="!isHome" :keepData="keepData" :vaults="vaults" />
  </div>
</template>

<script>
import ToVaultModalComp from "./ToVaultModalComp.vue";
import VaultSaveDropdown from "./VaultSaveDropdown.vue";
export default {
  name: "keep",
  props: ["keepData", "vaults"],
  data() {
    return {
      showOverlay: false
    };
  },
  computed: {
    isHome() {
      return this.$route.name == "home";
    },
    isVault() {
      return this.$route.name == "vault";
    }
  },
  methods: {
    deleteKeep() {
      let confirm = window.confirm("Are you sure you want to delete this?");
      if (confirm) {
        this.$store.dispatch("deleteKeep", this.keepData);
      }
      $("#modal-" + this.keepData.id).modal("hide");
    },
    upViewCount() {
      this.keepData.views++;
      this.$store.dispatch("editKeep", this.keepData);
    },

    upShareCount() {
      this.keepData.shares++;
      this.$store.dispatch("editKeep", this.keepData);
    },

    removeFromVault() {
      let tempVK = this.keepData;
      tempVK.vaultId = parseInt(this.$route.params.id);
      this.$store.dispatch("removeFromVault", this.keepData);
    }
  },
  components: { VaultSaveDropdown, ToVaultModalComp }
};
</script>


<style scoped>
.hoverIcon:hover {
  color: red !important;
}
</style>