Feature: SpecFlowFeature1
		Visit website
		Add 2 brownies to basket
		Add 3 Chips to basket
		Add 1 popcorn to basket 
		Remove Popcorn from Basket 

@SmokeTest
Scenario: Example C# Selenium Script 
		Given I am on AbelAndCole 

		When I search for brownies and add them to basket
		Then I will have brownies in my basket

		When I search for chips and add them to basket
		Then I will have chips in my basket

		When I search for popcorn and add them to basket
		Then I will have popcorn in my basket

		When I remove popcorn from my basket
		Then I will not have popcorn in my basket
