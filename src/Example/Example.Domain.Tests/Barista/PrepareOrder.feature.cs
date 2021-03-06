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
namespace Example.Barista
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.6.1.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("Prepare an order")]
    public partial class PrepareAnOrderFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "PrepareOrder.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Prepare an order", "In order to keep my job\r\nAs a barista\r\nI want to prepare drinks for the customers" +
                    "", GenerationTargetLanguage.CSharp, ((string[])(null)));
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
        [NUnit.Framework.DescriptionAttribute("Begin preparing an order")]
        [NUnit.Framework.CategoryAttribute("domain")]
        public virtual void BeginPreparingAnOrder()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Begin preparing an order", new string[] {
                        "domain"});
#line 7
this.ScenarioSetup(scenarioInfo);
#line 8
 testRunner.Given("the manager has set up the menu");
#line 9
 testRunner.And("the cashier has queued an order for a small latte, whole milk");
#line 10
 testRunner.When("I begin preparing the order");
#line 11
 testRunner.Then("the order is being prepared");
#line 12
 testRunner.And("nothing else happens");
#line hidden
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Prepare an order")]
        [NUnit.Framework.CategoryAttribute("domain")]
        public virtual void PrepareAnOrder()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Prepare an order", new string[] {
                        "domain"});
#line 15
this.ScenarioSetup(scenarioInfo);
#line 16
 testRunner.Given("the manager has set up the menu");
#line 17
 testRunner.And("the cashier has queued an order for a small latte, whole milk");
#line 18
 testRunner.And("I have begun preparing the order");
#line 19
 testRunner.When("I prepare the order");
#line 20
 testRunner.Then("the order is prepared");
#line 21
 testRunner.And("nothing else happens");
#line hidden
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Prepare an order without beginning preparation")]
        [NUnit.Framework.CategoryAttribute("domain")]
        public virtual void PrepareAnOrderWithoutBeginningPreparation()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Prepare an order without beginning preparation", new string[] {
                        "domain"});
#line 24
this.ScenarioSetup(scenarioInfo);
#line 25
 testRunner.Given("the manager has set up the menu");
#line 26
 testRunner.And("the cashier has queued an order for a small latte, whole milk");
#line 27
 testRunner.When("I prepare the order");
#line 28
 testRunner.Then("the order is being prepared");
#line 29
 testRunner.And("the order is prepared");
#line 30
 testRunner.And("nothing else happens");
#line hidden
            testRunner.CollectScenarioErrors();
        }
    }
}
#endregion
