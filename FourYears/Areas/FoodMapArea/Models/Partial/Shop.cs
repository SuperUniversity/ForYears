using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FourYears.Areas.FoodMapArea.Models
{
    [MetadataType(typeof(ShopMetadata))]
    public partial class Shop
    {
        public class ShopMetadata
        {
            [DisplayName("餐廳編號")]
            public int ShopID { get; set; }
            [DisplayName("地址")]
            public string Address { get; set; }
            [DisplayName("電話")]
            public string Phone { get; set; }
            [DisplayName("圖片1")]
            public string Image1 { get; set; }
            public byte[] BytesImage1 { get; set; }
            [DisplayName("介紹")]
            [DataType(DataType.MultilineText)]
            public string Description { get; set; }
            [DisplayName("平均消費")]
            public string Cost { get; set; }
            [DisplayName("營業時間")]
            public string BusinessTime { get; set; }
            [DisplayName("餐廳名稱")]
            public string ShopName { get; set; }
            [DisplayName("分類編號")]
            public Nullable<int> FoodCategoryID { get; set; }
            [DisplayName("圖片2")]
            public string Image2 { get; set; }
            public byte[] BytesImage2 { get; set; }
            [DisplayName("圖片3")]
            public string Image3 { get; set; }
            public byte[] BytesImage3 { get; set; }
            [DisplayName("粉絲專頁")]
            public string ShopLink { get; set; }
            [DisplayName("縣市編號")]
            public Nullable<int> CityID { get; set; }
            [DisplayName("學校編號")]
            public Nullable<int> SchoolID { get; set; }
        }
    }
}