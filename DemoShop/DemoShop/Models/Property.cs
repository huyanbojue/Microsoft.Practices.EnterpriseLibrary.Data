
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using ProtoBuf;

namespace DemoShop.Models
{
    //[Serializable]
    [ProtoContract]
    public class PropertyItem
    {
        [ProtoMember(1)]
        public int Id { get; set; }

        [ProtoMember(2)]
        public string Content { get; set; }

        [ProtoMember(3)]
        public string Title { get; set; }

        [ProtoMember(4)]
        public string Note { get; set; }

        [ProtoMember(5)]
        public double Floors { get; set; }

        [ProtoMember(6)]
        public int Bedrooms { get; set; }

        [ProtoMember(7)]
        public int Bathrooms { get; set; }

        [ProtoMember(8)]
        public string AddressNumber { get; set; }

        [ProtoMember(9)]
        public string AddressCorner { get; set; }

        [ProtoMember(10)]
        public DateTime CreatedDate { get; set; }

        [ProtoMember(11)]
        public DateTime LastUpdatedDate { get; set; }

        [ProtoMember(12)]
        public DateTime? AdsExpirationDate { get; set; }

        [ProtoMember(13)]
        public double AlleyWidth { get; set; }

        [ProtoMember(14)]
        public double PriceProposed { get; set; }

        [ProtoMember(15)]
        public double PriceProposedInVND { get; set; }

        [ProtoMember(16)]
        public double PriceEstimatedInVND { get; set; }

        [ProtoMember(17)]
        public double AreaUsable { get; set; }

        [ProtoMember(18)]
        public double AreaTotalWidth { get; set; }

        [ProtoMember(19)]
        public double AreaTotalLength { get; set; }

        [ProtoMember(20)]
        public double AreaTotal { get; set; }

        [ProtoMember(21)]
        public int ApartmentId { get; set; }

        [ProtoMember(21)]
        public string AdsTypeShortName { get; set; }

        [ProtoMember(22)]
        public PropertyBase AdsType { get; set; }

        [ProtoMember(23)]
        public PropertyBase Location { get; set; }

        [ProtoMember(24)]
        public PropertyBase PaymentMethod { get; set; }


        [ProtoMember(25)]
        public string ImageDefaultAvatar { get; set; }

        [ProtoMember(26)]
        public string newcol26 { get; set; }
        [ProtoMember(27)]
        public string newcol27 { get; set; }
        [ProtoMember(28)]
        public string newcol28 { get; set; }
        [ProtoMember(29)]
        public string newcol29 { get; set; }

    }

    //[Serializable]
    [ProtoContract]
    public class PropertyOptions
    {
        [ProtoMember(1)]
        public int? DomainGroupId { get; set; }
        [ProtoMember(2)]
        public int? AdsTypeId { get; set; }
        [ProtoMember(3)]
        public int? ProvinceId { get; set; }

        [ProtoMember(4)]
        public int? DistrictId { get; set; }
        [ProtoMember(5)]
        public int? WardId { get; set; }
        [ProtoMember(6)]
        public int? StreetId { get; set; }

        [ProtoMember(7)]
        public int? TypeGroupId { get; set; }
        [ProtoMember(8)]
        public int? UserId { get; set; }
        [ProtoMember(9)]
        public string TypeGroupCssClass { get; set; }

        [ProtoMember(10)]
        public int? PriceId { get; set; }
        [ProtoMember(11)]
        public int? AreaId { get; set; }

        [ProtoMember(12)]
        public double? PriceFrom { get; set; }
        [ProtoMember(13)]
        public double? PriceTo { get; set; }

        [ProtoMember(14)]
        public double? AreaFrom { get; set; }
        [ProtoMember(15)]
        public double? AreaTo { get; set; }

        [ProtoMember(16)]
        public string ApartmentIds { get; set; }
        [ProtoMember(17)]
        public string DirectionIds { get; set; }

        [ProtoMember(18)]
        public string TypeIds { get; set; }
        [ProtoMember(19)]
        public double? MinAreaTotal { get; set; }
        [ProtoMember(20)]
        public double? MaxAreaTotal { get; set; }
        [ProtoMember(21)]
        public double? MinPriceProposed { get; set; }
        [ProtoMember(22)]
        public double? MaxPriceProposed { get; set; }
        [ProtoMember(23)]
        public double? IsAuction { get; set; }
        [ProtoMember(24)]
        public double? IsOwner { get; set; }
        [ProtoMember(25)]
        public double? AdsGoodDeal { get; set; }        
    }

    //[Serializable]
    [ProtoContract]
    public class PropertyIndex
    {
        [ProtoMember(1)]
        public int TotalCount { get; set; }
        [ProtoMember(2)]
        public List<PropertyItem> ListProperties { get; set; }
        
    }

    //[Serializable]
    [ProtoContract]
    public class PropertyBase
    {
        [ProtoMember(1)]
        public int Id { get; set; }
        [ProtoMember(2)]
        public string Name { get; set; }
        [ProtoMember(3)]
        public string CssClass { get; set; }
        [ProtoMember(4)]
        public string ShortName { get; set; }

        [ProtoMember(5)]
        public string ContactPhone { get; set; }

        [ProtoMember(6)]
        public string Url { get; set; }
    }

    [ProtoContract]
    public class PropertyTypeConstructions
    {
        [ProtoMember(1)]
        public int Id { get; set; }
        [ProtoMember(2)]
        public string Name { get; set; }
        [ProtoMember(3)]
        public string CssClass { get; set; }

