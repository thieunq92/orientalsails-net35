using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using NHibernate.Criterion;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class SearchCustomer : SailsAdminBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string searchText = Request.QueryString["term"];
                var customerList =
                    Module.GetObject<Customer>(Expression.Like("Fullname", searchText, MatchMode.Anywhere), 10, 0,
                        new Order("Id", true));
                var sw = new StringWriter();
                var writer = new JsonTextWriter(sw);
                writer.WriteStartArray();
                foreach (var customer in customerList)
                {
                    writer.WriteStartObject();
                    writer.WritePropertyName("Id");
                    writer.WriteValue(customer.Id);
                    writer.WritePropertyName("Fullname");
                    writer.WriteValue(customer.Fullname);
                    writer.WritePropertyName("HasGenderValue");
                    writer.WriteValue(customer.IsMale.HasValue);
                    if (customer.IsMale.HasValue)
                    {
                        writer.WritePropertyName("IsMale");
                        writer.WriteValue(customer.IsMale.Value);
                    }
                    writer.WritePropertyName("HasBirthdayValue");
                    writer.WriteValue(customer.Birthday.HasValue);
                    if (customer.Birthday.HasValue)
                    {
                        writer.WritePropertyName("Birthday");
                        writer.WriteValue(customer.Birthday.Value.ToString("dd/MM/yyyy"));
                    }
                    if (customer.Nationality != null && !customer.Nationality.Name.Equals("KHONG RO",StringComparison.OrdinalIgnoreCase))
                    {
                        writer.WritePropertyName("HasNationality");
                        writer.WriteValue(true);

                        writer.WritePropertyName("Nationality");
                        writer.WriteValue(customer.Nationality.Name);

                        writer.WritePropertyName("NationId");
                        writer.WriteValue(customer.Nationality.Id);
                    }
                    else
                    {
                        writer.WritePropertyName("HasNationality");
                        writer.WriteValue(false);
                    }
                    writer.WritePropertyName("VisaNo");
                    writer.WriteValue(customer.VisaNo);
                    writer.WritePropertyName("Passport");
                    writer.WriteValue(customer.Passport);
                    writer.WritePropertyName("HasVisaExpiredValue");
                    writer.WriteValue(customer.VisaExpired.HasValue);
                    if (customer.VisaExpired.HasValue)
                    {
                        writer.WritePropertyName("VisaExpired");
                        writer.WriteValue(customer.VisaExpired.Value.ToString("dd/MM/yyyy"));
                    }
                    writer.WritePropertyName("NguyenQuan");
                    writer.WriteValue(customer.NguyenQuan);
                    writer.WritePropertyName("IsVietKieu");
                    writer.WriteValue(customer.IsVietKieu);
                    writer.WriteEndObject();
                }
                writer.WriteEndArray();
                string jsonString = sw.ToString();
                Response.Clear();
                Response.ContentType = "application/json; charset=utf-8";
                Response.Write(jsonString);
                Response.End();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}