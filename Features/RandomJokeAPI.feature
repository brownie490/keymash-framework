Feature: Random Joke API
	Simple calculator for adding two numbers


Scenario: 1. Get a Random Joke
	When I post a Random Joke request
	Then the API returns a success response
		And returns a Random Joke