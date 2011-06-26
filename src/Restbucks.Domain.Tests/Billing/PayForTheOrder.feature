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

@domain
Scenario: Can't pay for a paid order
	Given the franchise owner has set up the menu
	And I have placed an order
	And I have paid for the order
	When I pay with a credit card
	Then the aggregate state is invalid
	And the error is "This order is already paid for. Have a nice day."

@domain
Scenario: Pay the wrong amount 
	Given the franchise owner has set up the menu
	And I have placed an order
	When I pay the wrong amount
	Then the aggregate state is invalid
	And the error is "Incorrect amount. Your order total is $6.70."

