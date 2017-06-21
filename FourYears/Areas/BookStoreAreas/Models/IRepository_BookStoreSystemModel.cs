using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FourYears.Areas.BookStoreAreas.Models
{
    interface IRepository_BookStoreSystemModel<T>
    {
        IEnumerable<T> GetAll();  //讀取全部資料
        T GetByID(int id);  //根據參數:資料表主鍵ID 讀取資料
        void Create(T _Entity);  //新增實體資料
        void Delete(T _Entity);  //刪除實體資料
        void Update(T _Entity); //修改實體資料屬性
    }
}