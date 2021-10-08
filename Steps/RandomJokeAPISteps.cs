using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using TechTalk.SpecFlow;

namespace Automation
{
    [Binding]
    public class RandomJokeAPISteps : BasePage
    {

        private readonly ScenarioContext _scenarioContext;

        public RandomJokeAPISteps(ScenarioContext scenarioContext)
        {

            _scenarioContext = scenarioContext;

        }


        [When(@"I post a Random Joke request")]
        public void WhenIPostARandomJokeRequest()
        {

            string Response = RandomJoke.GetRandomJoke();

            Assert.IsFalse(string.IsNullOrEmpty(Response), "The Response was null or empty.");
            _scenarioContext["Response"] = Response;

        }
        

        [Then(@"the API returns a success response")]
        public void ThenTheAPIReturnsASuccessResponse()
        {


            RandomJoke.Response response = JsonConvert.DeserializeObject<RandomJoke.Response>(_scenarioContext["Response"].ToString());
            Assert.AreEqual("success", response.type);

        }
        

        [Then(@"returns a Random Joke")]
        public void ThenReturnsARandomJoke()
        {

            RandomJoke.Response response = JsonConvert.DeserializeObject<RandomJoke.Response>(_scenarioContext["Response"].ToString());
            Assert.IsNotNull(response.value.joke);
            Report.OutputJson((string)_scenarioContext["Response"], _scenarioContext);

        }

    }

}
