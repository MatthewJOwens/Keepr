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
            <h1>Form body</h1>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancel</button>
            <button type="button" class="btn btn-primary">Save</button>
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
      newKeep: {}
    };
  },
  computed: {},
  methods: {},
  components: {}
};
</script>


<style scoped>
</style>