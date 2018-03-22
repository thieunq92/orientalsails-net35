using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
    public partial class SearchOldBooking : SailsAdminBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string searchText = Request.QueryString["id"];
                string dateTime = Request.QueryString["datetime"];
                string[] customerIds = searchText.Split(',');
                var sw = new StringWriter();
                var writer = new JsonTextWriter(sw);
                writer.WriteStartArray();  
                foreach (var customerId in customerIds)
                {
                    writer.WriteStartObject();
                    writer.WritePropertyName("CustomerId");
                    writer.WriteValue(customerId);
                    var customer =
                        Module.GetObject<Customer>(Convert.ToInt32(customerId));
                    writer.WritePropertyName("BookingIds");
                    writer.WriteStartArray();
                    foreach (BookingRoom bookingRoom in customer.BookingRooms)
                    {    
                        if (bookingRoom.Book.StartDate < DateTime.ParseExact(dateTime, "dd/MM/yyyy", CultureInfo.InvariantCulture))
                        {      
                            writer.WriteValue(bookingRoom.Book.Id);
                          
                        }
                    }
                    writer.WriteEndArray();
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