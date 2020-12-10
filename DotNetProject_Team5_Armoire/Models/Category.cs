using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetProject_Team5_Armoire.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<Clothing> TypeClothings { get; set; }
        public Category (string name)
        {
            Name = name;
        }
    }
}
