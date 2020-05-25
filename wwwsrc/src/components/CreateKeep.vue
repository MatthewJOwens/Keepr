// make a + button
// align it to the bottom right in the MainVue file's div
// modal pops up with form
// name, description, img, isPrivate
<template>
  <div class="create-keep">
    <button
      type="button"
      class="rounded-circle btn btn-outline-primary text-dark shadow-sm border-0 m-2"
      data-toggle="modal"
      data-target="#createKeepModal"
    >
      <b>+</b>
    </button>
    <div class="modal fade" id="createKeepModal" tabindex="-1" role="dialog">
      <div class="modal-dialog modal-dialog-centered" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h3 class="modal-title" id="createKeepModalTitle">Keep something!</h3>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
            <!--TODO insert form -->
            <!-- name (req), description, img(req), isPrivate(req) -->
            <!-- <h1>Form body</h1> -->
            <form @submit.prevent="createKeep()">
              <div class="form-group">
                <label for="keepName">Post title:</label>
                <input
                  type="text"
                  class="form-control"
                  id="keepName"
                  required
                  v-model="newKeep.name"
                />
                <small class="form-text text-muted" id="nameHelp">Required</small>
              </div>
              <div class="form-group">
                <label for="keepImg">Image URL:</label>
                <input type="text" class="form-control" id="keepImg" required v-model="newKeep.img" />
              </div>

              <div class="form-check">
                <input
                  type="checkbox"
                  id="isPublic"
                  class="form-check-input"
                  v-model="newKeep.isPrivate"
                />
                <label for="isPublic" class="form-check-label">Check here to keep it private</label>
              </div>

              <div class="form-group">
                <label for="keepDescription">Post description:</label>
                <textarea class="form-control" id="keepDescription" v-model="newKeep.description" />
                <small class="form-text text-muted" id="descHelp">Also required</small>
              </div>
              <button type="submit" class="btn btn-primary">Save</button>
            </form>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancel</button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
export default {
  name: "create-keep",
  data() {
    return {
      newKeep: {
        isPrivate: true
      }
    };
  },
  computed: {},
  methods: {
    createKeep() {
      // this.newKeep.isPrivate = !event.target.isPublic;
      this.$store.dispatch("createKeep", this.newKeep);
      this.newKeep = {};
    }
  },
  components: {}
};
</script>


<style scoped>
</style>