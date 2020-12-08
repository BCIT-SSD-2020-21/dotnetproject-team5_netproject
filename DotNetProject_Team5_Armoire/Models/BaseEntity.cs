using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetProject_Team5_Armoire.Models
{
    public abstract class BaseEntity
    {
        public virtual int Id { get; protected set; }
    }
}
