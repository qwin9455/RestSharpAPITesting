
using ReqRes.Requests;
using System;

namespace ReqRes
{
    public class RegisterRequest : IJsonRequest
    {
        public string email { get; set; }
        public string password { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public long age { get; set; }

        public RegisterRequest(string email, string password, string firstname, string lastname, long age)
        {
            this.email = email;
            this.password = password;
            this.firstname = firstname;
            this.lastname = lastname;
            this.age = age;
        }

        public RegisterRequest()
        {
        }

        public RegisterRequest CreateRandomAccount()
        {
            Random randomGenerator = new Random();
            int randomInt = randomGenerator.Next(0, 100);

            this.email = "testemail" + randomInt + "@mail.com";
            this.password = "random_password_" + randomInt;
            this.firstname = "MyName" + randomInt;
            this.lastname = "MySurname" + randomInt;
            this.age = randomInt;

            return this;
           
        }

    }
}
