Feature: Cancel order
	In order to punish the barista for being slow
	As an obnoxious customer
	I want to cancel my coffee order and stomp off in a fit

@domain
Scenario: Cancel a placed order
	Given the franchise owner has set up the menu
	And I have placed an order
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
Scenario: Can't cancel a paid order
	Given the franchise owner has set up the menu
	And I have placed an order
	And I have paid for the order
	When I cancel the order
	Then the aggregate state is invalid
	And the error is "You can't cancel this order. You've already paid for it."
