var AppFooter = Vue.extend({
    template: '#app-footer-template',
    props: ['currentView'],

    methods: {
        notify: function (msg) {
            this.currentView = msg
            if (this.currentView.trim()) {
                this.$dispatch('onChangeView', this.currentView)
            }
        }
    }
})