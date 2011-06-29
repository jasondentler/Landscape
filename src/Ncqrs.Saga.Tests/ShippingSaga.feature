Feature: Shipping Saga
	In order to ship orders to customers
	As a shipping manager
	I want to be told when an order is safe to ship

@saga
Scenario: Dont ship an unpaid order
	When the shipment is prepared
	Then nothing happens

@saga
Scenario: Dont ship an unprepared order
	When the invoice is paid
	Then nothing happens

@saga
Scenario: Pay, then prepare
	Given the shipment is prepared
	When the invoice is paid
	Then ship the order

@saga
Scenario: Prepare, then pay
	Given the invoice is paid
	When the shipment is prepared
	Then ship the order

@saga
Scenario: Ship, then paid
	Given the invoice is paid
	And the shipment is prepared
	When the invoice is paid
	Then nothing happens

@saga
Scenario: Ship, then prepared
	Given the invoice is paid
	And the shipment is prepared
	When the shipment is prepared
	Then nothing happens

