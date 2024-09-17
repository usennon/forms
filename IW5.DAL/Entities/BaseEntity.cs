using IW5.DAL.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IW5.API.DAL.Entities
{
    public abstract record BaseEntity : IEntity
    {
        public Guid Id { get; set; }
    }
}
