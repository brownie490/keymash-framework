using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;


namespace Automation
{
    class Web : BasePage
    {

        /// <summary>
        ///     Go to the given URL with the current driver
        /// </summary>
        /// <param name="URL">The URL to navigate to</param>
        public static void GoTo(string URL)
        {

            driver.Navigate().GoToUrl(URL);

        }


        /// <summary>
        ///     Gets the current page's URL
        /// </summary>
        public static string GetURL()
        {

            return driver.Url;

        }


        /// <summary>
        ///     Refresh the current page
        /// </summary>
        public static void Refresh()
        {

            driver.Navigate().Refresh();

        }


        /// <summary>
        ///     Go back to the previous page
        /// </summary>
        public static void Back()
        {

            driver.Navigate().Back();

        }


        /// <summary>
        ///     Go forward one page
        /// </summary>
        public static void Forward()
        {

            driver.Navigate().Forward();

        }


        /// <summary>
        ///     Closes the current browser
        /// </summary>
        public static void Close()
        {

            driver.Quit();
            driver = null;

        }


        /// <summary>
        ///     Types a give value into a field.
        /// </summary>
        /// <param name="Text">The text to type into the field</param>
        /// <param name="WebElement">The element to type in</param>
        /// <param name="ClearFirst">Optional: Clear the element first? Defaults to true</param>
        public static void Type(string Text, IWebElement WebElement, bool ClearFirst = true)
        {

            // Wait for the element to exist
            WaitForElement(WebElement);

            // While the field does not contain the text
            if (WebElement.GetAttribute("value") != Text && Text != null)
            {

                // Type into the field.
                if (ClearFirst)
                {

                    Clear(WebElement);

                }

                WebElement.SendKeys(Text);

            }

            // Ensure the value has been typed in
            Check.AreEqual(Text, !string.IsNullOrEmpty(WebElement.GetAttribute("value")) ? WebElement.GetAttribute("value") : (Text == null ? null : ""));

        }


        /// <summary>
        ///     Types a given value into a field one character at a time.
        /// </summary>
        /// <param name="Text">The text to type into the field</param>
        /// <param name="WebElement">The element to type in.</param>
        /// <param name="Delay">Optional: The period to wait for after typing each character</param>
        /// <param name="ClearFirst">Optional: Clear the element first? Defaults to true</param>
        public static void TypeTextPerCharacter(string Text, IWebElement WebElement, int Delay = 0, bool ClearFirst = true)
        {

            // Wait for the element to exist
            WaitForElement(WebElement);

            // Clear the element's contents
            Clear(WebElement);

            // If the text to enter is valid...
            if (!string.IsNullOrEmpty(Text))
            {

                // Clear the field
                if (ClearFirst)
                {

                    Clear(WebElement);

                }

                // Type each character into the field
                foreach (var Character in Text)
                {

                    WebElement.SendKeys(Character.ToString());

                    // If we need to delay entering the characters...
                    if (Delay != 0)
                    {

                        Utils.Wait(Delay);

                    }

                }

            }

            // Ensure the value has been typed in
            Check.AreEqual(Text, WebElement.GetAttribute("value"));

        }


        /// <summary>
        ///     Clicks the given element
        /// </summary>
        /// <param name="WebElement">The element to click.</param>
        public static void Click(IWebElement WebElement)
        {

            // Wait until the element is clickable
            WaitUntilClickable(WebElement);

            // Click the element
            WebElement.Click();

        }


        /// <summary>
        ///     Clears the given field so that it's empty.
        /// </summary>
        /// <param name="WebElement">The element to clear.</param>
        public static void Clear(IWebElement WebElement)
        {

            // Clear the element
            WebElement.Clear();

            // Ensure the value has been typed in
            Check.AreEqual("", WebElement.GetAttribute("value"));

        }


