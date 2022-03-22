Feature: DummyApi
	Test dummy api from https://jsonplaceholder.typicode.com/posts

@DummyApiScenario
Scenario: Test dummy API with http request
	When I perform a http request <url> <httpRequestType>
	Then I receive a response that looks like <statusCode> <httpRequestType>
Examples:
	| httpRequestType | statusCode | url                                          |
	| GET             | -          | https://jsonplaceholder.typicode.com/posts/1 |
	| POST            | Created    | https://jsonplaceholder.typicode.com/posts/1 |
	| PUT             | OK         | https://jsonplaceholder.typicode.com/posts   |
	| PATCH           | OK         | https://jsonplaceholder.typicode.com/posts   |
	| DELETE          | OK         | https://jsonplaceholder.typicode.com/posts/1 |
