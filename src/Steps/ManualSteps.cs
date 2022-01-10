using TechTalk.SpecFlow;

namespace Automation
{

    [Binding, Scope(Tag ="Manual")]
    public class ManualSteps : BasePage
    {

        [Given(".*"), When(".*"), Then(".*")]
        public void ManualEmptySteps()
        {


        }

    }

}