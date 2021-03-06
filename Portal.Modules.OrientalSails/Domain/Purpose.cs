using System;
using CMS.Core.Domain;

namespace Portal.Modules.OrientalSails.Domain
{
    public class Purpose
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Code { get; set; }
        public virtual bool Deleted { get; set; }
    }
}