//ȫ�����ã�vue-resource
Vue.http.options.root = '/Functoin';
Vue.http.headers.common['Authorization'] = 'Basic YXBpOnBhc3N3b3Jk';

////ȫ�����ã�vue-validator
//Vue.validator('idcard', function (val) {
//    val = val.toUpperCase();
//    //身份证号码为15位或�?18位，15位时全为数字�?18位前17位为数字，最后一位是校验位，可能为数字或字符X�?
//    return (/(^\d{15}$)|(^\d{17}([0-9]|X)$)/.test(val));
//})
//
//Vue.validator('mobile', function (val) {
//
//    return (/^1[3|4|5|8|9][0-9]\d{8}$/ .test(val));
//})



var vm = new Vue({
    el: '#div_app',

    data: {
        currentView: '',
        person: {},
        imgDatas: [],
        headerTitle: '',
        wid:0,
        openid:'',
        hotel: {},
        room: {
            totalPrice:0,
            costPrice:0,
            roomType:''
        },
        order:{
            id:0,
            discount:0,
            totalPrice:0,
            costPrice:0,
            orderNum:1,
            status:-1,
            orderUser:{}
        },
        orderCount:0,
        swiper:{}
    },
    components: {
        'app-header': {
            template: '#app-header-template',
            props: ['headerTitle']
        },
        'app-footer': AppFooter,
        'view-about': ViewAbout,

        'view-room': ViewRoom,
        'view-order': ViewOrder,
        'view-orderCreate': ViewOrderCreate,
        'app-image': {
            template: '#view-image-template',
            props: ['viewImages']
        }
    },

    events: {
        'onChangeView': function (msg) {
            this.currentView = msg;

        },
        'onimgDataDispatch': function (data) {

            this.imgDatas = data;
            this.changeImgs();
        },
        'onviewOrderCreateDispatch': function (data) {
            //this.currentView='view-orderCreate';
            this.room = data;
        },
        'onUpdateOrderNumberDispatch':function(data){

                this.orderCount=data;

        },
        'onOrderDispatch':function(data){
            this.order=data;
        }
    },
    watch: {
        'currentView': function (val, oldVal) {
            var hotelImgType='hotelImg';
            switch (val) {
                case 'view-about':
                    this.headerTitle = '关于';
                    window.document.title = '关于';
                    break;
                case 'view-room':
                    this.headerTitle = '房型选择';
                    window.document.title = '房型选择';
                    break;
                case 'view-order':
                    this.headerTitle = '订单';
                    window.document.title = '订单';
                    break;
                case 'view-orderCreate':
                    this.headerTitle = '订单';
                    window.document.title = '订单';
                    hotelImgType='roomImg';
                    break;
            }

            if(hotelImgType=='hotelImg')
            {
                this.imgDatas = [{name: 0, url: this.hotel.coverSrc}];
                this.changeImgs();
            }
        }
    },
    methods: {
        getData: function (callBack) {
          var self = this;
          this.getHotelData(function (data) {
              self.hotel = data;

              //self.hotel.imgDatas = [{name: 0, url: 'http://www.cloudorg.com.cn/upload/201512/14/201512141710309494.png'}
              //];
              self.imgDatas=[{name: 0, url: self.hotel.coverSrc}];
              self.changeImgs();
          });
          this.getOrderCount(function (data) {
              self.orderCount = data;

          });

            // var data = [{no: 1, img: 'http://www.cloudorg.com.cn/upload/201512/14/201512141710309494.png'},
            //     {no: 2, img: 'http://www.cloudorg.com.cn/upload/201512/14/201512141712439846.jpg'},
            // ];
            // callBack(data);
        },
        getOrderCount:function(callBack){
            this.$http.get('Service/HotelService.asmx/GetOrderCount'
                , {wid: this.wid, openid: this.openid,hotelId:this.hotel.id}).then(function (response) {
                    if (response.data&&response.data.success) {
                        callBack(response.data.data);
                    } else{
                        $.toast("获取订单数量失败!");
                    }
                }, function (response) {

                    // handle error
                });
        },
        getHotelData: function (callBack) {

            this.$http.get('Service/HotelService.asmx/GetHotelInfo'
                , {wid: this.wid, openid: this.openid,hotelId:this.hotel.id}).then(function (response) {
                    if (response.data&&response.data.success) {
                        callBack(response.data.data);
                    } else{
                        $.toast("获取酒店信息失败!");
                    }
                }, function (response) {

                    // handle error
                });
        },
        getQueryString:function(){
            var wid=getQueryStringByName('wid');
            var hotelId=getQueryStringByName('hotelId');
            var openid=getQueryStringByName('openid');
            if(!openid)
            {
                console.log( '没有openid，跳转获取openid?');
                document.location.href=this.getOpenid();
                return;
            }
            this.getQueryData(wid,hotelId,openid);
        },
        initSwiper: function () {
            this.swiper = new Swiper('.swiper-container', {
                pagination: '.swiper-pagination',
                centeredSlides: true,
                paginationClickable: true
            });
        },
        changeImgs:function(){
            if(this.imgDatas)
            {
                this.swiper.removeAllSlides(); //�Ƴ�ȫ��
                var data=[];
                for(var i=0;i<this.imgDatas.length;i++)
                {
                    var div='<div class="swiper-slide"><img class="header-img" src="'+ this.imgDatas[i].url +'"></div>';
                    data.push(div);
                }
                this.swiper.appendSlide(data);
            }

        },

        getOpenid:function(){
          //todo:跳转获取openid
          var openid='test';
          return document.location.href+"&openid="+openid;
        },
        getQueryData:function(wid,hotelId, openid) {
          if (!this.openid) {
            this.openid = openid;
            this.wid = wid;
            this.hotel.id= hotelId;
            this.getData();
          }

        },
        initRouter:function(){
            var self = this;
          var routes = {


          '/room': function () {

            self.currentView='view-room';
        },
        '/about': function () {

            self.currentView='view-about';
      },
      '/order': function () {

        self.currentView='view-order';
      },
      '/order/:orderId': function (orderId) {

          if (orderId) {
              self.order.id=orderId;
          }
        self.currentView='view-orderCreate';

      },
              '/order/:orderId/:roomId': function (orderId,roomId) {

                  if (orderId) {
                      self.order.id=orderId;
                  }
                  if (roomId) {
                      self.room.id=roomId;
                  }
                  self.currentView='view-orderCreate';

              }
          };

          var router = Router(routes);

          router.init('/room');
        },
        initPreLoader:function(){
            $.showPreloader();
        }
    },

    ready: function () {
        var self = this;
        this.initSwiper();
        this.getQueryString();
        this.initRouter();
        this.initPreLoader();



    }
});
