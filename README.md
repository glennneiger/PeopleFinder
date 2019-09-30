# PeopleFinder API
## Requirements
• Provide a REST endpoint that takes a search input then returns a list of people whose first or last name matches what the input (including at least name, address, age, and interests).<br/>
• Seed the application with some users.<br/>
• Provide a REST endpoint that allows addition of new users.<br/>
• Provide documentation to interact with the REST endpoints.<br/>
• Use Entity Framework Code First to talk to the database.<br/>
• Add automated testing for appropriate parts of the application.
#### Extra Credit
• Write PowerShell scripts to add or search for particular users.<br/>
• Simulate latency in the API call.

## Assumptions
I retrieved the latest version of Visual Studio Community Edition, which was 2019 version 16.3 at the time of this writing, and wrote this solution with that version of Visual Studio. I believe that .NET Core 3.0 requires some form of VS 2019. It is also assumed that you would have access to NuGet and therefore able to retrieve/update/restore the packages listed in the Technical Architecture section below.

## Technical Architecture
From a technical solution standpoint, the following frameworks/technologies were used to accomplish the proposed requirements and extra credit.

• .NET Core 3.0<br/>
• C#<br/>
• WebAPI 2<br/>
• Entity Framework (Code First)<br/>
• SQLite<br/>
• MSTest<br/>
• Moq<br/>
• Swashbuckle<br/>

## Solution Description
To meet the proposed requirements, I first created a database with a Persons table using EF Code First via the PeopleFinder.Console project. This Persons table contains integer ID, first name, last name, address, age, and interests fields. With the database setup, I then created the core PeopleFinder project - first implementing a "person" service layer with the following functions.

• SelectAll - returning the full list of results within the Persons table<br/>
• SelectByID - returning a singular person based on input ID<br/>
• Search - returning a list of results where first name or last name matches the search input<br/>
• Add - adding a new person to the Persons table

With the service layer created (both concrete and abstract), I then created the person controller with REST endpoints corresponding with each service function listed above. I also am injecting the person service into the controller's constructor for purposes of modularity and unit testing. I then implemented Swashbuckle (Swagger UI) to serve as self-maintained REST documentation (which is also the initial launch page upon running the API).

With the API project in place, I then created a MSTest project with test methods written to cover all controller and service functions. For the controller tests, I'm mocking the associated service methods using Moq to isolate testing the controller. For the service tests, I'm using SQLite in-line memory testing for SelectAll, SelectByID, and Search. I'm mocking the person context to test the Add service method.

For the Extra Credit portion, I wrote the following PowerShell scripts.

• add_data.ps1 - this script prompts the user for input to create a new "person" and add it to the Persons table<br/>
• search_data.ps1 - this script prompts the user for search criteria and returns a list of persons whose first or last name matches the input criteria

I also added a delay filter within the PeopleFinder API project. This allowed me to simulate latency (based on a defined delay variable) to all REST endpoints. I implemented this filter within the ConfigureServices method.
