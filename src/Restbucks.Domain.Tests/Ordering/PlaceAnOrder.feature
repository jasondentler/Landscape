Feature: Place an order
	In order to avoid a murderous rampage
	As a coffee addict
	I want to order coffee

@domain
Scenario: Add a cappucino to a new order
	Given the franchise owner has set up the menu
	When I add a medium capuccino, skim milk, single shot
	Then the order is created
	And a medium capuccino, skim milk, single shot is added to the order
	And nothing else happens

@domain
Scenario: Add another item to an order
	Given the franchise owner has set up the menu
	And I have started an order 
	And I have added a medium capuccino, skim milk, single shot
	When I add a large hot chocolate, skim milk, no whipped cream
	Then a large hot chocolate, skim milk, no whipped cream is added to the order
	And nothing else happens

@domain
Scenario: Place the order 
	Given the franchise owner has set up the menu
	And I have started an order
	And I have added a medium capuccino, skim milk, single shot
	When I place the order for take away
	Then the order is placed for take away 
	And nothing else happens

