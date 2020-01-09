using System;
using AspNetCore31Lab.Protocol.Logic;
using AspNetCore31Lab.Protocol.Model.Data;
using AspNetCore31Lab.Protocol.Physical;
using Chef.Extensions.DbAccess;
using Chef.Extensions.DbAccess.Fluent;

namespace AspNetCore31Lab.Logic
{
    public class DataService : IDataService
    {
        private readonly IDataAccess<Member> membDataAccess;
        private readonly IConfig config;

        public DataService(IDataAccess<Member> membDataAccess)
        {
            this.membDataAccess = membDataAccess;
        }

        public DataService(IDataAccess<Member> membDataAccess, IConfig config)
        {
            this.membDataAccess = membDataAccess;
            this.config = config;
        }

        public string GetName()
        {
            return this.membDataAccess.Where(x => x.Id == 1).QueryOne().Name;
        }
    }
}