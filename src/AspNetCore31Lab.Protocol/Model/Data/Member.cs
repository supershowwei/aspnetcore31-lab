using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCore31Lab.Protocol.Model.Data
{
    public class Member
    {
        [Column("MemberNo")]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}