        /// <summary>
        ///     Selects an option from the given dropdown list
        /// </summary>
        /// <param name="Option">The option to select from the list</param>
        /// <param name="Dropdown">The dropdown list to select from.</param>
        public static void SelectFromList(string Option, IWebElement Dropdown)
        {

            // Select the option from the dropdown list
            var selectElement = new SelectElement(Dropdown);
            selectElement.SelectByText(Option);

            // Ensure it's been selected
            var selectedValue = selectElement.SelectedOption.Text;
            Check.AreEqual(Option.ToLower(), selectedValue.ToLower());

        }


        /// <summary>
        ///     Ticks a checkbox if it's currently unticked.
        /// </summary>
        /// <param name="TickBox">The TickBox to select</param>
        public static void TickCheckbox(IWebElement TickBox)
        {

            // Find the Tickbox and tick it if it's not already ticked
            if (!TickBox.Selected)
            {

                TickBox.Click();

            }

            // Ensure the TickBox is selected
            Check.IsTrue(Convert.ToBoolean(TickBox.GetAttribute("checked")));

        }


        /// <summary>
        ///     Unticks a checkbox if it's currently ticked.
        /// </summary>
        /// <param name="TickBox">The TickBox to select</param>
        public static void UnTickCheckbox(IWebElement TickBox)
        {

            // Find the Tickbox and unticks it if it's already ticked
            if (TickBox.Selected)
            {

                TickBox.Click();

            }

            // Ensure the TickBox is not selected
            Check.IsFalse(Convert.ToBoolean(TickBox.GetAttribute("checked")));

        }


        /// <summary>
        ///     Waits for the given element to be displayed before continuing.
        /// </summary>
        /// <param name="WebElement">The WebElement to check is displayed</param>
        public static void WaitForElement(IWebElement WebElement)
        {

            int counter = 0;


            // While the element is not displayed
            while (!IsElementDisplayed(WebElement))
            {

                // Wait a second and increment the counter
                Utils.Wait(1);
                counter++;

                // If the Timeout has been reached, break out of the loop
                if (counter >= Config.current.defaults.timeout)
                {

                    break;

                }

            }

        }


        /// <summary>
        ///     Waits for the given element to be clickable before continuing.
        /// </summary>
        /// <param name="WebElement">The WebElement to check is clickable</param>
        public static void WaitUntilClickable(IWebElement WebElement)
        {

            int counter = 0;


            // Wait for the Element to be displayed
            WaitForElement(WebElement);

            // While the element is not enabled
            while (!WebElement.Enabled)
            {

                // Wait a second and increment the counter
                Utils.Wait(1);
                counter++;

                // If the Timeout has been reached, break out of the loop
                if (counter >= Config.current.defaults.timeout)
                {

                    break;

                }

            }

        }


        /// <summary>
        ///     Returns true if the element is displayed
        /// </summary>
        /// <param name="WebElement">The WebElement to check if it is Displayed</param>
        /// <returns>True or False if the element is displayed.</returns>
        public static bool IsElementDisplayed(IWebElement WebElement)
        {

            try
            {

                return WebElement.Displayed;

            }

            catch (Exception)
            {

                return false;

            }

        }


        /// <summary>
        ///     Returns the status of the object as true or false if it is clickable
        /// </summary>
        /// <param name="WebElement">The WebElement to check if it is clickable</param>
        /// <returns>True or False if the element is clickable.</returns>
        public static bool IsElementClickable(IWebElement WebElement)
        {

            // Wait for the Element to be displayed
            WaitForElement(WebElement);

            // If the element is clickable, return true. Otherwise return false
            return WebElement.Enabled;

        }


        /// <summary>
        ///     Waits for the URL of the current page to contain the given string
        /// </summary>
        /// <param name="URL">The URL substring that needs to be contained within the URL</param>
        public static void WaitForURL(string URL)
        {

            int counter = 0;


            // While the URL doesn't contain the given string
            while (GetURL().Contains(URL))
            {
                // Wait a second and increment the counter
                Utils.Wait(1);
                counter++;

                // If the Timeout has been reached, break out of the loop
                if (counter >= Config.current.defaults.timeout)
                {

                    break;

                }

            }

        }


