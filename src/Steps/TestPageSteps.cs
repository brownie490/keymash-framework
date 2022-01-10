using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Infrastructure;

namespace Automation
{
    [Binding]
    public class TestPageSteps : BasePage
    {

        private readonly ScenarioContext _scenarioContext;
        private readonly ISpecFlowOutputHelper _specFlowOutputHelper;

        public TestPageSteps(ScenarioContext scenarioContext, ISpecFlowOutputHelper outputHelper)
        {

            _scenarioContext = scenarioContext;
            _specFlowOutputHelper = outputHelper;

        }


        [Given(@"I'm on the Test Page")]
        public void GivenIMOnTheTestPage()
        {

            TestPage testPage = new TestPage(driver);
            testPage.GoToPage();
            Check.IsTrue(Web.IsElementDisplayed(testPage.Username), "The Test Page was not displayed.");

        }
        

        [When(@"I complete the form with all the required fields")]
        public void WhenICompleteTheFormWithAllTheRequiredFields()
        {

            TestPage testPage = new TestPage(driver);
            Web.Type(Utils.RandomString(5), testPage.Username);
            Web.Type(Utils.RandomString(10), testPage.Password);
            Web.Type(Utils.RandomString(5) + "@" + Utils.RandomString(5) + ".com", testPage.Email);
            Web.Type("http://" + Utils.RandomString(5) + ".com", testPage.Website);
            Web.Type(Utils.LoremIpsum(10, 50, 1, 2, 1), testPage.Message);
            Report.Screenshot(_scenarioContext, _specFlowOutputHelper);

        }


        [When(@"I do not enter a value in the (.*) field")]
        public void WhenIDoNotEnterAValueInTheField(string Field)
        {

            _scenarioContext["Username"]    = (Field.ToUpper() == "USERNAME" ? null : Utils.RandomString(5));
            _scenarioContext["Password"]    = (Field.ToUpper() == "PASSWORD" ? null : Utils.RandomString(10));
            _scenarioContext["Email"]       = (Field.ToUpper() == "EMAIL" ? null : Utils.RandomString(5) + "@" + Utils.RandomString(5) + ".com");
            _scenarioContext["Website"]     = (Field.ToUpper() == "WEBSITE" ? null : "http://" + Utils.RandomString(5) + ".com");
            _scenarioContext["Message"]     = (Field.ToUpper() == "MESSAGE" ? null : Utils.LoremIpsum(10, 50, 1, 2, 1));

        }


        [When(@"I do not enter any values into the required fields")]
        public void WhenIDoNotEnterAnyValuesIntoTheRequiredFields()
        {

            TestPage testPage = new TestPage(driver);
            Web.Clear(testPage.Username);
            Web.Clear(testPage.Password);
            Web.Clear(testPage.Email);
            Web.Clear(testPage.Website);
            Web.Clear(testPage.Message);

        }


        [When(@"enter all the remaining required fields")]
        public void WhenEnterAllTheRemainingRequiredFields()
        {

            TestPage testPage = new TestPage(driver);
            

            if (!string.IsNullOrEmpty((string)_scenarioContext["Username"]))
            {

                Web.Type((string)_scenarioContext["Username"], testPage.Username);

            }

            if (!string.IsNullOrEmpty((string)_scenarioContext["Password"]))
            {

                Web.Type((string)_scenarioContext["Password"], testPage.Password);

            }

            if (!string.IsNullOrEmpty((string)_scenarioContext["Email"]))
            {

                Web.Type((string)_scenarioContext["Email"], testPage.Email);

            }

            if (!string.IsNullOrEmpty((string)_scenarioContext["Website"]))
            {

                Web.Type((string)_scenarioContext["Website"], testPage.Website);

            }

            if (!string.IsNullOrEmpty((string)_scenarioContext["Message"]))
            {

                Web.Type((string)_scenarioContext["Message"], testPage.Message);

            }

            Report.Screenshot(_scenarioContext, _specFlowOutputHelper);

        }


        [When(@"click Submit")]
        public void WhenClickSubmit()
        {

            TestPage testPage = new TestPage(driver);
            Web.Click(testPage.Submit);

        }


        [When(@"select the I have a bike option")]
        public void WhenSelectTheIHaveABikeOption()
        {

            TestPage testPage = new TestPage(driver);
            Web.TickCheckbox(testPage.BikeOption);

        }


        [When(@"leave the I have a car option unticked")]
        public void WhenLeaveTheIHaveACarOptionUnticked()
        {

            TestPage testPage = new TestPage(driver);
            Web.UnTickCheckbox(testPage.CarOption);

        }


        [When(@"select (.*) from the car dropdown")]
        public void WhenSelectFromTheCarDropdown(string Car)
        {

            TestPage testPage = new TestPage(driver);
            Web.UnTickCheckbox(testPage.BikeOption);
            Web.TickCheckbox(testPage.CarOption);
            Web.SelectFromList(Car, testPage.CarsDropdown);

        }


        [When(@"I hover over the hover option")]
        public void WhenIHoverOverTheHoverOption()
        {

            TestPage testPage = new TestPage(driver);
            Web.HoverOver(testPage.HoverOverMeOption);

        }


        [Then(@"page will indicate that the feedback was submitted")]
        public void ThenPageWillIndicateThatTheFeedbackWasSubmitted()
        {

            TestPage testPage = new TestPage(driver);
            Web.WaitForElement(testPage.FormSubmittedTextLocation);
            Check.AreEqual(testPage.FormSubmittedText, testPage.FormSubmittedTextLocation.Text);
            Report.Screenshot(_scenarioContext, _specFlowOutputHelper);

        }


        [Then(@"the links will be displayed")]
        public void ThenTheLinksWillBeDisplayed()
        {

            TestPage testPage = new TestPage(driver);
            Web.WaitForElement(testPage.Link1);
            Check.IsTrue(Web.IsElementDisplayed(testPage.Link1));
            Check.IsTrue(Web.IsElementDisplayed(testPage.Link2));
            Check.IsTrue(Web.IsElementDisplayed(testPage.Link3));
            Report.Screenshot(_scenarioContext, _specFlowOutputHelper);

        }


        [Then(@"all the required fields will be highlighted")]
        public void ThenAllTheRequiredFieldsWillBeHighlighted()
        {

            TestPage testPage = new TestPage(driver);
            testPage.UsernameErrorDisplayed();
            testPage.PasswordErrorDisplayed();
            testPage.EmailErrorDisplayed();
            testPage.WebsiteErrorDisplayed();
            testPage.MessageErrorDisplayed();
            Report.Screenshot(_scenarioContext, _specFlowOutputHelper);

        }


        [Then(@"the (.*) field will be highlighted")]
        public void ThenTheFieldWillBeHighlighted(string Field)
        {

            TestPage testPage = new TestPage(driver);

            switch (Field.ToUpper())
            {

                case "USERNAME":
                    testPage.UsernameErrorDisplayed();
                    break;

                case "PASSWORD":
                    testPage.PasswordErrorDisplayed();
                    break;

                case "EMAIL":
                    testPage.EmailErrorDisplayed();
                    break;

                case "WEBSITE":
                    testPage.WebsiteErrorDisplayed();
                    break;

                case "MESSAGE":
                    testPage.MessageErrorDisplayed();
                    break;

                default:
                    Check.Fail("Invalid field: " + Field);
                    break;

            }

            Report.Screenshot(_scenarioContext, _specFlowOutputHelper);

        }

    }

}
