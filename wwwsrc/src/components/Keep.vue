// individual Keeps -- card and modal
// keep count, share count, view count
// counts highlight on hover
// image and name on main part
// image, name, and description on modal
// modal data-target="#modal-{{keepData.id}}", I think
<template>
  <div class="keep card">
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
        <i class="fab fa-korvue"></i>
        {{keepData.keeps}}
      </div>
      <!-- TODO bring up share menu? -->
      <div class="col p-0">
        <i class="fas fa-share"></i>
        {{keepData.shares}}
      </div>
      <div class="col p-0">
        <i class="far fa-eye"></i>
        {{keepData.views}}
      </div>
    </div>
    <div class="modal fade" :id="'modal-' + keepData.id" tabindex="-1" role="dialog">
      <div class="modal-dialog modal-dialog-centered" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <img class="img-fluid" :src="keepData.img" :alt="keepData.name" />
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
  </div>
</template>


<script>
export default {
  name: "keep",
  props: ["keepData"],
  data() {
    return {};
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
    }
  },
  components: {}
};
</script>


<style scoped>
</style>