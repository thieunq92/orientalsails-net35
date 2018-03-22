using System;
using System.Collections;
using CMS.Core.Domain;

namespace Portal.Modules.OrientalSails.Domain
{
    public class Activity
    {
        public virtual int Id { get; set; }
        public virtual User User { get; set; }
        public virtual DateTime Time { get; set; }
        public virtual string Action { get; set; }
        public virtual string Url { get; set; }
        public virtual string Params { get; set; }
        public virtual ImportantLevel Level { get; set; }
        public virtual string Note { get; set; }
        public virtual string ObjectType { get; set; }
        public virtual int ObjectId { get; set; }
        public virtual DateTime DateMeeting { get; set; }
        public virtual DateTime? UpdateTime { get; set; }

    }

    public enum ImportantLevel
    {
        Info,
        Regular,
        Important
    }

}
