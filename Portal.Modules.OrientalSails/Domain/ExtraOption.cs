
using System;
using System.Collections;
using System.Web.UI.WebControls;

namespace Portal.Modules.OrientalSails.Domain
{
    public class ExtraOption
    {
        private string name;
        private string description;

        public virtual int Id { get; set; }
        public virtual double Price { get; set; }
        public virtual bool Deleted { get; set; }
        public virtual bool IsIncluded { get; set; }
        public virtual ServiceTarget Target { get; set; }

        public virtual string Name
        {
            get { return name; }
            set
            {
                if (value != null && value.Length > 250)
                    throw new ArgumentOutOfRangeException("Invalid value for Name", value, value.ToString());
                name = value;
            }
        }


        public virtual string Description
        {
            get { return description; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for Description", value, value.ToString());
                description = value;
            }
        }


    }
}
