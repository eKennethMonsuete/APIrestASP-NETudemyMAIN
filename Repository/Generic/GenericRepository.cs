using APIrestASP_NETudemy.Model;
using APIrestASP_NETudemy.Model.Base;
using Microsoft.EntityFrameworkCore;
using RestASPNETErudio.Business.Implementations;
using RestASPNETErudio.Model.Context;

namespace APIrestASP_NETudemy.Repository.Generic
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {


        private MySQLContext _context;

        private DbSet<T> dbSet;

        // o Repository ficou apenas para a persistencia 
        public GenericRepository(MySQLContext context)
        {
            _context = context;
            dbSet = _context.Set<T>();
        }
        

        

        public List<T> FindAll()
        {
            return dbSet.ToList();
        }

        public T FindByID(long id)
        {
            return dbSet.SingleOrDefault(p => p.Id.Equals(id));
        }
        
        public T Create(T item)
        {
            try
            {
                dbSet.Add(item);
                _context.SaveChanges(); 
                return item;

            }
            catch (Exception)
            {

                throw;
            }
           
        }

        public T Update(T item)
        {
            var result = dbSet.SingleOrDefault(p => p.Id.Equals(item.Id));
            if (result != null)
            {  
                    try
                {
                    _context.Entry(result).CurrentValues.SetValues(item);
                    _context.SaveChanges();
                    return item;

                }
                    catch (Exception)
                {

                    throw;
                }

            } else{return null;}
                
            
            

              

        }

        public void Delete(long id)
        {
            var result = dbSet.SingleOrDefault(p => p.Id.Equals(id));
            if (result != null)
            {    
                    try
                {
                    dbSet.Remove(result);
                    _context.SaveChanges();
                

                }
                catch (Exception)
                {

                    throw;
                }

            }
            

        }

        public bool Exists(long id)
        {
            return dbSet.Any(p => p.Id.Equals(id));

        }
    }


}
