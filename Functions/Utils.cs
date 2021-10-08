using System;
using System.Text;
using System.Linq;
using Newtonsoft.Json;
using HtmlAgilityPack;
using System.Threading;
using Newtonsoft.Json.Linq;
using System.Globalization;


namespace Automation
{
    class Utils
    {

        private static readonly Random random = new();


        /// <summary>
        ///     Takes the given email address and adds a random value before the @
        /// </summary>
        /// <param name="Email">The Email to convert.
        ///     For Example: firstname.lastname@email.com
        /// </param>
        /// <returns>The Email after it's been randomized. For Example: "firstname.lastname+123456@email.com"</returns>
        public static string RandomizeEmail(string Email)
        {

            Random rnd = new Random();
            string RandomString = null;

            // Generate a unique fileName
            for (int i = 0; i < 10; i++)
            {

                RandomString += rnd.Next(1, 9).ToString();

            }

            // Return the email address with the random string
            return Email.Replace("@", "+" + RandomString + "@" );

        }


        /// <summary>
        ///     Returns a random string with the given number length
        /// </summary>
        /// <param name="Length">The length of the string to return
        ///     For Example: 5 would result is fiK2Q
        /// </param>
        /// <returns>A Random character string of the given length</returns>
        public static string RandomString(int Length)
        {

            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, Length)
              .Select(s => s[random.Next(s.Length)]).ToArray());

        }


        /// <summary>
        ///     Converts the given string to Title Case
        /// </summary>
        /// <param name="Text">The string to convert.
        ///     For Example: "She sells sea shells"
        /// </param>
        /// <returns>The string in Title Case. For Example: "She Sells Sea Shells"</returns>
        public static string ToTitleCase(string Text)
        {

            if (!string.IsNullOrEmpty(Text) && !string.IsNullOrWhiteSpace(Text))
            {

                return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(Text.ToLower());

            }
            else
            {

                return Text;

            }

        }


        /// <summary>
        ///     Checks to see if the given Json string is valid or not
        /// </summary>
        /// <param name="Offset">The Json string to check.</param>
        /// <returns>true or false if the Json is valid.</returns>
        public static bool IsValidJson(string Json)
        {
            if (string.IsNullOrWhiteSpace(Json))
            {

                return false;

            }

            Json = Json.Trim();
            if ((Json.StartsWith("{") && Json.EndsWith("}")) || //For object
                (Json.StartsWith("[") && Json.EndsWith("]"))) //For array
            { 
                try
                {
                    var obj = JToken.Parse(Json);
                    return true;
                }

                catch (JsonReaderException jex)
                {
                    //Exception in parsing json
                    Console.WriteLine(jex.Message);
                    return false;
                }

                catch (Exception error) //some other exception
                {
                    Console.WriteLine(error.ToString());
                    return false;
                }

            }
            else
            {

                return false;

            }

        }


        /// <summary>
        ///     Checks to see if the given HTML is valid or not and returns true or false
        /// </summary>
        /// <param name="HTML">The HTML to check.</param>
        /// <returns>true or false if the HTML is valid or not</returns>
        public static bool IsHTMLValid(string HTML)
        {

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(HTML);

            if (doc.ParseErrors.Any())
            {

                return false;

            } else
            {

                return true;

            }

        }


        /// <summary>
        ///     Checks to see if the given URL is valid or not and returns true or false
        /// </summary>
        /// <param name="URL">The URL to check.</param>
        /// <returns>true or false if the URL is valid or not</returns>
        public static bool IsValidURL(string URL)
        {

            Uri uriResult;
            bool result = Uri.TryCreate(URL, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            return result;

        }


        /// <summary>
        ///     Gets the current date/time and can be passed a +- Offset and custom date format.
        /// </summary>
        /// <param name="Offset">Optional. Can be null or blank and the current datetime will be returned. Or an offset can be passed.
        ///     For Example: 
        ///     "+1d" - Today's date plus one day (Tomorrow)
        ///     "-1d" - Today's date minus one day (Yesterday)
        /// </param>
        /// <param name="Format">Optional. If nothing is specified, the value in the config file will be used. Otherwise the custome format will be used
        ///     For Example:
        ///     "dddd, dd MMMM yyyy HH:mm:ss" - Friday, 29 May 2015 05:50:06
        /// </param>
        /// <returns>The current date with or without an offset in the default or specified format.</returns>
        public static string GetDateTime(string Offset = null, string Format = null)
        {
            
            return DateTime.Now.AddSeconds(Offset != null ? Convert.ToInt32(Offset.Contains("s") ? Offset.Replace("s", "") : "0") : 0)
                               .AddMinutes(Offset != null ? Convert.ToInt32(Offset.Contains("m") ? Offset.Replace("m", "") : "0") : 0)
                               .AddHours(Offset != null ? Convert.ToInt32(Offset.Contains("h") ? Offset.Replace("h", "") : "0") : 0)
                               .AddDays(Offset != null ? Convert.ToInt32(Offset.Contains("d") ? Offset.Replace("d", "") : "0") : 0)
                               .AddMonths(Offset != null ? Convert.ToInt32(Offset.Contains("M") ? Offset.Replace("M", "") : "0") : 0)
                               .AddYears(Offset != null ? Convert.ToInt32(Offset.Contains("y") ? Offset.Replace("y", "") : "0") : 0)
                               .ToString(Format ?? Config.current.defaults.DateFormat);

        }


        /// <summary>
        ///     Waits for the given time period in Seconds.
        /// </summary>
        /// <param name="Duration">Optional. Can be null or blank and will wait for the default wait period</param>
        public static void Wait(int Duration = 0)
        {

            Thread.Sleep((Duration == 0 ? Config.current.defaults.timeout : Duration) * 1000);

        }


        /// <summary>
        ///     Replaces invalid characters witin a string.
        /// </summary>
        /// <param name="Text">The text to check and remove characters from</param>
        /// <returns>The given Text with invalid characters removed</returns>
        public static string ReplaceInvalidChars(string Text)
        {

            return Text.Replace(@"\", "").Replace("/", "").Replace("<", "").Replace(">", "").Replace(":", "").Replace("\"", "").Replace("|", "").Replace("?", "").Replace("*", "");

        }


        /// <summary>
        ///     Generate a random Lorum Ipsum text block
        /// </summary>
        /// <param name="minWords">The minimum number of words the text block should contain</param>
        /// <param name="maxWords">The maximum number of words the text block should contain</param>
        /// <param name="minSentences">The minimum number of sentances the text block should contain</param>
        /// <param name="maxSentences">The maximum number of sentances the text block should contain</param>
        /// <param name="numParagraphs">The minimum number of paragraphs the text block should contain</param>
        /// <returns>The Lorum Ipsum text block</returns>
        public static string LoremIpsum(int minWords, int maxWords, int minSentences, int maxSentences, int numParagraphs)
        {

            var words = new[]{"lorem", "ipsum", "dolor", "sit", "amet", "consectetuer", "adipiscing", "elit", "sed", "diam", "nonummy", "nibh", "euismod", "tincidunt", "ut", "laoreet", "dolore", "magna", "aliquam", "erat"};
            var rand = new Random();
            int numSentences = rand.Next(maxSentences - minSentences) + minSentences + 1;
            int numWords = rand.Next(maxWords - minWords) + minWords + 1;

            StringBuilder result = new StringBuilder();

            for (int p = 0; p < numParagraphs; p++)
            {

                for (int s = 0; s < numSentences; s++)
                {

                    for (int w = 0; w < numWords; w++)
                    {

                        if (w > 0) { result.Append(" "); }
                        result.Append(words[rand.Next(words.Length)]);

                    }

                    result.Append(". ");
                }

            }

            return result.ToString();
        }

    }

}
