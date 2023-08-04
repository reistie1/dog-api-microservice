# Project Information
This is the main API entry point. The project configures the following services
	- mediaitR & pipeline behaviours, 
	- autofac for most of the application services used, 
	- admin authorization
	- authentication
	- options for split feature flags and jwt tokens
	- logging via Serilog
	- HTTP client for Dog Api
	- Identity database context
    - Swagger API documentation
	- configuration for the application in various environments
It also contains DTO's for sending back data models to the end user, all Api controllers part of the application which follow the vertical slice architecture that contain the mediatR command, handlers, validators and a dedicated service for performing the requested operation and returning the data.
Also contained in this project is the authorization handler to validate that a user is in an Admin role and has access to the various resources.

# Additional Information
Within the main program file the Identity database context creates the database if it does not already exist and seeds the basic roles and users into the database. The following accounts can be used to access the API endpoints. 

# Note
ListAllBreeds requires an Admin account to access the resource.

# Admin Account
Email: kyle_admin@mailsac.com
Password: Welcome1!

# User Account
Email: bob_smith@mailsac.com
Password: Welcome1!

# Docker
This project contains a docker file to build the project into a Linux alpine instance and uses Docker compose to build the project and all required projects into an image along side a seq image for structured logging and a mssql database
