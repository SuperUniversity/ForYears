using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FourYears.Areas.JobArea.Models
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private superuniversityEntities1 db = new superuniversityEntities1();
        private DbSet<T> DbSet = null;

        public Repository()
        {
            DbSet = db.Set<T>();
        }
        public void Create(T _entity)
        {
            DbSet.Add(_entity);
            //try
            //{
            db.SaveChanges();
            //}
            //catch(Exception ex)
            //{
            //    throw;
            //}

        }

        public void Delete(T _entity)
        {
            DbSet.Remove(_entity);
            db.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return DbSet.ToList();
        }

        public T GetById(int id)
        {
            return DbSet.Find(id);
        }

        public void Update(T _entity)
        {
            db.Entry(_entity).State = System.Data.Entity.EntityState.Modified;
            //try
            //{
            db.SaveChanges();
            //}
            //catch (Exception ex)
            //{
            //    string t = ex.Message;
            //}
        }
    }
}