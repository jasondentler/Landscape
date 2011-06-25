﻿using System.Linq;
using NUnit.Framework;
using Newtonsoft.Json;
using SharpTestsEx;
using TechTalk.SpecFlow;

namespace Restbucks
{
    [Binding]
    public class Then
    {

        [Then(@"nothing else happens")]
        public void ThenNothingElseHappens()
        {
            var untestedEvents = DomainHelper.GetUntestedEvents();

            var data = untestedEvents.Select(e =>
                                             string.Format("{0}: {1}",
                                                           e.GetType().ToString(),
                                                           JsonConvert.SerializeObject(e)));

            var dataString = string.Join(",\r\n", data);

            var msg = string.Format("The following events weren't checked by the scenario: \r\n{0}",
                                    dataString);

            untestedEvents.Should(msg).Be.Empty();
        }


        [Then(@"the aggregate state is invalid")]
        public void ThenTheAggregateStateIsInvalid()
        {
            var ex = DomainHelper.GetException<InvalidAggregateStateException>();
            ex.Should().Not.Be.Null();
        }

        [Then(@"the error is ""(.*)""")]
        public void ThenTheErrorIs(string message)
        {
            var ex = DomainHelper.GetException();
            var actual = ex.Message;
            actual.Should().Be.EqualTo(message);
        }
    
    }
}