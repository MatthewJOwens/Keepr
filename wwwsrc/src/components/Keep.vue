// individual Keeps -- card and modal
// keep count, share count, view count
// counts highlight on hover
// image and name on main part
// image, name, and description on modal
// modal data-target="#modal-{{keepData.id}}", I think
<template>
  <div class="keep card">
    <img
      class="card-img-top"
      :src="keepData.img"
      :alt="keepData.name"
      data-toggle="modal"
      :data-target="'#modal-' + keepData.id"
    />
    <div class="card-body" data-toggle="modal" :data-target="'#modal-' + keepData.id">
      <h5 class="card-title">{{keepData.name}}</h5>
    </div>
    <div class="card-footer row d-flex justify-content-around m-0">
      <div class="col">
        <i class="fab fa-korvue"></i>
        {{keepData.keeps}}
      </div>
      <div class="col">
        <i class="fas fa-share"></i>
        {{keepData.shares}}
      </div>
      <div class="col">
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
            <div class="col">K:{{keepData.keeps}}</div>
            <div class="col">S:{{keepData.shares}}</div>
            <div class="col">V:{{keepData.views}}</div>
            <div class="col-12" v-if="keepData.userId == this.$auth.user.email">
              <button class="btn btn-danger btn-block" @click="deleteKeep()" />
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
      console.log("Delete ", keepData.id);
    }
  },
  components: {}
};
</script>


<style scoped>
</style>