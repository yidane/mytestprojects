﻿var AppFooter = Vue.extend({
    template: '#app-footer-template',
    data: function () {
        return { msg: 'view-room' }
    },
    methods: {
        notify: function (msg) {
            this.msg = msg
            if (this.msg.trim()) {
                this.$dispatch('child-msg', this.msg)
            }
        }
    }
})