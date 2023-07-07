# Integrated Systems Homework

This repository is a .NET Core homework project for my university project, more specifically for the "Integrated Systems" subject.

## To Dos
General to-do items just to keep track of my work before implementing the full features.

 - [X] Create the required and complementary entities/data models
 - [X] Configure relationships
 - [X] Implement base repository interfaces
 - [X] Configure application context
 - [X] Implement a generic repository
 - [X] Implement DTOs for the services
 - [X] Install AutoMapper
 - [X] Implement & Configure AutoMapper
 - [X] Implement customized repositories
 - [X] Implement the base service functionality (excluding 3rd-party library implementation)
 - [X] Implement controllers
 - [X] Configure application
 - [X] Configure a database (MS SQL)
 - [X] Create migrations
 - [X] Test migrations
 - [X] Configure & Setup Base Identity Users
 - [X] Configure & Setup Auth Functionality with JWT
 - [X] Test existing Identity functionality
 - [X] Implement Swagger
 - [X] Configure Identity roles for each user type
 - [X] Auto-magically fetch user where needed using the token
 - [X] Start testing endpoints through Swagger
 - [X] Implement required 3rd-party functionality - *In Progress*
      - Stripe payment - **Implemented**
      - Excel import/export - **Implemented**
      - Invoice PDF generation - https://ironpdf.com/tutorials/dotnet-core-pdf-generating/ - **Implemented**
      - Sending an e-mail - https://mailtrap.io/blog/asp-net-core-send-email/ - **Implemented**
 - [ ] Implement views - *In Progress*
 - [ ] Test/Experiment with controllers and views to see how well it works - *In Progress*
 - [X] Add error checking and handling **- set to done since we don't care about this**
      - For example trying to create an object when passing in a GUID, or updating one without a GUID

## Bugs

 - [X] No Movie Genre functionality
 - [X] Add "Include" extension to the base GetById and GetAll calls so we can fetch all needed relationships
     - Decided to just implement custom methods in the custom repositories where needed with an include
 - [X] Add custom DTOs for create and update
 - [X] Change DTOs to receive only IDs of the relations
 - [X] Call SaveChanges after creations and updates
 - [X] Check Delete methods for SaveChanges calls
 - [X] Manually set relations through fetch (rather than through DTO mapping) so context can keep track
 - [X] Swap "OrderNumber" to an int instead of string
 - [X] Swap over from "auto-generated" identity guid to "manually" setting guid (using Guid.NewGuid()) and see whether that makes the code behaviour "more predictable"

## Requirements

Make a web application for ticket sales using Onion Architecture. The application should have the following functionalities:

### Authentication System

 - [X] Register (by default on register every user has role "User")
 - [X] Make the following roles available:
   - Administrator
   - User
 - [ ] Page for managing users and updating roles
 
 ### Services

 #### Tickets
 - [X] CRUD operations for tickets (and related entities)
 - [X] View to see all available tickets, and in the view, users can filter the tickets by date
 
 #### Cart
 - [X] Adding tickets to a Cart (both roles should be allowed to do this)
 
 #### Orders
 - [X] Creating an Order based on the user Cart.
 - [X] User has to pay for the Order.
 - [X] After creating an Order, send an e-mail to the user to confirm that the Order was created.
 - [X] Let the user view all of their previous Orders.
 - [X] For each Order a user can create an invoice as a PDF document.

 ### Administrator-specific services

 #### Tickets
 - [ ] Administrators can export all tickets as an Excel document.
 - [ ] When exporting, administrator can filter by Genre.
 
 #### Users
 - [ ] Administrator can import users using an Excel document.
   - Format of excel file:
 ```
         E-mail      | Password  |    Role
    =============================================
     admin@gmail.com | test123   | Administrator
     user@gmail.com  | test123   |    User
 ```


## Notes

No restrictions in terms of the database design, we have full freedom as long as we meet said requirements.

**Deadline: 10.07.2023, 23:59h**
