Feature: Queue order to barista
	In order to make drinks for the customers
	As a barista
	I want orders to be queued up

@domain
Scenario: Queue an order to the barista
	Given the franchise owner has set up the menu
	When I queue an order for the barista
	Then the order is queued for the barista
	And nothing else happens

