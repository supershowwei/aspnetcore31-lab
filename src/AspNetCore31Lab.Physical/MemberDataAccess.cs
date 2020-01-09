using AspNetCore31Lab.Protocol.Model.Data;
using Chef.Extensions.DbAccess;

namespace AspNetCore31Lab.Physical
{
    public class MemberDataAccess : SqlServerDataAccess<Member>
    {
        public MemberDataAccess()
            : base(string.Empty)
        {
        }
    }
}