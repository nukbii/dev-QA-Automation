Feature: Basic Web Tests
	As a user who blogs on the website
	I want to be able to see the all the recent blogs 
	And the other pages

@mytag
Scenario: Show that the recent blogs are displayed
	Given I am on the hompage
	Then the recent blogs section is displayed


Scenario Outline: Show that the ABOUT, SERVICES and WORK pages are showing their titles correctly
	Given I am on the <Page Name> page
	Then the page name <Page Name> is displayed

Examples: 
| Page Name |
| About     |
| Services  |
| Work      |

Scenario: Show the total number offices
	Given I am on the Contact Us page
	Then the number offices is shown in the output display
