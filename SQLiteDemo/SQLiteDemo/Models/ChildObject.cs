using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteDemo.Models
{
    public class ChildObject
    {
        public ChildObject()
        {
            DbId = Guid.NewGuid().ToString();
        }
        public string Name { get; set; }

        [PrimaryKey]
        public string DbId { get; set; }

        [ForeignKey(typeof(ParentObject))]
        public string ParentDbId { get; set; }
    }
}
