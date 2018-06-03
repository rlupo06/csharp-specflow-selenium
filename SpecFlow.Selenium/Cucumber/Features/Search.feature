Feature: Search
        As a User,
        Google searching should work on all platforms

@C001 @smokeTest @regression
Scenario: Google Search - appium
    When I search for "appium"
    And select "Appium" in the search results
    Then I am presented with the "http://appium.io/" homepage    