Feature: DemoTwo

@C002 @regression
Scenario: Google Search - specflow
    Given I am on "http://www.google.com"
    When I search for "specflow"
    And select "SpecFlow" in the search results
    Then I am presented with the "http://specflow.org/" homepage