Feature: Login
	Login to Json Server

@smoke
Scenario: Successful login to json server
	Given login using the email 'test6@mail.com' and password 'MykaPassword123'
	When send login request
	Then the login is successful

@smoke
Scenario: Unsuccessful login of a non-existent account
	Given login using the email 'test100@mail.com' and password 'MykaPassword123'
	When send login request
	Then the login is unsuccessful

@smoke
Scenario: Successful login of a newly registered account
	Given account is not yet registered
	When send register request
	And login using registered account
	And send login request
	Then the login is successful