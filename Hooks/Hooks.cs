using TechTalk.SpecFlow;

namespace Automation
{

    [Binding]
    public class Hooks
    {

        [BeforeTestRun]
        public static void InitializeConfig()
        {

            // Build up the config
            Config.Init();

        }


        [BeforeScenario, Scope(Tag = "Manual")]
        public static void ManualSteps()
        {


        }


        [BeforeScenario, Scope(Tag = "Web")]
        public static void LaunchTheApplication()
        {

            // Set up the WebDriver
            BasePage.SetupDriver();

        }


        [AfterScenario, Scope(Tag = "Web")]
        public static void TeardownDriver()
        {

            // Teardown the WebDriver
            BasePage.TearDownDriver();

        }


        [AfterTestRun]
        public static void ClearAllDriverProcesses()
        {

            string[] Processes = new string[] { "chromedriver", "geckodriver" };


            foreach (string Process in Processes)
            {

                Windows.KillAllProcesses(Process);

            }

        }


        public class Setup
        {

            public static void Main()
            {

            }

        }

    }

}
