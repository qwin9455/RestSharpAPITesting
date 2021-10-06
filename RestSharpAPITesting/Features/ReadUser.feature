Feature: ReadUser
	Get users from Json Server

@mytag
Scenario: Get a user when an account is logged in
	Given login using the email 'test6@mail.com' and password 'MykaPassword123'
	When send login request
	And send get user '1' request
	Then the get user is successful