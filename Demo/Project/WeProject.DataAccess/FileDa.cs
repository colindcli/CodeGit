using System;
using Dapper;
using WeProject.Entity;

namespace WeProject.DataAccess
{
    public class FileDa : RepositoryBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public void Upload(Attachment model)
        {
            Db(p => p.Insert<Guid>(model));
        }
    }
}
