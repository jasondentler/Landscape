Feature: Add a menu item
	In order to make my millions off the local caffeine addicts
	As a Restbucks franchise owner
	I want to add items to the menu

@domain 
Scenario: Add a menu item
	When I add coffee to the menu with a price of $7.20
	Then coffee is added to the menu with a price of $7.20
	And coffee is added to the price list with a price of $7.20
	And nothing else happens

@domain
Scenario: New menu items are added to the product service
	When I add coffee to the menu with a price of $7.20
	Then the product catalog has coffee
