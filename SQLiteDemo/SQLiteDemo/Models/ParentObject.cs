using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteDemo.Models
{
    public class ParentObject
    {
        public ParentObject()
        {
            DbId = Guid.NewGuid().ToString();
        }
        public string Name { get; set; }

        [PrimaryKey]
        public string DbId { get; set; }

        [OneToMany (CascadeOperations = CascadeOperation.All)]
        public List<ChildObject> Children { get; set; } = new List<ChildObject>();
    }
}
