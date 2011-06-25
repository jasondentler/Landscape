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
namespace Restbucks.Menu
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.6.1.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("Add customizations to products")]
    public partial class AddCustomizationsToProductsFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "AddCustomization.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Add customizations to products", "In order to market to non-conformists\r\nAs a Restbucks franchise owner\r\nI want to " +
                    "offer customizations to products", GenerationTargetLanguage.CSharp, ((string[])(null)));
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
        [NUnit.Framework.DescriptionAttribute("Add drink sizes")]
        [NUnit.Framework.CategoryAttribute("domain")]
        public virtual void AddDrinkSizes()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Add drink sizes", new string[] {
                        "domain"});
#line 7
this.ScenarioSetup(scenarioInfo);
#line 8
 testRunner.Given("I have added coffee to the menu");
#line 9
 testRunner.When("I add coffee sizes");
#line 10
 testRunner.Then("coffee sizes are added");
#line 11
 testRunner.And("nothing else happens");
#line hidden
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Add a customization twice")]
        [NUnit.Framework.CategoryAttribute("domain")]
        public virtual void AddACustomizationTwice()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Add a customization twice", new string[] {
                        "domain"});
#line 14
this.ScenarioSetup(scenarioInfo);
#line 15
 testRunner.Given("I have added coffee to the menu");
#line 16
 testRunner.And("I have added coffee sizes");
#line 17
 testRunner.When("I add coffee sizes");
#line 18
 testRunner.Then("the aggregate state is invalid");
#line 19
 testRunner.And("the error is \"Coffee already has a Size customization.\"");
#line hidden
            testRunner.CollectScenarioErrors();
        }
    }
}
#endregion