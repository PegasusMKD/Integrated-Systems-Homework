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
 - [ ] Test migrations - *In Progress*
 - [ ] Configure Identity
 - [ ] Implement Swagger
 - [ ] Start testing endpoints through Swagger
 - [ ] Implement views
 - [ ] Test/Experiment with controllers and views to see how well it works
 - [ ] Add error checking and handling
      - For example trying to create an object when passing in a GUID, or updating one without a GUID
 - [ ] Implement required 3rd-party functionality
      - Stripe payment
      - Excel import/export
      - Invoice PDF generation
      - Sending an e-mail


## Requirements

Make a web application for ticket sales using Onion Architecture. The application should have the following functionalities:

### Authentication System

 - [ ] Register (by default on register every user has role "User")
 - [ ] Make the following roles available:
   - Administrator
   - User
 - [ ] Page for managing users and updating roles
 
 ### Services

 #### Tickets
 - [ ] CRUD operations for tickets (and related entities)
 - [ ] View to see all available tickets, and in the view, users can filter the tickets by date
 
 #### Cart
 - [ ] Adding tickets to a Cart (both roles should be allowed to do this)
 
 #### Orders
 - [ ] Creating an Order based on the user Cart.
 - [ ] User has to pay for the Order.
 - [ ] After creating an Order, send an e-mail to the user to confirm that the Order was created.
 - [ ] Let the user view all of their previous Orders.
 - [ ] For each Order a user can create an invoice as a PDF document.

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
