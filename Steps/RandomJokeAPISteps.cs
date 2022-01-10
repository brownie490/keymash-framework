using Newtonsoft.Json;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Infrastructure;

namespace Automation
{
    [Binding]
    public class RandomJokeAPISteps : BasePage
    {

        private readonly ScenarioContext _scenarioContext;
        private readonly ISpecFlowOutputHelper _specFlowOutputHelper;

        public RandomJokeAPISteps(ScenarioContext scenarioContext, ISpecFlowOutputHelper outputHelper)
        {

            _scenarioContext = scenarioContext;
            _specFlowOutputHelper = outputHelper;

        }


        [When(@"I post a Random Joke request")]
        public void WhenIPostARandomJokeRequest()
        {

            string Response = RandomJoke.GetRandomJoke();

            Check.IsFalse(string.IsNullOrEmpty(Response), "The Response was null or empty.");
            _scenarioContext["Response"] = Response;

        }
        

        [Then(@"the API returns a success response")]
        public void ThenTheAPIReturnsASuccessResponse()
        {

            RandomJoke.Response response = JsonConvert.DeserializeObject<RandomJoke.Response>(_scenarioContext["Response"].ToString());
            Check.AreEqual("success", response.type);

        }
        

        [Then(@"returns a Random Joke")]
        public void ThenReturnsARandomJoke()
        {

            RandomJoke.Response response = JsonConvert.DeserializeObject<RandomJoke.Response>(_scenarioContext["Response"].ToString());
            Check.IsNotNullOrEmpty(response.value.joke);
            Report.OutputJson((string)_scenarioContext["Response"], _scenarioContext, _specFlowOutputHelper);

        }

    }

}
