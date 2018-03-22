using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


using System.IO;
using System.Net;
using System.Text;

namespace Portal.Modules.OrientalSails.Web.Admin.BO
{
    public partial class vpc_queryDR : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void SubButL_Click(object sender, EventArgs e)
        {
            string postData = "";
            string seperator = "";
            string resQS = "";
            int paras = 7;
            string vpcURL = "https://mtf.onepay.vn/vpcpay/Vpcdps.op";


            string[,] MyArray =
			{
			{"vpc_AccessCode","6BEB2546"},
			{"vpc_Command","queryDR"},           
			{"vpc_MerchTxnRef",vpc_MerchTxnRef.Text},			
            {"vpc_Merchant","TESTONEPAY"},						
            {"vpc_Password","op01"},
			{"vpc_User","op123456"},
			{"vpc_Version", "1"}							
			};
            for (int i = 0; i < paras; i++)
            {
                postData = postData + seperator + Server.UrlEncode(MyArray[i, 0]) + "=" + Server.UrlEncode(MyArray[i, 1]);
                seperator = "&";
            }

            resQS = doPost(vpcURL, postData);
            Response.Write(resQS);
        }

        /**
  * This method is for performing a Form POST operation from input data parameters.
  *
  * @param vpc_Host  - is a String containing the vpc URL
  * @param data      - is a String containing the post data key value pairs
  * @param useProxy  - is a boolean indicating if a Proxy Server is involved in the transfer
  * @param proxyHost - is a String containing the IP address of the Proxy to send the data to
  * @param proxyPort - is an integer containing the port number of the Proxy socket listener
  * @return          - is body data of the POST data response    
  */
        public static string doPost(string vpc_Host, string postData)
        {
            string page = "Response:";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(vpc_Host);
            request.Method = "POST";
            request.UserAgent = "HTTP Client";
            request.ContentType = "application/x-www-form-urlencoded";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string responseFromServer = reader.ReadToEnd();
            page = page + responseFromServer;
            reader.Close();
            response.Close();
            return page;
        }
    }
}
