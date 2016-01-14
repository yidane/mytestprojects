using System;

namespace WeixinPF.Messages.RequestResponse
{
    public class GetHotelResponse
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string CoverSrc { get; set; }
        public string Tel { get; set; }
        public string Address { get; set; }
        public string Introduction { get; set; }
    }
}
