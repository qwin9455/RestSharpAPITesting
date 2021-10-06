Feature: Register
	Register to Json Server

@smoke
Scenario: Successful registration to json server
	Given account is not yet registered
	When send register request
	Then the registration is successful

@smoke
Scenario: Unsuccessful registration of an account that already exist
	Given we use the email 'test6@mail.com'
	When send register request
	Then the registration is unsuccessful
	And error message is 'Email already exists'

@smoke
Scenario: Unsuccessful registration of an invalid email address
	Given we use the email 'invalidemail'
	When send register request
	Then the registration is unsuccessful
	And error message is 'Email is invalid'