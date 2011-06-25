// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:1.6.1.0
//      SpecFlow Generator Version:1.6.0.0
//      Runtime Version:4.0.30319.431
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
namespace Restbucks.Ordering
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.6.1.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("Place an order")]
    public partial class PlaceAnOrderFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "PlaceAnOrder.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Place an order", "In order to avoid a murderous rampage\r\nAs a coffee addict\r\nI want to place my ord" +
                    "er for coffee", GenerationTargetLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.TestFixtureTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Place the order")]
        [NUnit.Framework.CategoryAttribute("domain")]
        public virtual void PlaceTheOrder()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Place the order", new string[] {
                        "domain"});
#line 7
this.ScenarioSetup(scenarioInfo);
#line 8
 testRunner.Given("the franchise owner has set up the menu");
#line 9
 testRunner.And("I have created an order");
#line 10
 testRunner.And("I have added a medium cappuccino, skim milk, single shot");
#line 11
 testRunner.When("I place the order for take away");
#line 12
 testRunner.Then("the order is placed for take away");
#line 13
 testRunner.And("nothing else happens");
#line hidden
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Place an empty order")]
        [NUnit.Framework.CategoryAttribute("domain")]
        public virtual void PlaceAnEmptyOrder()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Place an empty order", new string[] {
                        "domain"});
#line 16
this.ScenarioSetup(scenarioInfo);
#line 17
 testRunner.Given("the franchise owner has set up the menu");
#line 18
 testRunner.And("I have created an order");
#line 19
 testRunner.When("I place the order for take away");
#line 20
 testRunner.Then("the aggregate state is invalid");
#line 21
 testRunner.And("the error is \"You can\'t place an empty order. Add an item.\"");
#line hidden
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Place an already-placed order")]
        [NUnit.Framework.CategoryAttribute("domain")]
        public virtual void PlaceAnAlready_PlacedOrder()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Place an already-placed order", new string[] {
                        "domain"});
#line 24
this.ScenarioSetup(scenarioInfo);
#line 25
 testRunner.Given("the franchise owner has set up the menu");
#line 26
 testRunner.Given("I have placed an order");
#line 27
 testRunner.When("I place the order for take away");
#line 28
 testRunner.Then("nothing happens");
#line hidden
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Place a cancelled order")]
        [NUnit.Framework.CategoryAttribute("domain")]
        public virtual void PlaceACancelledOrder()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Place a cancelled order", new string[] {
                        "domain"});
#line 31
this.ScenarioSetup(scenarioInfo);
#line 32
 testRunner.Given("the franchise owner has set up the menu");
#line 33
 testRunner.And("I have created and cancelled an order");
#line 34
 testRunner.When("I place the order for take away");
#line 35
 testRunner.Then("the aggregate state is invalid");
#line 36
 testRunner.And("the error is \"This order is cancelled. Create a new order.\"");
#line hidden
            testRunner.CollectScenarioErrors();
        }
    }
}
#endregion