        /// <summary>
        ///     Gets the given attribute for the given element
        /// </summary>
        /// <param name="Attribute">The Attribute to get from the element. For example, "class" or "value".</param>
        /// <param name="WebElement">The WebElement to get the attribute from </param>
        /// <returns>The value of the attribute</returns>
        public static string GetAttribute(string Attribute, IWebElement WebElement)
        {

            // Wait for the Element to be displayed
            WaitForElement(WebElement);

            return WebElement.GetAttribute(Attribute);

        }


        /// <summary>
        ///     Hovers over the specified element. This can be used if a menu item appears when the mouse hovers over the element
        /// </summary>
        /// <param name="WebElement">The WebElement to hover over</param>
        public static void HoverOver(IWebElement WebElement)
        {

            // Hover over the object
            Actions action = new Actions(driver);
            action.MoveToElement(WebElement).Perform();

        }


        /// <summary>
        ///     Drags the source element to the target element
        /// </summary>
        /// <param name="Source">The source element to click.</param>
        /// <param name="Destination">The destination element to drop.</param>
        public static void DragAndDrop(IWebElement Source, IWebElement Destination)
        {

            // Create Actions object
            Actions Builder = new Actions(driver);

            // Create a chain of actions to move element from source to destination
            Builder.ClickAndHold(Source).MoveToElement(Destination).Release(Destination).Build().Perform();

        }


        /// <summary>
        ///     Add a new Cookie with the given name and value
        /// </summary>
        /// <param name="Key">The name of the Cookie to add</param>
        /// <param name="Value">The value to assign to the Cookie</param>
        public static void AddCookie(string Key, string Value)
        {

            try
            {

                // Adds the cookie into current browser context
                Cookie cookie = new Cookie(Key, Value, @"/");
                driver.Manage().Cookies.AddCookie(cookie);

            }

            catch (Exception error)
            {

                Check.Fail("Could not add the Cookie '" + Key + "'. Error: " + error.ToString());

            }

            // Ensure the Cookie has been added.
            Check.IsTrue(IsCookieSet(Key), "The Cookie has not been added.");

        }


        /// <summary>
        ///     Edits an existing Cookie with the given name and value
        /// </summary>
        /// <param name="Key">The name of the Cookie to edit</param>
        /// <param name="Value">The value to assign to the Cookie</param>
        public static void EditCookie(string Key, string Value)
        {

            // Delete the existing Cookie
            DeleteCookie(Key);

            // Add the new Cookie
            AddCookie(Key, Value);

            // Ensure the Cookie has been created
            Check.AreEqual(Value, GetCookieValue(Key));

        }


        /// <summary>
        ///     Deletes the given Cookie
        /// </summary>
        /// <param name="Key">The name of the Cookie to delete</param>
        public static void DeleteCookie(string Key)
        {

            // If the Cookie exists, then delete it
            if (IsCookieSet(Key))
            {

                driver.Manage().Cookies.DeleteCookieNamed(Key);

            }

            // Ensure the Cookie has been deleted
            Check.IsFalse(IsCookieSet(Key), "The Cookie '" + Key + "' has not been deleted");

        }


        /// <summary>
        ///     Deletes all Cookies
        /// </summary>
        public static void DeleteAllCookies()
        {

            driver.Manage().Cookies.DeleteAllCookies();

        }


        /// <summary>
        ///     Gets the value from the given Cookie
        /// </summary>
        /// <param name="Key">The name of the Cookie to get the value from</param>
        /// <returns>The value of the Cookie</returns>
        public static string GetCookieValue(string Key)
        {

            Cookie cookie = driver.Manage().Cookies.GetCookieNamed(Key);

            return cookie.Value;

        }


        /// <summary>
        ///     Checks to see if the given Cookie exists
        /// </summary>
        /// <param name="Key">The name of the Cookie to find</param>
        /// <returns>true or false if the Cookie exists</returns>
        public static bool IsCookieSet(string Key)
        {
            try
            {

                // Attempt to find the Cookie
                var Cookie = driver.Manage().Cookies.GetCookieNamed(Key);

                return Cookie != null;

            }

            catch (Exception)
            {

                return false;

            }

        }

    }

}