using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net;
using CipherPark.TriggerOrange.Core;
using CipherPark.TriggerOrange.Web.Models;

namespace CipherPark.TriggerOrange.Web.CoreServices
{
    public static class WebApiServices
    {
        public static byte[] DownloadImage(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            // Check that the remote file was found. The ContentType
            // check is performed since a request for a non-existent
            // image file might be redirected to a 404-page, which would
            // yield the StatusCode "OK", even though the image was not
            // found.
            if ((response.StatusCode == HttpStatusCode.OK ||
                response.StatusCode == HttpStatusCode.Moved ||
                response.StatusCode == HttpStatusCode.Redirect) &&
                response.ContentType.StartsWith("image", StringComparison.OrdinalIgnoreCase))
            {
                // if the remote file was found, download oit
                using (Stream inputStream = response.GetResponseStream())
                using (Stream outputStream = new MemoryStream())
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead = 0;
                    int totalBytesRead = 0;
                    do
                    {
                        bytesRead = inputStream.Read(buffer, 0, buffer.Length);
                        outputStream.Write(buffer, 0, bytesRead);
                        totalBytesRead += bytesRead;
                    } while (bytesRead != 0);
                    outputStream.Seek(0, SeekOrigin.Begin);
                    byte[] result = new byte[totalBytesRead];
                    outputStream.Read(result, 0, totalBytesRead);
                    return result;
                }
            }
            else
                return null;
        }
    }
}

