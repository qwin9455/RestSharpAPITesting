using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReqRes;
using ReqRes.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using TechTalk.SpecFlow;

namespace RestSharpAPITesting.Steps
{
    [Binding]
    public sealed class ReadUserSteps
    {
        private readonly ScenarioContext _scenarioContext;

        public ReadUserSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [When(@"send get user '(.*)' request")]
        public void WhenSendGetUserRequest(int user)
        {
            ResponseObject<LoginResponse> loginResponse = _scenarioContext.Get<ResponseObject<LoginResponse>>("loginResponse");
            ResponseObject<ReadUserResponse> readResponse = HttpConnection<ReadUserResponse>.Get("440/users/" + user, loginResponse.GetObject.accessToken);

            _scenarioContext.Add("readResponse", readResponse);
        }

        [Then(@"the get user is successful")]
        public void ThenTheGetUserIsSuccessful()
        {
            ResponseObject<ReadUserResponse> readResponse = _scenarioContext.Get<ResponseObject<ReadUserResponse>>("readResponse");

            Assert.AreEqual(HttpStatusCode.OK, readResponse.HttpCode);
        }

    }
}
