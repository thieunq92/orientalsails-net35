using System;
using System.IO;
using System.Net;
using System.Text;

namespace CMS.Web.Util
{
    public class RemoteFileHelper
    {
        private const int MAX_SIZE = 65536; // max read buffer size conserves memory
        private const int MIN_SIZE = 8192; // min size prevents numerous small reads        

        /// <summary>
        /// Hàm kiểm tra xem có request được ảnh hay không (Status code hợp lệ là 200 và 302)
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public bool CheckFileExists(string url)
        {
            try
            {
                HttpWebRequest extRequest = (HttpWebRequest) WebRequest.Create(url);
                HttpWebResponse extResponse = (HttpWebResponse) extRequest.GetResponse();
                if (extResponse.StatusCode == HttpStatusCode.OK || extResponse.StatusCode == HttpStatusCode.Found)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Lấy toàn bộ nội dung của URL vào một chuỗi string
        /// </summary>
        /// <param name="url">Đường dẫn tới trang cần lấy nội dung</param>
        /// <returns></returns>
        public string GetPageContent(string url)
        {
            StringBuilder sb;
            try
            {
                HttpWebRequest extRequest = (HttpWebRequest) WebRequest.Create(url);
                HttpWebResponse extResponse = (HttpWebResponse) extRequest.GetResponse();
                Stream responseStream = extResponse.GetResponseStream();

                // Content-Length header is not trustable, but makes a good hint.
                // Responses longer than int size will throw an exception here!
                int length = (int) extResponse.ContentLength;


                // Use Content-Length if between bufSizeMax and bufSizeMin
                int bufSize = MIN_SIZE;
                if (length > bufSize)
                    bufSize = length > MAX_SIZE ? MAX_SIZE : length;


                // Allocate buffer and StringBuilder for reading response
                byte[] buf = new byte[bufSize];
                sb = new StringBuilder(bufSize);

                // Read response stream until end
                while ((length = responseStream.Read(buf, 0, buf.Length)) != 0)
                    sb.Append(Encoding.UTF8.GetString(buf, 0, length));

                return sb.ToString();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}