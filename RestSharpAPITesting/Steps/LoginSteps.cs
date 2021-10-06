using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReqRes;
using ReqRes.Api;
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
    public sealed class LoginSteps
    {

        private readonly ScenarioContext _scenarioContext;

        public LoginSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"login using the email '(.*)' and password '(.*)'")]
        public void GivenLoginUsingTheEmailAndPassword(string email, string password)
        {
            _scenarioContext.Add("loginRequest", new LoginRequest(email, password));
        }

        [When(@"login using registered account")]
        public void WhenLoginUsingRegisteredAccount()
        {
            RegisterRequest request = _scenarioContext.Get<RegisterRequest>("registerRequest");
            _scenarioContext.Add("loginRequest", new LoginRequest(request.email, request.password));
        }

        [When(@"send login request")]
        public void WhenSendLoginRequest()
        {
            LoginRequest request = _scenarioContext.Get<LoginRequest>("loginRequest");
            ResponseObject<LoginResponse> response = HttpConnection<LoginResponse>.Post("login", request);

            _scenarioContext.Add("loginResponse", response);
        }

        [Then(@"the login is successful")]
        public void ThenTheLoginIsSuccessful()
        {
            LoginRequest request = _scenarioContext.Get<LoginRequest>("loginRequest");
            ResponseObject<LoginResponse> response = _scenarioContext.Get<ResponseObject<LoginResponse>>("loginResponse");

            Assert.AreEqual(HttpStatusCode.OK, response.HttpCode);
            Assert.AreEqual(request.email, response.GetObject.user.email);
            Assert.IsNotNull(response.GetObject.accessToken);
        }

        [Then(@"the login is unsuccessful")]
        public void ThenTheLoginIsUnsuccessful()
        {
            ResponseObject<LoginResponse> response = _scenarioContext.Get<ResponseObject<LoginResponse>>("loginResponse");
            Assert.AreEqual(HttpStatusCode.BadRequest, response.HttpCode);
        }

    }
}
