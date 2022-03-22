Feature: Amazon
	Follow next step to complete tasks in https://www.amazon.com.mx/

@OneProductTest
Scenario: Add to cart the lowest price product
	Given I navigate to amazon web site in Mexico
	When I search for a product Bose Headphones
	And I click on search button
	And I select sort by from lowest to top price product
	And I select the first product in the list
	When I click on Add to cart button
	And I go to the cart
	Then I verify the product selected is added in the cart

@MultipleProductsTest
Scenario Outline: Add to cart the lowest price for three diffent products
	Given I navigate to amazon web site in Mexico
	When I search for a product <product>
	And I click on search button
	And I select sort by from lowest to top price product
	And I select the first product in the list
	When I click on Add to cart button
	And I go to the cart
	Then I verify the product selected is added in the cart
Examples:
	| product           |
	| Alien Bluray disc |
	| Smartphone        |
	| Iron Maiden funko |