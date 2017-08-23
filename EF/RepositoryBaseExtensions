using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using Z.EntityFramework.Plus;

namespace Project.DataAccess
{
    public static class RepositoryBaseExtensions
    {
        public static void Delete<TEntity>(this DbContext context, IList<TEntity> items) where TEntity : class
        {
            foreach (var item in items)
            {
                context.Set<TEntity>().Attach(item);
                context.Set<TEntity>().Remove(item);
            }
            context.SaveChanges();
        }

        public static void Delete<TEntity>(this DbContext context, TEntity item) where TEntity : class
        {
            //RemoveHoldingEntityInContext(context, item);
            context.Set<TEntity>().Attach(item);
            context.Set<TEntity>().Remove(item);
            context.SaveChanges();
        }

        public static void Delete<TEntity>(this DbContext context, Expression<Func<TEntity, bool>> whereClaus) where TEntity : class
        {
            context.Set<TEntity>().Where(whereClaus).Delete();
            context.SaveChanges();
        }

        public static long GetCount<TEntity>(this DbContext context, Expression<Func<TEntity, bool>> whereClaus) where TEntity : class
        {
            return context.Set<TEntity>().Count(whereClaus);
        }

        public static IQueryable<TEntity> GetWhere<TEntity>(this DbContext context, Expression<Func<TEntity, bool>> whereClaus) where TEntity : class
        {
            return context.Set<TEntity>().Where(whereClaus);
        }

        public static TEntity GetFirstOrDefault<TEntity>(this DbContext context, Expression<Func<TEntity, bool>> whereClaus) where TEntity : class
        {
            return context.Set<TEntity>().FirstOrDefault(whereClaus);
        }
        public static TEntity GetFirst<TEntity>(this DbContext context, Expression<Func<TEntity, bool>> whereClaus) where TEntity : class
        {
            return context.Set<TEntity>().First(whereClaus);
        }

        public static void Insert<TEntity>(this DbContext context, IList<TEntity> items) where TEntity : class
        {

            foreach (var item in items)
            {
                context.Set<TEntity>().Add(item);
            }
            context.SaveChanges();
        }
        public static TEntity Insert<TEntity>(this DbContext context, TEntity item) where TEntity : class
        {
            RemoveHoldingEntityInContext(context, item);
            var m = context.Set<TEntity>().Add(item);
            context.SaveChanges();
            return m;
        }

        public static void Update<TEntity>(this DbContext context, IList<TEntity> items) where TEntity : class
        {
            foreach (var item in items)
            {
                if (!context.Set<TEntity>().Local.Contains(item))
                {
                    context.Set<TEntity>().Attach(item);
                }
                context.Entry(item).State = EntityState.Modified;
            }
            context.SaveChanges();
        }

        public static void Update<TEntity>(this DbContext context, TEntity item) where TEntity : class
        {
            //if (!context.Set<TEntity>().Local.Contains(item))
            //{
            //    context.Set<TEntity>().Attach(item);
            //}
            RemoveHoldingEntityInContext(context, item);
            context.Entry(item).State = EntityState.Modified;
            context.SaveChanges();
        }

        /// <summary>
        /// 只更新指定属性
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="context">实体上下文</param>
        /// <param name="whereClaus">条件表达式</param>
        /// <param name="updateClaus">New 表达式 返回如： new Staff() {  }的表达式 可参考 OrginizationDa UpdateStaffInfo方法</param>
        /// <returns></returns>
        public static int Update<TEntity>(this DbContext context, Expression<Func<TEntity, bool>> whereClaus, Expression<Func<TEntity, TEntity>> updateClaus) where TEntity : class
        {
            var m = context.Set<TEntity>().Where(whereClaus).Update(updateClaus);
            context.SaveChanges();
            return m;
        }

        public static void InsetOrUpdate<TEntity>(this DbContext context, TEntity item) where TEntity : class
        {
            if (context.Entry(item).State == EntityState.Detached)
            {
                context.Set<TEntity>().Attach(item);
            }
            if (!context.Set<TEntity>().Local.Contains(item))
            {
                context.Set<TEntity>().Attach(item);
            }
            context.Entry(item).State = EntityState.Modified;
            context.SaveChanges();
        }

        public static void SaveChanges(this DbContext context)
        {
            context.SaveChanges();
        }

        /// <summary>
        /// 用于监测Context中的Entity是否存在，如果存在，将其Detach，防止出现问题。
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns> 
        private static Boolean RemoveHoldingEntityInContext<TEntity>(this DbContext _context, TEntity entity) where TEntity : class
        {
            var objContext = ((IObjectContextAdapter)_context).ObjectContext;
            var objSet = objContext.CreateObjectSet<TEntity>();
            var entityKey = objContext.CreateEntityKey(objSet.EntitySet.Name, entity);

            Object foundEntity;
            var exists = objContext.TryGetObjectByKey(entityKey, out foundEntity);

            if (exists)
            {
                objContext.Detach(foundEntity);
            }

            return (exists);
        }
    }
}
