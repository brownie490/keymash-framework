using System;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using Microsoft.VisualStudio.TestTools.UnitTesting;


[assembly: Parallelize(Workers = 0, Scope = ExecutionScope.ClassLevel)]


namespace Automation
{
    public class BasePage
    {

        public static IWebDriver driver;
        public static string DriverPath = Path.GetFullPath(@"../") + @"bin\Debug\net5.0";


        // Constructor of base class
        public BasePage()
        {

        }


        /// <summary>
        ///     Sets up a new instance of a WebDriver
        /// </summary>
        public static void SetupDriver()
        {

            // If the driver is not initialized, set up a new one
            if (driver == null)
            {

                switch (Config.current.driver.browser.ToUpper())
                {

                    case "CHROME":

                        driver = Config.current.driver.type.ToUpper() == "REMOTE" ? NewRemoteChromeDriver() : NewChromeDriver();
                        break;

                    case "FIREFOX":

                        driver = Config.current.driver.type.ToUpper() == "REMOTE" ? NewRemoteFirefoxDriver() : NewFirefoxDriver();
                        break;

                    //case "EDGE":

                    //    driver = NewEdgeDriver();
                    //    //    driver = Config.current.driver.type.ToUpper() == "REMOTE" ? NewRemoteEdgeDriver() : NewEdgeDriver();
                    //    break;

                    default:

                        Check.Fail("Invalid Browser: " + Config.current.driver.browser);
                        break;

                }

            }

        }


        /// <summary>
        ///     Sets up a new instance of a local ChromeDriver
        /// </summary>
        /// <returns>The new isntance of the local ChromeDriver</returns>
        public static IWebDriver NewChromeDriver()
        {

            IWebDriver driver;

            ChromeOptions chromeOptions = new ChromeOptions();

            // Maxmimize the screen if required
            chromeOptions.AddArgument(Config.current.driver.startmaximized ? "--start-maximized" : "--window-size=" + Config.current.driver.defaultwindowsize);

            // If we're using a specific user agent, add it as an argument
            if (!string.IsNullOrEmpty(Config.current.driver.useragent))
            {

                chromeOptions.AddArgument("--user-agent=" + Config.current.driver.useragent);

            }

            // Ignore Certificate errors
            chromeOptions.AcceptInsecureCertificates = Config.current.driver.ignoreinvalidcerts;

            // Launch the new driver
            driver = new ChromeDriver(DriverPath, chromeOptions);

            return driver;

        }


        /// <summary>
        ///     Sets up a new instance of a remote ChromeDriver
        /// </summary>
        /// <returns>The new isntance of the remote ChromeDriver</returns>
        public static RemoteWebDriver NewRemoteChromeDriver()
        {

            RemoteWebDriver driver;

            ChromeOptions chromeOptions = new ChromeOptions
            {

                BrowserVersion = "",
                PlatformName = "LINUX",

            };

            // Maxmimize the screen if required
            chromeOptions.AddArgument("--start-maximized");

            // If we're using a specific user agent, add it as an argument
            if (!string.IsNullOrEmpty(Config.current.driver.useragent))
            {

                chromeOptions.AddArgument("--user-agent=" + Config.current.driver.useragent);

            }

            // Ignore Certificate errors
            chromeOptions.AcceptInsecureCertificates = Config.current.driver.ignoreinvalidcerts;

            // Launch the new driver
            Uri GRID = new Uri(Config.current.seleniumHub.URL);
            driver = new RemoteWebDriver(GRID, chromeOptions.ToCapabilities());

            return driver;

        }


