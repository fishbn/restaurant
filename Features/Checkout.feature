Feature: Checkout

Scenario: 1
	Given a group of 4 people orders 4 starters, 4 mains and 4 drinks
	When the order is sent to the endpoint
	Then the total is calculated correctly in the bill
	And the total number of items ordered is verified to be 4 starters, 4 mains, and 4 drinks

Scenario: 2
	Given a group of 2 people order 1 starter and 2 mains and 2 drinks before 19:00
	When the endpoint is called
	Then the correct total is returned
	And the total number of items ordered by the first 2 people is verified to be 1 starter, 2 mains, and 2 drinks

	Given 2 more people join the group at 20:00 and order 2 mains and 2 drinks
	When the updated order is sent to the endpoint
	Then the total is calculated correctly in the bill
	And the total number of items ordered by the last 2 people is verified to be 2 mains and 2 drinks
	And the correct calculation of the final bill after all orders are combined is verified

Scenario: 3
	Given a group of 4 people order 4 starters, 4 mains and 4 drinks
	When the order is sent to the endpoint
	Then the total is calculated correctly in the bill
	And the total number of items ordered is verified to be 4 starters, 4 mains, and 4 drinks

	Given 1 person cancels their order
	When the updated order is sent to the endpoint
	Then the total is calculated correctly in the final bill
	And the total number of items ordered after 1 person cancels their order is verified to be 3 starters, 3 mains, and 3 drinks.