Feature: Prepare orders
	In order to sell to customers
	As a barista
	I want prepare orders

@domain
Scenario: Begin preparing an order
	Given the franchise owner has set up the menu
	And an order has been queued for the barista
	When I begin preparing the order
	Then the order is being prepared
	And nothing else happens

@domain
Scenario: Begin preparing an order that's already being prepared
	Given the franchise owner has set up the menu
	And an order has been queued for the barista
	And I have started preparing the order
	When I begin preparing the order
	Then the aggregate state is invalid
	And the error is "This order is already being prepared."

@domain
Scenario: Begin preparing an order that's already prepared
	Given the franchise owner has set up the menu
	And an order has been queued for the barista
	And I have started preparing the order
	And I have prepared the order 
	When I begin preparing the order
	Then the aggregate state is invalid
	And the error is "This order is already prepared."

@domain
Scenario: Finish preparing the order
	Given the franchise owner has set up the menu
	And an order has been queued for the barista
	And I have started preparing the order
	When I finish preparing the order
	Then the order is prepared
	And nothing else happens

@domain
Scenario: Finish preparing a queued order
	Given the franchise owner has set up the menu
	And an order has been queued for the barista
	When I finish preparing the order
	Then the aggregate state is invalid
	And the error is "You never started preparing this order."