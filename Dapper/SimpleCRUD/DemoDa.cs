using System.Collections.Generic;
using System.Linq;
using Dapper;

namespace Project.DataAccess
{
    public class DemoDa : RepositoryBase
    {
        #region 实体
        [Table("Users")]
        public partial class User
        {
            [Key]
            public int UserId { get; set; }
            [Column("strFirstName")]
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int Age { get; set; }
        }

        public partial class User
        {
            [Editable(false)] // Additional properties not in database
            [IgnoreSelect] // Excludes the property from selects
            [IgnoreInsert] // Excludes the property from inserts
            [IgnoreUpdate] // Excludes the property from updates
            [NotMapped] // Excludes the property from all operations
            public string FullName => $"{FirstName} {LastName}";
        }
        #endregion
        
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="user"></param>
        public void Insert(User user)
        {
            Db(p => p.Insert(user));
        }
        
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="user"></param>
        public void Update(User user)
        {
            Db(p => p.Update(user));
        }
        
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="user"></param>
        public void Delete(User user)
        {
            Db(p => p.Delete(user));
        }

        /// <summary>
        /// 删除
        /// </summary>
        public void DeleteList(int age)
        {
            Db(p => p.DeleteList<User>(new { Age = age }));
        }
        
        /// <summary>
        /// 删除
        /// </summary>
        public void DeleteList2(int age)
        {
            Db(p => p.DeleteList<User>("Where age > @Age", new { Age = age }));
        }
        
        #region SELECT
        /// <summary>
        /// 获取记录数
        /// </summary>
        /// <returns></returns>
        public int GetRecord()
        {
            return Db(p => p.RecordCount<User>("Where age > @Age", new { Age = 20 }));
        }

        /// <summary>
        /// 获取映射到强类型对象的单个记录
        /// </summary>
        public User Get()
        {
            //sql: Select UserId, strFirstName as FirstName, LastName, Age from [Users] where UserId = @UserID
            return Db(p => p.Get<User>(1));
        }

        /// <summary>
        /// 执行查询并将结果映射到强类型列表
        /// </summary>
        /// <returns></returns>
        public List<User> GetList()
        {
            //sql: Select * from [User]
            return Db(p => p.GetList<User>().ToList());
        }

        /// <summary>
        /// 执行一个查询条件，并将结果映射到强类型列表
        /// </summary>
        /// <returns></returns>
        public List<User> GetListWhere()
        {
            //sql: Select * from [User] where Age = @Age
            return Db(p => p.GetList<User>(new { Age = 10 }).ToList());
        }

        /// <summary>
        /// 使用where子句执行查询，并将结果映射到强类型列表
        /// </summary>
        /// <returns></returns>
        public List<User> GetListWhere2()
        {
            //sql: Select * from [User] where age = 10 or Name like '%Smith%'
            return Db(p => p.GetList<User>("where age = @Age or Name like @Name", new { Age = 10, Name = "%Smith%" }).ToList());
        }

        /// <summary>
        /// 使用where子句执行查询，并将结果映射到具有Paging的强类型列表
        /// </summary>
        /// <returns></returns>
        public List<User> GetListPaged()
        {
            //sql: SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY Name desc) AS PagedNumber, Id, Name, Age FROM [User] where age = 10) AS u WHERE PagedNUMBER BETWEEN ((1 - 1) * 10 + 1) AND (1 * 10)
            return Db(p => p.GetListPaged<User>(1, 10, "where age = @Age", "Name desc", new { Age = 10 }).ToList());
        }

        #endregion
    }
}
