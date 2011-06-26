Feature: Cancel order
	In order to punish the barista for being slow
	As a rude customer
	I want to cancel my coffee order and stomp off in a fit

@domain
Scenario: Cancel a placed order
	Given the franchise owner has set up the menu
	And I have placed an order
	When I cancel the order
	Then the order is cancelled
	And nothing else happens

@domain
Scenario: Cancel an unplaced order
	Given the franchise owner has set up the menu
	And I have created an order
	When I cancel the order
	Then the order is cancelled 
	And nothing else happens

@domain
Scenario: Cancel a cancelled order
	Given the franchise owner has set up the menu
	And I have created and cancelled an order
	When I cancel the order 
	Then nothing happens

@domain
Scenario: Change location of a cancelled order
	Given the franchise owner has set up the menu
	And I have created and cancelled an order
	When I change the order location to take away
	Then the aggregate state is invalid
	And the error is "You can't change the location of a cancelled order."


