Feature: Create an order
	In order to avoid a murderous rampage
	As a coffee addict
	I want to order coffee

@domain
Scenario: Add a cappucino to a new order
	Given the franchise owner has set up the menu
	When I add a medium cappuccino, skim milk, single shot
	Then the cart is created
	And a medium cappuccino, skim milk, single shot is added to the cart
	And nothing else happens

@domain
Scenario: Add another item to an order
	Given the franchise owner has set up the menu
	And I have created a cart 
	And I have added a medium cappuccino, skim milk, single shot
	When I add a large hot chocolate, skim milk, no whipped cream
	Then a large hot chocolate, skim milk, no whipped cream is added to the cart
	And nothing else happens

@domain
Scenario: Add a third item to an order
	Given the franchise owner has set up the menu
	And I have created a cart 
	And I have added a medium cappuccino, skim milk, single shot
	And I have added a medium cappuccino, skim milk, single shot
	When I add a large hot chocolate, skim milk, no whipped cream
	Then a large hot chocolate, skim milk, no whipped cream is added to the cart
	And nothing else happens

