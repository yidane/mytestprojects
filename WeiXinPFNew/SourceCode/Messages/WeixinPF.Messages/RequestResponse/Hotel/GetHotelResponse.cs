using System;

namespace WeixinPF.Messages.RequestResponse
{
    public class GetHotelResponse
    {
        private bool _listmode = true;

        public int id { set; get; }

        public int? wid { set; get; }

        public string hotelName { set; get; }

        public string hotelAddress { set; get; }

        public string hotelPhone { set; get; }

        public string mobilPhone { set; get; }

        public string noticeEmail { set; get; }

        public string emailPws { set; get; }

        public string smtp { set; get; }

        public string coverPic { set; get; }

        public string topPic { set; get; }

        public int? orderLimit { set; get; }

        public bool listMode
        {
            set { _listmode = value; }
            get { return _listmode; }
        }

        public int? messageNotice { set; get; }

        public string pwd { set; get; }

        public string hotelIntroduct { set; get; }

        public string orderRemark { set; get; }

        public DateTime? createDate { set; get; }

        public int? sortid { set; get; }

        public decimal? xplace { set; get; }

        public decimal? yplace { set; get; }


        public string HotelCode { get; set; }

        public string Operator { get; set; }

        public string HotelLevel { get; set; }

        public bool? Recommend { get; set; }
    }
}
