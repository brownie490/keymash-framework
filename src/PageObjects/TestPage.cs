using OpenQA.Selenium;
using SeleniumExtras.PageObjects;


namespace Automation
{
    public class TestPage
    {

        private readonly IWebDriver driver;

        public TestPage(IWebDriver driver)
        {

            this.driver = driver;
            PageFactory.InitElements(driver, this);

        }

        [FindsBy(How = How.Id, Using = "username")]
        public IWebElement Username { get; set; }

        [FindsBy(How = How.Id, Using = "username-error")]
        public IWebElement UsernameErrorTextLocation { get; set; }
        public string UsernameErrorText = "Username is required";

        [FindsBy(How = How.Id, Using = "password")]
        public IWebElement Password { get; set; }

        [FindsBy(How = How.Id, Using = "password-error")]
        public IWebElement PasswordErrorTextLocation { get; set; }
        public string PasswordErrorText = "Password is required";

        [FindsBy(How = How.Id, Using = "email")]
        public IWebElement Email { get; set; }

        [FindsBy(How = How.Id, Using = "email-error")]
        public IWebElement EmailErrorTextLocation { get; set; }
        public string EmailErrorText = "Email is required";

        [FindsBy(How = How.Id, Using = "website")]
        public IWebElement Website { get; set; }

        [FindsBy(How = How.Id, Using = "website-error")]
        public IWebElement WebsiteErrorTextLocation { get; set; }
        public string WebsiteErrorText = "Website is required";

        [FindsBy(How = How.Id, Using = "message")]
        public IWebElement Message { get; set; }

        [FindsBy(How = How.Id, Using = "message-error")]
        public IWebElement MessageErrorTextLocation { get; set; }
        public string MessageErrorText = "Message is required";

        [FindsBy(How = How.Id, Using = "Bike")]
        public IWebElement BikeOption { get; set; }

        [FindsBy(How = How.Id, Using = "Car")]
        public IWebElement CarOption { get; set; }

        [FindsBy(How = How.Id, Using = "CarsList")]
        public IWebElement CarsDropdown { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#contact > div.dropdown > button")]
        public IWebElement HoverOverMeOption { get; set; }

        [FindsBy(How = How.LinkText, Using = "Link 1")]
        public IWebElement Link1 { get; set; }

        [FindsBy(How = How.LinkText, Using = "Link 2")]
        public IWebElement Link2 { get; set; }

        [FindsBy(How = How.LinkText, Using = "Link 3")]
        public IWebElement Link3 { get; set; }

        [FindsBy(How = How.Id, Using = "submit")]
        public IWebElement Submit { get; set; }

        public string InvalidFieldHighlighting = "form-control invalid";

        [FindsBy(How = How.CssSelector, Using = "body > div > p > b")]
        public IWebElement FormSubmittedTextLocation { get; set; }
        public string FormSubmittedText = "Feedback submitted.";

        public void GoToPage()
        {

            driver.Navigate().GoToUrl(Config.current.testPage.URL);

        }

        public void UsernameErrorDisplayed()
        {

            Check.AreEqual(InvalidFieldHighlighting, Web.GetAttribute("class", Username));
            Check.AreEqual(UsernameErrorText, UsernameErrorTextLocation.Text);

        }

        public void PasswordErrorDisplayed()
        {

            Check.AreEqual(InvalidFieldHighlighting, Web.GetAttribute("class", Password));
            Check.AreEqual(PasswordErrorText, PasswordErrorTextLocation.Text);

        }

        public void EmailErrorDisplayed()
        {

            Check.AreEqual(InvalidFieldHighlighting, Web.GetAttribute("class", Email));
            Check.AreEqual(EmailErrorText, EmailErrorTextLocation.Text);

        }

        public void WebsiteErrorDisplayed()
        {

            Check.AreEqual(InvalidFieldHighlighting, Web.GetAttribute("class", Website));
            Check.AreEqual(WebsiteErrorText, WebsiteErrorTextLocation.Text);

        }

        public void MessageErrorDisplayed()
        {

            Check.AreEqual(InvalidFieldHighlighting, Web.GetAttribute("class", Message));
            Check.AreEqual(MessageErrorText, MessageErrorTextLocation.Text);

        }

    }

}