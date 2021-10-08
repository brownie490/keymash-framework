using System.IO;
using System.Net;


namespace Automation
{
    class API
    {

        const bool AcceptCerts = true;


        /// <summary>
        ///     Accept all Security Certificates
        /// </summary>
        private static void AcceptAllSecurityCertificates()
        {

            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

        }


        /// <summary>
        ///     Submits a GET request to the given URL
        /// </summary>
        /// <param name="URL">The URL of the end point including any parameters</param>
        /// <returns>The full response as a string.</returns>
        public static string Get(string URL)
        {

            // Accept all the security certificates
            if (AcceptCerts) {

                AcceptAllSecurityCertificates();

            }

            // Attempt to post the request
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                request.Headers.Add("Authorization: ");

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {

                    return reader.ReadToEnd();

                }

            }


            // Catch any errors and return then back
            catch (WebException Error)
            {

                HttpWebResponse httpResponse = (HttpWebResponse)Error.Response;


                using (var reader = new StreamReader(httpResponse.GetResponseStream()))
                {

                    string ResponseData = reader.ReadToEnd();

                    if (!string.IsNullOrEmpty(ResponseData))
                    {

                        return ResponseData;


                    }
                    else
                    {

                        int StatusCode = (int)httpResponse.StatusCode;
                        return StatusCode.ToString();

                    }

                }

            }

        }


        /// <summary>
        ///     Submits a POST request to the given URL with the given body
        /// </summary>
        /// <param name="URL">The URL of the end point</param>
        /// <param name="RequestBody">The JSON to post as a string</param>
        /// <param name="ContentType">Optional - defaults to application/json</param>
        /// <returns>The full response as a string.</returns>
        public static string Post(string URL, string RequestBody, string ContentType = "application/json")
        {

            // Accept all the security certificates
            if (AcceptCerts)
            {

                AcceptAllSecurityCertificates();

            }

            // Attempt to post the request
            try
            {

                var request = (HttpWebRequest)WebRequest.Create(URL);
                request.ContentType = ContentType;
                request.Method = "POST";

                request.Headers.Add("Authorization: ");

                using (var reader = new StreamWriter(request.GetRequestStream()))
                {
                    reader.Write(RequestBody);
                    reader.Flush();
                    reader.Close();
                }

                var httpResponse = (HttpWebResponse)request.GetResponse();
                using (var reader = new StreamReader(httpResponse.GetResponseStream()))
                {

                    return reader.ReadToEnd();

                }

            }

            // Catch any errors and return then back
            catch (WebException Error)
            {

                HttpWebResponse httpResponse = (HttpWebResponse)Error.Response;


                using (var reader = new StreamReader(httpResponse.GetResponseStream()))
                {

                    string ResponseData = reader.ReadToEnd();

                    if (!string.IsNullOrEmpty(ResponseData))
                    {
                        
                        return ResponseData;


                    } else {

                        int StatusCode = (int)httpResponse.StatusCode;
                        return StatusCode.ToString();

                    }

                }

            }

        }


        /// <summary>
        ///     Submits a PUT request to the given URL with the given body
        /// </summary>
        /// <param name="URL">The URL of the end point</param>
        /// <param name="RequestBody">The JSON to post as a string</param>
        /// <param name="ContentType">Optional - defaults to application/json</param>
        /// <returns>The full response as a string.</returns>
        public static string Put(string URL, string RequestBody, string ContentType = "application/json")
        {

            // Accept all the security certificates
            if (AcceptCerts)
            {

                AcceptAllSecurityCertificates();

            }

            // Attempt to post the request
            try
            {

                var request = (HttpWebRequest)WebRequest.Create(URL);
                request.ContentType = "text/json";
                request.Method = "PUT";

                request.Headers.Add("Authorization: ");

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {

                    streamWriter.Write(RequestBody);

                }

                var httpResponse = (HttpWebResponse)request.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {

                    int StatusCode = (int)httpResponse.StatusCode;
                    return StatusCode.ToString();

                    // return streamReader.ReadToEnd();

                }

            }

            // Catch any errors and return then back
            catch (WebException Error)
            {

                HttpWebResponse httpResponse = (HttpWebResponse)Error.Response;


                using (var reader = new StreamReader(httpResponse.GetResponseStream()))
                {

                    string ResponseData = reader.ReadToEnd();

                    if (!string.IsNullOrEmpty(ResponseData))
                    {

                        return ResponseData;


                    }
                    else
                    {

                        int StatusCode = (int)httpResponse.StatusCode;
                        return StatusCode.ToString();

                    }

                }

            }

        }

    }

}
