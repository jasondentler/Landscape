Feature: Add Product
	In order to make my millions off the local caffeine addicts
	As a Restbucks franchise owner
	I want to add products to the menu

@domain 
Scenario: Add a product
	When I add coffee to the menu with a price of $7.20
	Then coffee is added to the menu
	And the price of coffee is $7.20
	And nothing else happens

