using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReqRes;
using ReqRes.Api;
using ReqRes.Responses;
using RestSharp;
using System;
using System.Net;

namespace RestSharpAPITesting
{
    [TestClass]
    public class APITests
    {
        [TestMethod]
        public void Register_ShouldAbleToRegister_WhenEmailDoesNotExist()
        {
            RegisterRequest request = new RegisterRequest().CreateRandomAccount();

            ResponseObject<RegisterResponse> resp = HttpConnection<RegisterResponse>.Post("register", request);

            Assert.AreEqual(HttpStatusCode.Created, resp.HttpCode);
            Assert.AreEqual(request.email, resp.GetObject.user.email);
            Assert.AreEqual(request.firstname, resp.GetObject.user.firstname);
            Assert.AreEqual(request.lastname, resp.GetObject.user.lastname);
            Assert.AreEqual(request.age, resp.GetObject.user.age);
        }

        [TestMethod]
        public void Register_ShouldNotAbleToRegister_WhenEmailAlreadyExist()
        {
            RegisterRequest request = new RegisterRequest("test6@mail.com", "MykaPassword123", "Myka", "Test", 33);

            ResponseObject<RegisterResponse> resp = HttpConnection<RegisterResponse>.Post("register", request);

            Assert.AreEqual(HttpStatusCode.BadRequest, resp.HttpCode);
            Assert.IsTrue(resp.Content.Contains("Email already exists"));
        }

        [TestMethod]
        public void Register_ShouldNotAbleToRegister_WhenEmailFormatIsIncorrect()
        {
            RegisterRequest request = new RegisterRequest("test6", "MykaPassword123", "Myka", "Test", 33);

            ResponseObject<RegisterResponse> resp = HttpConnection<RegisterResponse>.Post("register", request);

            Assert.AreEqual(HttpStatusCode.BadRequest, resp.HttpCode);
            Assert.IsTrue(resp.Content.Contains("Email format is invalid"));
        }

        [TestMethod]
        public void Login_ShouldAbleToLogin_WhenAccountExist()
        {
            LoginRequest request = new LoginRequest("test6@mail.com", "MykaPassword123");

            ResponseObject<LoginResponse> resp = HttpConnection<LoginResponse>.Post("login", request);

            Assert.AreEqual(HttpStatusCode.OK, resp.HttpCode);
            Assert.AreEqual(request.email, resp.GetObject.user.email);
            Assert.IsNotNull(resp.GetObject.accessToken);
        }

        [TestMethod]
        public void Login_ShouldNotAbleToLogin_WhenAccountDoesNotExist()
        {
            LoginRequest request = new LoginRequest("randomemail@mail.com", "MykaPassword123");

            ResponseObject<LoginResponse> resp = HttpConnection<LoginResponse>.Post("login", request);

            Assert.AreEqual(HttpStatusCode.BadRequest, resp.HttpCode);
        }

        [TestMethod]
        public void RegisterLogin_ShouldAbleToLogin_WhenNewlyRegistered()
        {
            RegisterRequest registerRequest = new RegisterRequest().CreateRandomAccount();
            LoginRequest loginRequest = new LoginRequest(registerRequest.email, registerRequest.password);

            ResponseObject<RegisterResponse> resp = HttpConnection<RegisterResponse>.Post("register", registerRequest);

            Assert.AreEqual(HttpStatusCode.Created, resp.HttpCode);

            ResponseObject<LoginResponse> resp2 = HttpConnection<LoginResponse>.Post("login", loginRequest);

            Assert.AreEqual(HttpStatusCode.OK, resp2.HttpCode);
            Assert.AreEqual(registerRequest.firstname, resp2.GetObject.user.firstname);
            Assert.AreEqual(registerRequest.lastname, resp2.GetObject.user.lastname);
        }

        [TestMethod]
        public void User_ShouldAbleToReadResource_WhenAnAccountIsLoggedIn()
        {
            LoginRequest request = new LoginRequest("test6@mail.com", "MykaPassword123");

            ResponseObject<LoginResponse> resp = HttpConnection<LoginResponse>.Post("login", request);
            
            Assert.AreEqual(HttpStatusCode.OK, resp.HttpCode);

            ResponseObject<ReadUserResponse> resp2 = HttpConnection<ReadUserResponse>.Get("440/users/1", resp.GetObject.accessToken);

            Assert.AreEqual(HttpStatusCode.OK, resp2.HttpCode);
            Assert.AreEqual("olivier@mail.com", resp2.GetObject.email);
        }
    }
}
