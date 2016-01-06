var AppFooter = Vue.extend({
    template: '#app-footer-template',
    props: ['wid','openid','hotel','currentView','orderCount'],

    methods: {
        notify: function (msg) {
            // this.currentView = msg
            // if (this.currentView.trim()) {
            //     this.$dispatch('onChangeView', this.currentView);
            // }
            this.$dispatch('onimgDataDispatch', this.hotel.imgDatas);
        }
    }
});
