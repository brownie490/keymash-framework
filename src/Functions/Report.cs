using System;
using Gherkin;
using System.IO;
using System.Xml;
using System.Drawing;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using Newtonsoft.Json.Linq;
using System.Drawing.Imaging;
using TechTalk.SpecFlow.Infrastructure;

namespace Automation
{
    class Report : BasePage
    {

        /// <summary>
        ///     Takes a screenshot of the given page.
        /// </summary>
        /// <param name="scenarioContext">
        ///     Optional: The current scenario we are executing. If specified,
        ///     the FileName will be generated from the scenario name.
        ///     If not specified, a random FileName will be generated.
        /// </param>
        /// <param name="specFlowOutputHelper">Optional: This will help attach the evidence to the step defintion.
        /// If null, it will not attach it.
        /// </param>
        public static void Screenshot(ScenarioContext scenarioContext = null, ISpecFlowOutputHelper specFlowOutputHelper = null)
        {

            // If output is switched on in the config file, create the file
            if (Config.current.reporting.output)
            {

                string FileName = GenerateFileName("." + Config.current.reporting.screenshotformat.ToLower(), scenarioContext);


                // If the file already exists, delete it
                if (File.Exists(FileName))
                {

                    Windows.MakeFileReadWrite(FileName);
                    Windows.DeleteFile(FileName);
                    Check.IsFalse(File.Exists(FileName), "The file was not deleted successfully. FileName: " + FileName);

                }

                // Take a screenshot
                Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                screenshot.SaveAsFile(FileName, ScreenshotFormat());

                // Add a watermark to the screenshot
                WatermarkImage(FileName);

                // Make the File Read-Only if required
                if (Config.current.reporting.ReadOnlyFiles)
                {

                    Windows.MakeFileReadOnly(FileName);

                }

                // Ensure the screenshot has been created
                Check.IsTrue(File.Exists(FileName), "The screenshot was not saved successfully. FileName: " + FileName);

                // Add the image to the LivingDoc if required
                if (specFlowOutputHelper != null)
                {

                    specFlowOutputHelper.WriteLine("Test Evidence:");
                    specFlowOutputHelper.AddAttachment(FileName);

                }

            }

        }


        /// <summary>
        ///     Adds a Watermark to an image
        /// </summary>
        /// <param name="FileName">The path to the file to add the Watermark to.</param>
        /// <param name="Text">Optional: The text to add to the image. If left blank, the current datetime will be added.</param>
        public static void WatermarkImage(string FileName, string Text = null)
        {

            // If watermarking is enabled, add the watermark
            if (Config.current.reporting.AddWatermark)
            {

                try
                {

                    string SourceImage = "temp_" + FileName;
                    string TargetImage = FileName;


                    // If the file already exists, delete it
                    if (File.Exists(SourceImage))
                    {

                        Windows.DeleteFile(SourceImage);

                    }

                    // Rename the existing file to a temporary name
                    Windows.RenameFile(FileName, SourceImage);

                    // If no text has been provided, add the datetime to the image
                    Text ??= (Config.current.reporting.ShowBrowser ? "Browser: " + Utils.ToTitleCase(Config.current.driver.browser) + "\n" : "") + Utils.GetDateTime(null, Config.current.reporting.WatermarkDateTimeFormat);

                    // Open source image
                    FileStream source = new FileStream(SourceImage, FileMode.Open);
                    Stream output = new MemoryStream();
                    #pragma warning disable CA1416 // Validate platform compatibility
                    Image img = Image.FromStream(source);

                    // Set the font
                    Font font = new Font(Config.current.reporting.WatermarkFont, Config.current.reporting.WatermarkFontSize, FontStyle.Bold, GraphicsUnit.Pixel);

                    // Choose color and transparency
                    Color color = Color.FromArgb(100, 255, 0, 0);

                    // Set the location of the watermark
                    Point pt = new Point(WatermarkXCoordinates(img.Width), WatermarkYCoordinates(img.Height));
                    SolidBrush brush = new SolidBrush(color);

                    // Draw text on image
                    Graphics graphics = Graphics.FromImage(img);
                    graphics.DrawString(Text, font, brush, pt);
                    graphics.Dispose();

                    // Update the image
                    img.Save(output, ImageFormat.Png);
                    Image imgFinal = Image.FromStream(output);

                    // Write the new image to file
                    Bitmap bmp = new Bitmap(img.Width, img.Height, img.PixelFormat);
                    Graphics graphics2 = Graphics.FromImage(bmp);
                    graphics2.DrawImage(imgFinal, new Point(0, 0));
                    bmp.Save(TargetImage, WatermarkScreenshotFormat());
                    source.Dispose();
                    imgFinal.Dispose();
                    img.Dispose();

                    // Delete the original image
                    Windows.DeleteFile(SourceImage);

                    // Ensure the original image has been deleted and the new one created.
                    Check.IsFalse(File.Exists(SourceImage));
                    Check.IsTrue(File.Exists(TargetImage));
                    #pragma warning restore CA1416 // Validate platform compatibility
                }
                catch (Exception error)
                {

                    Check.Fail("Something went wrong. Error:" + error.ToString());

                }

            }

        }


