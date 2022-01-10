using System;
using OpenQA.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Automation
{

    class Check
    {


        /// <summary>
        ///     Ensures the given element is displayed
        /// </summary>
        /// <param name="WebElement">The element to check</param>
        public static void ElementDisplayed(IWebElement WebElement)
        {

            Web.WaitForElement(WebElement);
            IsTrue(Web.IsElementDisplayed(WebElement));

        }


        /// <summary>
        ///     Ensures the given value is true and throws and exception if not
        /// </summary>
        /// <param name="Value">The value to check</param>
        /// <param name="Message">Optional. The custom message to display if the value is not true.</param>
        public static void IsTrue(bool Value, string Message = null)
        {

            Assert.IsTrue(Value, Message);

        }


        /// <summary>
        ///     Ensures the given value is false and throws and exception if not
        /// </summary>
        /// <param name="Value">The value to check</param>
        /// <param name="Message">Optional. The custom message to display if the value is not false.</param>
        public static void IsFalse(bool Value, string Message = null)
        {

            Assert.IsFalse(Value, Message);

        }


        /// <summary>
        ///     Ensures the given values are the same
        /// </summary>
        /// <param name="Expected">The value we are expecting</param>
        /// <param name="Actual">The actual value to compare the Expected value to.</param>
        public static void AreEqual(dynamic Expected, dynamic Actual)
        {

            Assert.AreEqual(Expected, Actual);

        }


        /// <summary>
        ///     Ensures the given values are not the same
        /// </summary>
        /// <param name="NotExpected">The value we are not expecting</param>
        /// <param name="Actual">The actual value to compare the NotExpected value to.</param>
        public static void AreNotEqual(dynamic NotExpected, dynamic Actual)
        {

            Assert.AreNotEqual(NotExpected, Actual);

        }


  //      /// <summary>
  //      ///     Ensures the given value is null
  //      /// </summary>
  //      /// <param name="Value">The value we expecting to be null</param>
  //      public static void IsNull(dynamic Value)
		//{

  //          Assert.IsNull(Value);

		//}


  //      /// <summary>
  //      ///     Ensures the given value is not null
  //      /// </summary>
  //      /// <param name="Value">The value we are not expecting to be null</param>
  //      public static void IsNotNull(dynamic Value)
  //      {

  //          Assert.IsNotNull(Value);

  //      }


        /// <summary>
        ///     Ensures that "This" is greater than "That"
        /// </summary>
        /// <param name="This">The value that needs to be greater than "That"</param>
        /// <param name="That">The value we are comparing against</param>
        /// <param name="Message">Optional. The custom message to display if "This" is not greater than "That".</param>
        public static void IsGreaterThan(dynamic This, dynamic That, string Message = null)
        {

            if (!(This > That))
            {

                Fail(Message);

            }

        }


        /// <summary>
        ///     Ensures that "This" is greater than or equal to "That"
        /// </summary>
        /// <param name="This">The value that needs to be greater than or equal to "That"</param>
        /// <param name="That">The value we are comparing against</param>
        /// <param name="Message">Optional. The custom message to display if "This" is not greater than or equal to "That".</param>
        public static void IsGreaterThanOrEqualTo(dynamic This, dynamic That, string Message = null)
        {

            if (!(This >= That))
            {

                Fail(Message);

            }

        }


        /// <summary>
        ///     Ensures that "This" is less than "That"
        /// </summary>
        /// <param name="This">The value that needs to be less than "That"</param>
        /// <param name="That">The value we are comparing against</param>
        /// <param name="Message">Optional. The custom message to display if "This" is not less than "That".</param>
        public static void IsLessThan(dynamic This, dynamic That, string Message = null)
        {

            if (!(This < That))
            {

                Fail(Message);

            }

        }


        /// <summary>
        ///     Ensures that "This" is less than or equal to "That"
        /// </summary>
        /// <param name="This">The value that needs to be less than or equal to "That"</param>
        /// <param name="That">The value we are comparing against</param>
        /// <param name="Message">Optional. The custom message to display if "This" is not less than or equal to "That".</param>
        public static void IsLessThanOrEqualTo(dynamic This, dynamic That, string Message = null)
        {

            if (!(This <= That))
            {

                Fail(Message);

            }

        }


        /// <summary>
        ///     Checks the given value is null or empty
        /// </summary>
        /// <param name="Value">The value to check</param>
        /// <param name="Message">Optional. The custom message to display if the value is not null or empty.</param>
        public static void IsNullOrEmpty(dynamic Value, string Message = null)
        {

            IsTrue(string.IsNullOrEmpty(Value), Message);

        }


        /// <summary>
        ///     Checks the given value is not null or empty
        /// </summary>
        /// <param name="Value">The value to check</param>
        /// <param name="Message">Optional. The custom message to display if the value is null or empty.</param>
        public static void IsNotNullOrEmpty(dynamic Value, string Message = null)
        {

            IsFalse(string.IsNullOrEmpty(Value));

        }


        /// <summary>
        ///     Ensures the given values are the same
        /// </summary>
        /// <param name="ExpectedObject">The object we want to compare</param>
        /// <param name="ActualObject">The actual object to compare the expected object to</param>
        public static void AreSame(Object ExpectedObject, Object ActualObject)
        {

            Assert.AreSame(ExpectedObject, ActualObject);

        }


        /// <summary>
        ///     Ensures the given values are not the same
        /// </summary>
        /// <param name="NotExpectedObject">The object we want to compare</param>
        /// <param name="ActualObject">The actual object to compare the expected object to</param>
        public static void AreNotSame(Object NotExpectedObject, Object ActualObject)
        {

            Assert.AreNotSame(NotExpectedObject, ActualObject);

        }


        /// <summary>
        ///     Fail the current test
        /// </summary>
        /// <param name="Message">Optional. The custom message to display.</param>
        public static void Fail(string Message = null)
        {

            Assert.Fail(Message);

        }


        /// <summary>
        ///     Throw a warning message
        /// </summary>
        /// <param name="Message">Optional. The custom message to display.</param>
        public static void Warning(string Message)
        {

            Assert.Inconclusive(Message);

        }

    }

}
