using System;
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
    public class AnswerOption
    {
        public virtual int Id { get; set; }
        public virtual Question Question { get; set; }
        public virtual AnswerSheet AnswerSheet { get; set; }
        public virtual int Option { get; set; }
    }
}
