Feature: LybraryDatabase
	Feature that will be test next action in the Lybrary database

@TestGetRequest
Scenario: Get data from Library Api against Database data.
	When I make a http Get request to https://localhost:7224/books
	Then I query the database with statement SELECT * FROM dbo.Books
	And compare the results.

@TestPostRequest
Scenario: Post a record throug Library Api and query the database to validate it.
	When I make a http Post request to https://localhost:7224/book response should be not null
	And retrieve the Id inserted with statement SELECT TOP 1 * FROM dbo.Books ORDER BY Id DESC
	Then I query the database to get api inserted data with statement SELECT * FROM dbo.Books WHERE Id = 

@TestPutRequest
Scenario: Put a record throug Library Api and query the database to validate it.	
	When I make a http Put request to https://localhost:7224/book
	Then Data must be updated when query the database with statement SELECT TOP 1 * FROM dbo.Books WHERE Id = 13

@TestDeleteRequest
Scenario: Delete a record throug Library Api and query the database to validate it.
	When I make a http Delete request to https://localhost:7224/book/1015
	Then Record not must be in database when query the database with statement SELECT TOP 1 * FROM dbo.Books WHERE Id = 1015