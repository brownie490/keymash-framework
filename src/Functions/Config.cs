using System.IO;
using Newtonsoft.Json;

namespace Automation
{

    public class Config
    {

        public Defaults defaults { get; set; }
        public Reporting reporting { get; set; }    
        public SeleniumHub seleniumHub { get; set; }
        public Driver driver { get; set; }
        public TestPage testPage { get; set; }
        public Database database { get; set; }

        public static Config current;


        /// <summary>
        ///     Builds the current Config into an object
        /// </summary>
        public static void Init()
        {

            // If the config has not already been built, bulid it now
            if (current == null)
            {
                
                var jsonString = Windows.ReadFile(Path.GetFullPath(@"../") + "config.json");
                current = JsonConvert.DeserializeObject<Config>(jsonString);

            }

        }


        /// <summary>
        ///     Rebuilds the current Config into an object
        /// </summary>
        public static void Refresh()
        {

            current = null;
            Init();

        }

        public class Defaults
        {
            public int timeout { get; set; }
            public string DateFormat { get; set; }

        }


        public class Reporting
        {
            public bool output { get; set; }
            public bool AddWatermark { get; set; }
            public string WatermarkDateTimeFormat { get; set; }
            public string WatermarkFont { get; set; }
            public int WatermarkFontSize { get; set; }
            public string WatermarkLocation { get; set; }
            public bool AddTagsToWatermark { get; set; }
            public string screenshotformat { get; set; }
            public string SlackURL { get; set; }
            public bool ReadOnlyFiles { get; set; }
            public bool ShowBrowser { get; set; }


        }


        public class SeleniumHub
        {
            public string URL { get; set; }

        }

        public class Driver
        {
            public string browser { get; set; }
            public string type { get; set; }
            public bool Headless { get; set; }
            public bool startmaximized { get; set; }
            public int WindowXSize { get; set; }
            public int WindowYSize { get; set; }
            public bool ignoreinvalidcerts { get; set; }
            public string EmulateDevice { get; set; }
            public string useragent { get; set; }

        }

        public class TestPage
        {
            public string URL { get; set; }

        }

        public class Database
        {
            public string source { get; set; }
            public string dbName { get; set; }
            public bool integratedSecurity { get; set; }
            public string userID { get; set; }
            public string password { get; set; }

        }

    }

}
