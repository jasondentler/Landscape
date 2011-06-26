Feature: Pay for the order
	In order to avoid criminal charges
	As a coffee addict
	I want to pay for my coffee order

@domain
Scenario: Pay for my order
	Given the franchise owner has set up the menu
	And I have placed an order
	When I pay with a credit card
	Then the order is paid for 
	And nothing else happens

