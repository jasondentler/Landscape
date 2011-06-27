Feature: Change order location
	In order to make it to work on time
	As a coffee addict
	I want to change my coffee order to go

@domain
Scenario: Change order location
	Given the franchise owner has set up the menu
	And I have created a cart
	And I have added a medium cappuccino, skim milk, single shot
	And I have placed the order "for here"
	When I change the order location to take away
	Then the order location is changed from in shop to take away 
	And nothing else happens

@domain
Scenario: Can't change location on an unplaced order
	Given the franchise owner has set up the menu
	And I have created a cart
	And I have added a medium cappuccino, skim milk, single shot
	When I change the order location to take away
	Then the aggregate state is invalid
	And the error is "You can't change the location before you place the order."

@domain
Scenario: Changing location to the same location does nothing
	Given the franchise owner has set up the menu
	And I have created a cart
	And I have added a medium cappuccino, skim milk, single shot
	And I have placed the order "for here"
	When I change the order location to in shop
	Then nothing happens

@domain
Scenario: Change location of a paid order
	Given the franchise owner has set up the menu
	And I have created a cart
	And I have added a medium cappuccino, skim milk, single shot
	And I have placed the order "for here"
	And I have paid for the order
	When I change the order location to take away
	Then the order location is changed from in shop to take away
	And nothing else happens