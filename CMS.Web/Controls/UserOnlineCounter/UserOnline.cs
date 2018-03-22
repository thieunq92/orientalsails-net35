using System.Web;
using System.Web.UI.WebControls;

namespace CMS.Web.Controls.UserOnlineCounter
{
    public class UserOnline: WebControl
    {
        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            //Attributes.Add("cellSpacing", "1");
            //Attributes.Add("cellPadding", "1");
            //Attributes.Add("border", "1");
            //Attributes.Add("borderWidth", BorderWidth.ToString());
            //Attributes.Add("borderColor", formatColour(BorderColor));

            base.RenderBeginTag(writer);
            writer.Write(HttpContext.Current.Application["ActiveUsers"].ToString());
            base.RenderEndTag(writer);
        }
    }
}
