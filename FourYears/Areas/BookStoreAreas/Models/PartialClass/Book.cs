using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FourYears.Areas.BookStoreAreas.Models.PartialClass
{
    [MetadataType(typeof(BookMetaData))]

    public partial class Book
    {
        public class BookMetaData
        {
            [DisplayName("商品編號")]
            public int BookID { get; set; }
            [DisplayName("商品名稱")]
            [Required(ErrorMessage = "請填寫商品名稱")]
            public string BookName { get; set; }
            public int SubCategoryID { get; set; }
            public int AuthorID { get; set; }
            public int PublisherID { get; set; }
            [DisplayName("出版日期")]
            [DataType(DataType.Date)]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
            public Nullable<System.DateTime> PublishDate { get; set; }
            [DisplayName("上架日期")]
            [DataType(DataType.Date)]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
            public Nullable<System.DateTime> OnShelfDate { get; set; }
            [DisplayName("語言")]
            public string Language { get; set; }
            [DisplayName("原價")]
            [DataType(DataType.Currency)]
            [DisplayFormat(DataFormatString = "{0:C0}")]
            public decimal ListPrice { get; set; }
            [DisplayName("售價")]
            [Required(ErrorMessage = "請填寫售價")]
            [DataType(DataType.Currency)]
            [DisplayFormat(DataFormatString = "{0:C0}")]
            public decimal SalePrice { get; set; }
            [DisplayName("內容簡介")]
            public string BookDescription { get; set; }
            [DisplayName("作者簡介")]
            [DataType(DataType.MultilineText)]
            public string AuthorDescription { get; set; }
            [DisplayName("其他資訊")]
            [DataType(DataType.MultilineText)]
            public string OtherImformation { get; set; }
            [DisplayName("圖片1")]
            public string Image1 { get; set; }
            public byte[] BytesImage1 { get; set; }
            [DisplayName("圖片2")]
            public string Image2 { get; set; }
            public byte[] BytesImage2 { get; set; }
            [DisplayName("圖片3")]
            public string Image3 { get; set; }
            public byte[] BytesImage3 { get; set; }
        }
    }
}