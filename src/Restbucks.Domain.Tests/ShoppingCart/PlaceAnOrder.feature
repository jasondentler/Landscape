Feature: Place an order
	In order to avoid a murderous rampage
	As a coffee addict
	I want to place my order for coffee

@domain
Scenario: Place the order 
	Given the franchise owner has set up the menu
	And I have created an order
	And I have added a medium cappuccino, skim milk, single shot
	When I place the order for take away
	Then the order is placed for take away 
	And the placed order has one item
	And the placed order contains a medium cappuccino, skim milk, single shot
	And nothing else happens

@domain
Scenario: Place an empty order
	Given the franchise owner has set up the menu
	And I have created an order
	When I place the order for take away
	Then the aggregate state is invalid
	And the error is "You can't place an empty order. Add an item."

@domain
Scenario: Place an already-placed order
	Given the franchise owner has set up the menu
	Given I have placed an order
	When I place the order for take away
	Then nothing happens

@domain
Scenario: Place a cancelled order
	Given the franchise owner has set up the menu
	And I have created and cancelled an order
	When I place the order for take away
	Then the aggregate state is invalid
	And the error is "This order is cancelled. Create a new order."