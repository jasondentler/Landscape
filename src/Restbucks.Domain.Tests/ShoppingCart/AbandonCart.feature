Feature: Abandon shopping cart
	In order to make it to work on time
	As a habitually-late employee
	I want to walk away before placing my order

@domain
Scenario: Abandon shopping cart
	Given the franchise owner has set up the menu
	And I have created a cart
	When I abandon the cart
	Then the cart is abandoned 
	And nothing else happens

@domain
Scenario: Abandon shopping cart after placing order
	Given the franchise owner has set up the menu
	And I have placed an order
	When I abandon the cart
	Then the aggregate state is invalid
	And the error is "You've already placed the order. You can't abandon your shopping cart."

@domain
Scenario: Abandon an already abandoned cart
	Given the franchise owner has set up the menu
	And I have created a cart
	And I have abandoned the cart
	When I abandon the cart
	Then nothing happens
