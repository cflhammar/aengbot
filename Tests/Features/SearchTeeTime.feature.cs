﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by Reqnroll (https://www.reqnroll.net/).
//      Reqnroll Version:2.0.0.0
//      Reqnroll Generator Version:2.0.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Tests.Features
{
    using Reqnroll;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Reqnroll", "2.0.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("Search tee times logic tests")]
    [NUnit.Framework.FixtureLifeCycleAttribute(NUnit.Framework.LifeCycle.InstancePerTestCase)]
    public partial class SearchTeeTimesLogicTestsFeature
    {
        
        private global::Reqnroll.ITestRunner testRunner;
        
        private static string[] featureTags = ((string[])(null));
        
        private static global::Reqnroll.FeatureInfo featureInfo = new global::Reqnroll.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Features", "Search tee times logic tests", null, global::Reqnroll.ProgrammingLanguage.CSharp, featureTags);
        
#line 1 "SearchTeeTime.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public static async System.Threading.Tasks.Task FeatureSetupAsync()
        {
        }
        
        [NUnit.Framework.OneTimeTearDownAttribute()]
        public static async System.Threading.Tasks.Task FeatureTearDownAsync()
        {
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public async System.Threading.Tasks.Task TestInitializeAsync()
        {
            testRunner = global::Reqnroll.TestRunnerManager.GetTestRunnerForAssembly(featureHint: featureInfo);
            if (((testRunner.FeatureContext != null) 
                        && (testRunner.FeatureContext.FeatureInfo.Equals(featureInfo) == false)))
            {
                await testRunner.OnFeatureEndAsync();
            }
            if ((testRunner.FeatureContext == null))
            {
                await testRunner.OnFeatureStartAsync(featureInfo);
            }
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public async System.Threading.Tasks.Task TestTearDownAsync()
        {
            await testRunner.OnScenarioEndAsync();
            global::Reqnroll.TestRunnerManager.ReleaseTestRunner(testRunner);
        }
        
        public void ScenarioInitialize(global::Reqnroll.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<NUnit.Framework.TestContext>(NUnit.Framework.TestContext.CurrentContext);
        }
        
        public async System.Threading.Tasks.Task ScenarioStartAsync()
        {
            await testRunner.OnScenarioStartAsync();
        }
        
        public async System.Threading.Tasks.Task ScenarioCleanupAsync()
        {
            await testRunner.CollectScenarioErrorsAsync();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Given a tee time was found")]
        public async System.Threading.Tasks.Task GivenATeeTimeWasFound()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            global::Reqnroll.ScenarioInfo scenarioInfo = new global::Reqnroll.ScenarioInfo("Given a tee time was found", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 3
    this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((global::Reqnroll.TagHelper.ContainsIgnoreTag(scenarioInfo.CombinedTags) || global::Reqnroll.TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                await this.ScenarioStartAsync();
#line 4
        await testRunner.GivenAsync("current date is 2025-01-01", ((string)(null)), ((global::Reqnroll.Table)(null)), "Given ");
#line hidden
                global::Reqnroll.Table table6 = new global::Reqnroll.Table(new string[] {
                            "Id",
                            "Name"});
                table6.AddRow(new string[] {
                            "1",
                            "GolfCourse1"});
#line 5
        await testRunner.GivenAsync("courses exist", ((string)(null)), table6, "Given ");
#line hidden
                global::Reqnroll.Table table7 = new global::Reqnroll.Table(new string[] {
                            "Email",
                            "CourseId",
                            "FromTime",
                            "ToTime",
                            "NumberOfPlayers"});
                table7.AddRow(new string[] {
                            "user@email.com",
                            "1",
                            "2025-02-01T09:00",
                            "2025-02-01T12:00",
                            "2"});
#line 8
        await testRunner.AndAsync("users subscribed to", ((string)(null)), table7, "And ");
#line hidden
                global::Reqnroll.Table table8 = new global::Reqnroll.Table(new string[] {
                            "CourseId",
                            "FromTime",
                            "ToTime",
                            "AvailableSlots",
                            "Status",
                            "Event"});
                table8.AddRow(new string[] {
                            "1",
                            "2025-02-01T10:00",
                            "2025-02-01T10:10",
                            "2",
                            "",
                            ""});
                table8.AddRow(new string[] {
                            "1",
                            "2025-02-01T10:10",
                            "2025-02-01T10:20",
                            "0",
                            "",
                            ""});
                table8.AddRow(new string[] {
                            "1",
                            "2025-02-01T10:20",
                            "2025-02-01T10:30",
                            "4",
                            "Stängd",
                            ""});
                table8.AddRow(new string[] {
                            "1",
                            "2025-02-01T10:30",
                            "2025-02-01T10:40",
                            "4",
                            "",
                            "Tävling"});
                table8.AddRow(new string[] {
                            "1",
                            "2025-02-01T10:40",
                            "2025-02-01T10:50",
                            "4",
                            "",
                            ""});
#line 11
        await testRunner.AndAsync("sweetspot has bookings", ((string)(null)), table8, "And ");
#line hidden
#line 18
        await testRunner.WhenAsync("the service is triggered to search", ((string)(null)), ((global::Reqnroll.Table)(null)), "When ");
#line hidden
                global::Reqnroll.Table table9 = new global::Reqnroll.Table(new string[] {
                            "Email",
                            "CourseName",
                            "TeeTime",
                            "AvailableSlots"});
                table9.AddRow(new string[] {
                            "user@email.com",
                            "GolfCourse1",
                            "2025-02-01T10:00",
                            "2"});
                table9.AddRow(new string[] {
                            "user@email.com",
                            "GolfCourse2",
                            "2025-02-01T10:40",
                            "4"});
#line 19
        await testRunner.ThenAsync("notifications are sent to", ((string)(null)), table9, "Then ");
#line hidden
            }
            await this.ScenarioCleanupAsync();
        }
    }
}
#pragma warning restore
#endregion
