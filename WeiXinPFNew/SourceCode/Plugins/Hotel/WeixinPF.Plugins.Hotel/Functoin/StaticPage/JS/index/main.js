
var vm = new Vue({
    el: '#div_app',

    data: {
        currentView: 'view-room',

        person: {},
        imgDatas:[],
        headerTitle:''
    },
    components: {
        'app-header': {
            template: '#app-header-template',
            props:['headerTitle']
        },
        'app-footer': AppFooter,
        'view-about': ViewAbout,

        'view-room':  ViewRoom,
        'view-order': ViewOrder,
        'app-image':{
            template:'#view-image-template',
            props: ['viewImages']
        }
    },

    events: {
        'child-msg': function (msg) {
            // �¼��ص��ڵ� `this` �Զ��󶨵�ע������ʵ����
            this.currentView = msg;
            switch (msg){
                case 'view-about':
                    this.headerTitle='关于';
                    window.document.title='关于';
                    break;
                case 'view-room':
                    this.headerTitle='房型选择';
                    window.document.title='房型选择';
                    break;
                case 'view-order':
                    this.headerTitle='订单';
                    window.document.title='订单';
                    break;
            }
        },
        'onimgDataDispatch':function(data){
            this.imgDatas=data;
        }
    },

    methods: {
        getData: function (callBack) {
            //// GET request
            //this.$http.get('/Functoin/Service/HotelService.asmx/GetJson').then(function (response) {
            //
            //    // set data on vm
            //    this.person = response.data;
            //
            //}, function (response) {
            //
            //    // handle error
            //});
            var data=[{no:1 ,img:'http://www.cloudorg.com.cn/upload/201512/14/201512141710309494.png'},
                {no:2 ,img:'http://www.cloudorg.com.cn/upload/201512/14/201512141712439846.jpg'},
            ];
            callBack(data);
        },
        initSwiper:function(){
            var mySwiper = new Swiper('.swiper-container', {
                direction: 'vertical',
                loop: true,

                // �����Ҫ��ҳ��?
                pagination: '.swiper-pagination',

                // �����Ҫǰ�����˰��?
                nextButton: '.swiper-button-next',
                prevButton: '.swiper-button-prev',

                // �����Ҫ������?
                scrollbar: '.swiper-scrollbar'
            });
        }
    },
    ready: function () {
        var self=this;

        this.openid= getQueryStringByName('openid');
        this.wid='123';//getQueryStringByName('wid');

        //1.�Ӹ������ȡ���ݣ����ݸ�app-image���?
        this.getData(function(data){
            self.imgDatas=data;

            setTimeout(function(){
                self.initSwiper();
            },1000);
        });

        //2.������ͼ����ʱ���ȡ���ݣ����ݸ�app-image���?
        //this.currentView='view-room';



    }
})
