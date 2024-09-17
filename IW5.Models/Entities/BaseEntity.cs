using IW5.DAL.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IW5.Models.Entities
{
    public abstract record BaseEntity : IEntity
    {
        public Guid Id { get; set; }
    }
}
