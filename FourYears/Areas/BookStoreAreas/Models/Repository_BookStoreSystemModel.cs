using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FourYears.Areas.BookStoreAreas.Models
{
   
        public class Repository_BookStoreSystemModel<T> : IRepository_BookStoreSystemModel<T> where T : class
        {
            private superuniversityEntities4 DB = null;
            private DbSet<T> DBSet = null;

            public Repository_BookStoreSystemModel()  //方法同名的建構子
            {
                DB = new superuniversityEntities4();
                DBSet = DB.Set<T>();
            }
            public IEnumerable<T> GetAll()  //讀取全部資料
            {
                return DBSet.ToList();
            }

            public T GetByID(int id) //根據參數:資料表主鍵ID 讀取資料
            {
                return DBSet.Find(id);
            }

            public void Create(T _Entity) //新增實體資料
            {
                DBSet.Add(_Entity);
                DB.SaveChanges();
            }

            public void Delete(T _Entity)  //刪除實體資料
            {
                DBSet.Remove(_Entity);   // 可改寫 DB.Entry(_Entity).State = EntityState.Deleted;
                DB.SaveChanges();
            }

            public void Update(T _Entity)  //修改實體資料屬性
            {
                DB.Entry(_Entity).State = EntityState.Modified;
                DB.SaveChanges();
            }
        }
    }
