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
    public sealed class RegisterSteps
    {
        private readonly ScenarioContext _scenarioContext;
        public RegisterSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"account is not yet registered")]
        public void GivenAccountIsNotYetRegistered()
        {
            _scenarioContext.Add("registerRequest", new RegisterRequest().CreateRandomAccount());
        }

        [Given(@"we use the email '(.*)'")]
        public void GivenWeUseTheEmail(string email)
        {
            _scenarioContext.Add("registerRequest", new RegisterRequest(email, "Any Password", "Any Name", "Any Surname", 33));
        }

        [When(@"send register request")]
        public void WhenSendRegisterRequest()
        {
            RegisterRequest request = _scenarioContext.Get<RegisterRequest>("registerRequest");
            ResponseObject<RegisterResponse> response = HttpConnection<RegisterResponse>.Post("register", request);

            _scenarioContext.Add("registerResponse", response);
        }

        [Then(@"the registration is successful")]
        public void ThenTheRegistrationIsSuccessful()
        {
            RegisterRequest request = _scenarioContext.Get<RegisterRequest>("registerRequest");
            ResponseObject<RegisterResponse> response = _scenarioContext.Get<ResponseObject<RegisterResponse>>("registerResponse");

            Assert.AreEqual(HttpStatusCode.Created, response.HttpCode);
            Assert.AreEqual(request.email, response.GetObject.user.email);
            Assert.AreEqual(request.firstname, response.GetObject.user.firstname);
            Assert.AreEqual(request.lastname, response.GetObject.user.lastname);
            Assert.AreEqual(request.age, response.GetObject.user.age);
        }

        [Then(@"the registration is unsuccessful")]
        public void ThenTheRegistrationIsUnsuccessful()
        {
            ResponseObject<RegisterResponse> response = _scenarioContext.Get<ResponseObject<RegisterResponse>>("registerResponse");
            Assert.AreEqual(HttpStatusCode.BadRequest, response.HttpCode);
        }

        [Then(@"error message is '(.*)'")]
        public void ThenErrorMessageIs(string errorMessage)
        {
            ResponseObject<RegisterResponse> response = _scenarioContext.Get<ResponseObject<RegisterResponse>>("registerResponse");
            Assert.IsTrue(response.Content.Contains(errorMessage));
        }

    }
}
