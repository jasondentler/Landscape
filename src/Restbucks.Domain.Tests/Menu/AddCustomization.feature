Feature: Add customizations to products
	In order to market to non-conformists
	As a Restbucks franchise owner
	I want to offer customizations to products

@domain
Scenario: Add drink sizes
	Given I have added coffee to the menu
	When I add coffee sizes 
	Then coffee sizes are added
	And nothing else happens

@domain
Scenario: Add a customization twice
	Given I have added coffee to the menu
	And I have added coffee sizes
	When I add coffee sizes 
	Then the aggregate state is invalid 
	And the error is "Coffee already has a Size customization."
