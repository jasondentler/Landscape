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