        /// <summary>
        ///     Sets up a new instance of a local FirefoxDriver
        /// </summary>
        /// <returns>The new isntance of the local FirefoxDriver</returns>
        public static IWebDriver NewFirefoxDriver()
        {

            IWebDriver driver;

            FirefoxOptions firefoxOptions = new FirefoxOptions();

            // Set the Gecko Driver Host. This makes it run quicker
            FirefoxDriverService firefoxService = FirefoxDriverService.CreateDefaultService(DriverPath);
            firefoxService.Host = "::1";

            // Maxmimize the screen if required
            firefoxOptions.AddArgument(Config.current.driver.startmaximized ? "--start-maximized" : "--window-size=" + Config.current.driver.defaultwindowsize);

            // If we're using a specific user agent, add it as an argument
            if (!string.IsNullOrEmpty(Config.current.driver.useragent))
            {

                firefoxOptions.AddArgument("--user-agent=" + Config.current.driver.useragent);

            }

            // Ignore Certificate errors
            firefoxOptions.AcceptInsecureCertificates = Config.current.driver.ignoreinvalidcerts;

            // Launch the new driver
            driver = new FirefoxDriver(firefoxService, firefoxOptions);

            return driver;

        }


        /// <summary>
        ///     Sets up a new instance of a remote FirefoxDriver
        /// </summary>
        /// <returns>The new isntance of the remote FirefoxDriver</returns>
        public static RemoteWebDriver NewRemoteFirefoxDriver()
        {

            RemoteWebDriver driver;

            FirefoxOptions firefoxOptions = new FirefoxOptions
            {

                BrowserVersion = "",
                PlatformName = "LINUX",

            };

            // Maxmimize the screen if required
            firefoxOptions.AddArgument(Config.current.driver.startmaximized ? "--start-maximized" : "--window-size=" + Config.current.driver.defaultwindowsize);

            // If we're using a specific user agent, add it as an argument
            if (!string.IsNullOrEmpty(Config.current.driver.useragent))
            {

                firefoxOptions.AddArgument("--user-agent=" + Config.current.driver.useragent);

            }

            // Launch the new driver
            Uri GRID = new Uri(Config.current.seleniumHub.URL);
            driver = new RemoteWebDriver(GRID, firefoxOptions.ToCapabilities());

            return driver;

        }


        ///// <summary>
        /////     Sets up a new instance of a local EdgeDriver
        ///// </summary>
        ///// <returns>The new isntance of the local EdgeDriver</returns>
        //public static IWebDriver NewEdgeDriver()
        //{

        //    IWebDriver driver;

        //    // Launch Microsoft Edge (Chromium)
        //    EdgeOptions edgeOptions = new EdgeOptions();
        //    edgeOptions.UseChromium = true;

        //    // Maxmimize the screen if required
        //    edgeOptions.AddArgument(Config.current.driver.startmaximized ? "--start-maximized" : "--window-size=" + Config.current.driver.defaultwindowsize);

        //    // If we're using a specific user agent, add it as an argument
        //    if (!string.IsNullOrEmpty(Config.current.driver.useragent))
        //    {

        //        edgeOptions.AddArgument("--user-agent=" + Config.current.driver.useragent);

        //    }

        //    // Ignore Certificate errors
        //    edgeOptions.AcceptInsecureCertificates = Config.current.driver.ignoreinvalidcerts;
        //    driver = new EdgeDriver(DriverPath, edgeOptions);

        //    return driver;

        //    }


            ///// <summary>
            /////     Sets up a new instance of a remote EdgeDriver
            ///// </summary>
            ///// <returns>The new isntance of the remote EdgeDriver</returns>
            //public static RemoteWebDriver NewRemoteEdgeDriver()
            //{
            //    RemoteWebDriver driver;

            //    var edgeOptions = new EdgeOptions
            //    {

            //        BrowserVersion = "",
            //        PlatformName = "LINUX",

            //    };

            //    // Maxmimize the screen if required
            //    edgeOptions.AddArgument(Config.current.driver.startmaximized ? "--start-maximized" : "--window-size=" + Config.current.driver.defaultwindowsize);

            //    // Launch the new driver
            //    driver = new RemoteWebDriver(GRID, edgeOptions.ToCapabilities());

            //    return driver;

            //}


            /// <summary>
            ///     Tears down the current WebDriver
            /// </summary>
            public static void TearDownDriver()
        {

            // If the driver is still initialized
            if (driver != null)
            {

                // Kill the driver
                Web.Close();

            }

        }

        public static void ReloadDriver()
        {

            TearDownDriver();
            Utils.Wait(2);
            SetupDriver();

        }

    }

}