        [ProtoMember(4)]
        public int? GroupId { get; set; }
        [ProtoMember(5)]
        public int? TypeId { get; set; }
        [ProtoMember(6)]
        public int? MinFloor { get; set; }
        [ProtoMember(7)]
        public int? MaxFloor { get; set; }
        [ProtoMember(8)]
        public int? SeqOrder { get; set; }
    }

    public class PropertyStatistics
    {
        /// <summary>
        /// Tất cả
        /// </summary>
        public int CountAll { get; set; }
        /// <summary>
        /// Đang hiển thị
        /// </summary>
        public int CountDelivering { get; set; }
        /// <summary>
        /// Hết hạn hiển thị
        /// </summary>
        public int CountExpired { get; set; }
        /// <summary>
        /// Chờ duyệt
        /// </summary>
        public int CountPending { get; set; }
        /// <summary>
        /// Đang soạn thảo
        /// </summary>
        public int CountDraft { get; set; }
        /// <summary>
        /// Không hợp lệ
        /// </summary>
        public int CountInValid { get; set; }
        /// <summary>
        /// Tạm ngưng
        /// </summary>
        public int CountPause { get; set; }
        /// <summary>
        /// Đã xóa
        /// </summary>
        public int CountDeleted { get; set; }
        /// <summary>
        /// Quảng cáo đang chạy
        /// </summary>
        public int CountAdsDelivering { get; set; }
        /// <summary>
        /// BĐS định giá
        /// </summary>
        public int CountEstimate { get; set; }
        /// <summary>
        /// BĐS trao đổi
        /// </summary>
        public int CountExchange { get; set; }

        /// <summary>
        /// Cần bán
        /// </summary>
        public int CountPropertySelling { get; set; }
        /// <summary>
        /// Cho thuê
        /// </summary>
        public int CountPropertyLeasing { get; set; }
        /// <summary>
        /// Cần mua
        /// </summary>
        public int CountPropertyBuying { get; set; }
        /// <summary>
        /// Cần thuê
        /// </summary>
        public int CountPropertyRengting { get; set; }
    }

    //[Serializable]
    [ProtoContract]
    public class UserGroupContact
    {
        [ProtoMember(1)]
        public int Id { get; set; }
        [ProtoMember(2)]
        public PropertyBase UserGroup { get; set; }
        [ProtoMember(3)]
        public PropertyBase PropertyProvince { get; set; }
        [ProtoMember(4)]
        public PropertyBase PropertyDistrict { get; set; }
        [ProtoMember(5)]
        public PropertyBase PropertyWard { get; set; }
        [ProtoMember(6)]
        public PropertyBase PropertyStreet { get; set; }
        [ProtoMember(7)]
        public PropertyBase PropertyAdsType { get; set; }
        [ProtoMember(8)]
        public string TypeGroupCssClass { get; set; }
        [ProtoMember(9)]
        public string ContactPhone { get; set; }
    }

    


    public class PropertyAdvantageEntry
    {
        public bool IsChecked { get; set; }
        public PropertyBase Advantage { get; set; }
    }

    public class PropertySelectBase
    {
        public int Id { get; set; }
        /// <summary>
        /// 93: Nhà bán, 94: Nhà Thuê
        /// </summary>
        public short AdsType { get; set; }
        public string Name { get; set; }
        public double? FromValue { get; set; }
        public double? ToValue { get; set; }
    }


    [ProtoContract]
    public class PropertyIndex2
    {
        [ProtoMember(1)]
        public int TotalCount { get; set; }
        [ProtoMember(2)]
        public List<PropertyItem2> ListProperties { get; set; }

    }
    public class PropertyItem2
    {
        [ProtoMember(1)]
        public int Id { get; set; }

        [ProtoMember(2)]
        public string Content { get; set; }

        [ProtoMember(3)]
        public string Title { get; set; }

        [ProtoMember(4)]
        public string Note { get; set; }

        [ProtoMember(5)]
        public double Floors { get; set; }

        [ProtoMember(6)]
        public int Bedrooms { get; set; }

        [ProtoMember(7)]
        public int Bathrooms { get; set; }

        [ProtoMember(8)]
        public string AddressNumber { get; set; }

        [ProtoMember(9)]
        public string AddressCorner { get; set; }

        [ProtoMember(10)]
        public DateTime CreatedDate { get; set; }

        [ProtoMember(11)]
        public DateTime LastUpdatedDate { get; set; }

        [ProtoMember(12)]
        public DateTime? AdsExpirationDate { get; set; }

        [ProtoMember(13)]
        public double AlleyWidth { get; set; }

        [ProtoMember(14)]
        public double PriceProposed { get; set; }

        [ProtoMember(15)]
        public double PriceProposedInVND { get; set; }

        [ProtoMember(16)]
        public double PriceEstimatedInVND { get; set; }

        [ProtoMember(17)]
        public double AreaUsable { get; set; }

        [ProtoMember(18)]
        public double AreaTotalWidth { get; set; }

        [ProtoMember(19)]
        public double AreaTotalLength { get; set; }

        [ProtoMember(20)]
        public double AreaTotal { get; set; }

        [ProtoMember(21)]
        public int ApartmentId { get; set; }

        [ProtoMember(22)]
        public string ImageDefaultAvatar { get; set; }
    }
}