        /// <summary>
        ///     Gets the required x coordinate for the Watermark based off the value in the config file
        /// </summary>
        /// <returns>The x coordinate based off the value in the config file.</returns>
        public static int WatermarkXCoordinates(int ImageWidth)
        {

            switch(Config.current.reporting.WatermarkLocation.ToLower())
            {
                case "top-left":
                    return 10;

                case "bottom-left":
                    return 10;

                case "top-right":
                    return ImageWidth - 250;

                case "bottom-right":
                    return ImageWidth - 250;

                default:
                    return 10;
            }

        }


        /// <summary>
        ///     Gets the required y coordinate for the Watermark based off the value in the config file
        /// </summary>
        /// <returns>The y coordinate based off the value in the config file.</returns>
        public static int WatermarkYCoordinates(int ImageHeight)
        {

            switch (Config.current.reporting.WatermarkLocation.ToLower())
            {
                case "top-left":
                    return 5;

                case "bottom-left":
                    return ImageHeight - 50;

                case "top-right":
                    return 5;

                case "bottom-right":
                    return ImageHeight - 50;

                default:
                    return 5;
            }

        }


        /// <summary>
        ///     Writes the given data to a Json file
        /// </summary>
        /// <param name="Json">The Json body to show in the file</param>
        /// <param name="scenarioContext">
        ///     Optional: The current scenario we are executing. If specified,
        ///     the FileName will be generated from the scenario name.
        ///     If not specified, a random FileName will be generated.
        /// </param>
        /// <param name="specFlowOutputHelper">Optional: This will help attach the evidence to the step defintion.
        /// If null, it will not attach it.
        /// </param>
        public static void OutputJson(string Json, ScenarioContext scenarioContext = null, ISpecFlowOutputHelper specFlowOutputHelper = null)
        {

            // If output is switched on in the config file, create the file
            if (Config.current.reporting.output)
            {

                string FileName = GenerateFileName(".json", scenarioContext);


                if (Utils.IsValidJson(Json))
                {

                    // Format the Json string
                    Json = JToken.Parse(Json).ToString((Newtonsoft.Json.Formatting)Formatting.Indented);

                }

                // If the file already exists, delete it
                if (File.Exists(FileName))
                {

                    Windows.MakeFileReadWrite(FileName);
                    Windows.DeleteFile(FileName);
                    Check.IsFalse(File.Exists(FileName), "The file was not deleted successfully. FileName: " + FileName);

                }

                // Create and output the Json to the file
                Windows.CreateFile(FileName);
                Windows.WriteToFile((scenarioContext != null ? "Scenario: " + scenarioContext.ScenarioInfo.Title + "\nStep: " + scenarioContext.StepContext.StepInfo.Text + "\n\nDate Time: " + Utils.GetDateTime(null, "dd/MM/yyyy HH:mm tt") + "\n\nResponse\n" : "Response:\n") + Json, FileName);

                // Make the File Read-Only if required
                if (Config.current.reporting.ReadOnlyFiles)
                {

                    Windows.MakeFileReadOnly(FileName);

                }

                // Ensure the file has been created
                Check.IsTrue(File.Exists(FileName), "The screenshot was not saved successfully. FileName: " + FileName);

                // Add the Json to the LivingDoc if required
                if (specFlowOutputHelper != null)
                {

                    specFlowOutputHelper.WriteLine("Test Evidence:");
                    specFlowOutputHelper.WriteLine(Windows.ReadFile(FileName));

                }
            }        

        }


        /// <summary>
        ///     Writes the given HTML to a file
        /// </summary>
        /// <param name="HTML">The HTML to write to the file</param>
        /// <param name="scenarioContext">
        ///     Optional: The current scenario we are executing. If specified,
        ///     the FileName will be generated from the scenario name.
        ///     If not specified, a random FileName will be generated.
        /// </param>
        /// <param name="specFlowOutputHelper">Optional: This will help attach the evidence to the step defintion.
        /// If null, it will not attach it.
        /// </param>
        public static void OutputHTML(string HTML, ScenarioContext scenarioContext = null, ISpecFlowOutputHelper specFlowOutputHelper = null)
        {

            // If output is switched on in the config file, create the file
            if (Config.current.reporting.output)
            {

                if (!Utils.IsHTMLValid(HTML))
                {

                    Check.Fail("The given HTML is not valid.");

                }

                string FileName = GenerateFileName(".html", scenarioContext);

                // If the file already exists, delete it
                if (File.Exists(FileName))
                {

                    Windows.MakeFileReadWrite(FileName);
                    Windows.DeleteFile(FileName);
                    Check.IsFalse(File.Exists(FileName), "The file was not deleted successfully. FileName: " + FileName);

                }

                // Create and output the HTML to the file
                Windows.CreateFile(FileName);
                Windows.WriteToFile(HTML, FileName);

                // Make the File Read-Only if required
                if (Config.current.reporting.ReadOnlyFiles)
                {

                    Windows.MakeFileReadOnly(FileName);

                }

                // Ensure the file has been created
                Check.IsTrue(File.Exists(FileName), "The HTML file was not saved successfully. FileName: " + FileName);

                // Add the link to the LivingDoc if required
                if (specFlowOutputHelper != null)
                {

                    specFlowOutputHelper.WriteLine("Test Evidence:");
                    specFlowOutputHelper.AddAttachment(FileName);

                }

            }

        }


