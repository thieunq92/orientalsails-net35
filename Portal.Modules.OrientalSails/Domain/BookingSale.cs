using System;
using CMS.Core.Domain;

namespace Portal.Modules.OrientalSails.Domain
{
    public class BookingSale
    {
        public virtual int Id { get; set; }
        public virtual User Sale { get; set; }
    }
}
