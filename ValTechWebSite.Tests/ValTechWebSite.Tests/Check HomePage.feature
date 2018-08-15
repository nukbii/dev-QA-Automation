Feature: Check HomePage
	In order to check that I am have the latest information
	As a user
	I want to be able to see the latest news

@mytag
Scenario: Show that the latest news is displayed
	Given I am on the hompage
	Then the latest news section is displayed
