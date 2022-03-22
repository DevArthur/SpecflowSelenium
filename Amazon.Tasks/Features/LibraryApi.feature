Feature: LibraryApi
	Feature that will be test a Restfull API about a library

@LibraryApi
Scenario Outline: Library Restfull API
	When I make a http request <request> to <url>
	Then I get a response that looks like <request> <statusCode>	
Examples:
	| request | url                              | statusCode |
	| GET     | https://localhost:7224/books     | 200        |
	| POST    | https://localhost:7224/book      | 201        |
	| PUT     | https://localhost:7224/book      | 200        |
	| DELETE  | https://localhost:7224/book/1005 | 200        |
	