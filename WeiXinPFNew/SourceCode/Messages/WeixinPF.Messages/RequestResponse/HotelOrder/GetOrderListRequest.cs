namespace WeixinPF.Messages.RequestResponse
{
    public class GetOrderListRequest
    {
        public int Wid { get; set; }
        public string OpenId { get; set; }
        public int HotelId { get; set; }
    }
}
