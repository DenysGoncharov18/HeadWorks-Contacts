using System;
using SQLite;

namespace Contacts.Models
{
    public class ProfileModel: IEntityBase
    {
        [PrimaryKey, AutoIncrement ]
        public int Id { get; set; }
        public string FisrtName { get; set; }
        public string LastName { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
