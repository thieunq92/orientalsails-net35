using System;
using System.Collections;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CMS.Core.Domain;

namespace Portal.Modules.OrientalSails.Domain
{
    public class DayNote
    {
        public DayNote() { }

        public DayNote(DateTime date)
        {
            Date = date;
        }

        public virtual int Id { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual string Note { get; set; }
        public virtual Cruise Cruise { get; set; }
    }
}