        /// <summary>
        ///     Generates a FileName for the Test Evidence
        /// </summary>
        /// <param name="Suffix">Optional. The File extensiion to add to the end of the FileName</param>
        /// <param name="scenarioContext">
        ///     Optional: The current scenario we are executing. If specified,
        ///     the FileName will be generated from the scenario name.
        ///     If not specified, a random FileName will be generated.
        /// </param>
        public static string GenerateFileName(string Suffix = null, ScenarioContext scenarioContext = null)
        {

            string FileName = null;
            Random rnd = new Random();


            // If the given filename is null or empty, generate a random name
            if (scenarioContext != null)
            {

                FileName = Utils.ToTitleCase(scenarioContext.ScenarioInfo.Title).Replace("\"", "").Replace("'", "") + " - " + Utils.ToTitleCase(scenarioContext.StepContext.StepInfo.Text).Replace("\"", "").Replace("'", "");
                
            }
            else
            {
                // Generate a unique fileName
                for (int i = 0; i < 10; i++)
                {

                    FileName += rnd.Next(1, 9).ToString();

                }

            }

            return Utils.ReplaceInvalidChars(FileName) + (Suffix ?? null);

        }


        /// <summary>
        ///     Gets the required screenshot format from the config file
        /// </summary>
        /// <returns>The ScreeshotImageFormat as specified in the config file</returns>
        public static ScreenshotImageFormat ScreenshotFormat()
        {

            return (Config.current.reporting.screenshotformat.ToUpper()) switch
            {
                "PNG" => ScreenshotImageFormat.Png,
                "JPG" => ScreenshotImageFormat.Jpeg,
                "JEPG" => ScreenshotImageFormat.Jpeg,
                "BMP" => ScreenshotImageFormat.Bmp,
                _ => ScreenshotImageFormat.Png,
            };

        }


        /// <summary>
        ///     Gets the required screenshot format from the config file
        /// </summary>
        /// <returns>The ImageFormat as specified in the config file</returns>
        public static ImageFormat WatermarkScreenshotFormat()
        {

            return (Config.current.reporting.screenshotformat.ToUpper()) switch
            {
            #pragma warning disable CA1416 // Validate platform compatibility
                "PNG" => ImageFormat.Png,
                "JPG" => ImageFormat.Jpeg,
                "JEPG" => ImageFormat.Jpeg,
                "BMP" => ImageFormat.Bmp,
                _ => ImageFormat.Png,
            };
            #pragma warning restore CA1416 // Validate platform compatibility
        }


        /// <summary>
        ///     Outputs all the Features and Scenarios to a txt file to be used as part of Formal Testing
        /// </summary>
        public static void GenerateTestCaseTable()
        {

            string FileName = "TestCaseTable.txt";


            // Generate the Header Row
            Windows.DeleteFile(FileName);
            Windows.WriteToFile("|Test Case ID|Type (i.e. Regression, Performance)|Test Case Title|Test Case Objective|", FileName);

            // Get a list of Feature Files
            string[] Files = Directory.GetFiles("../features/", "*.feature", SearchOption.AllDirectories);

            // Loop through each Feature file in the Features folder
            foreach (string File in Files)
            {

                var parser              = new Parser();
                var gherkinDocument     = parser.Parse(File);
                string FeatureName      = gherkinDocument.Feature.Name.ToString();
                string[] FeatureWords   = FeatureName.Split(" ");
                string Objective        = FeatureName.Replace(FeatureWords[0] + " - ", "");

                // Loop through each Scenario in the Feature
                foreach (Gherkin.Ast.Scenario ScenarioInfo in gherkinDocument.Feature.Children)
                {

                    var ScenarioTitle       = ScenarioInfo.Name;
                    string[] ScenarioWords  = ScenarioTitle.Split(" ");
                    string TestCaseID       = ScenarioWords[0];

                    string Row = "|" + TestCaseID + "|Functional|" + ScenarioTitle + "|" + Objective + "|";
                    Windows.WriteToFile(Row, FileName);

                }

            }

        }


        /// <summary>
        ///     Output a message to the required Slack Channel
        /// </summary>
        /// <param name="Message">The message to show in the post</param>
        public static void Slack(string Message)
        {

            string RequestBody = "{\"text\": \"" + Message + "\"}";


            // Post the request and ensure the response return was correct
            string Response = API.Post(Config.current.reporting.SlackURL, RequestBody);
            Check.AreEqual("204", Response);

        }

    }

}
