Feature: KeyMash Test Page

	As a User
	I want to practice my Automation skils on a Test Page
	So that I can improve my skils

	@Web @SmokeTest
	Scenario: 1. Complete the form
		Given I'm on the Test Page
		When I complete the form with all the required fields
			And click Submit
		Then page will indicate that the feedback was submitted

	@Web
	Scenario: 2. Select mode of transport
		Given I'm on the Test Page
		When I complete the form with all the required fields
			And select the I have a bike option
			And leave the I have a car option unticked
			And click Submit
		Then page will indicate that the feedback was submitted

	@Web
	Scenario: 3. I am an Audi driver
		Given I'm on the Test Page
		When I complete the form with all the required fields
			And select Audi from the car dropdown
			And click Submit
		Then page will indicate that the feedback was submitted

	@Web
	Scenario: 4. Options displayed when hovering over them
		Given I'm on the Test Page
		When I hover over the hover option
		Then the links will be displayed

	@Web
	Scenario: 5. Required Fields all highlighted when left empty
		Given I'm on the Test Page
		When I do not enter any values into the required fields
			And click Submit
		Then all the required fields will be highlighted

	@Web
	Scenario: 6. Username field required
		Given I'm on the Test Page
		When I do not enter a value in the Username field
			And enter all the remaining required fields
			And click Submit
		Then the Username field will be highlighted

	@Web
	Scenario: 7. Password field required
		Given I'm on the Test Page
		When I do not enter a value in the Password field
			And enter all the remaining required fields
			And click Submit
		Then the Password field will be highlighted

	@Web
	Scenario: 8. Email field required
		Given I'm on the Test Page
		When I do not enter a value in the Email field
			And enter all the remaining required fields
			And click Submit
		Then the Email field will be highlighted

	@Web
	Scenario: 9. Website field required
		Given I'm on the Test Page
		When I do not enter a value in the Website field
			And enter all the remaining required fields
			And click Submit
		Then the Website field will be highlighted

	@Web
	Scenario: 10. Message field required
		Given I'm on the Test Page
		When I do not enter a value in the Message field
			And enter all the remaining required fields
			And click Submit
		Then the Message field will be highlighted