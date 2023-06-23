# Integrated Systems Homework

## To Dos
 - [X] Create the required and complementary entities/data models
 - [X] Configure relationships
 - [ ] Implement base repository interfaces - *In Progress*
 - [ ] Configure application context - *In Progress*
 - [ ] Implement repositories
 - [ ] Create migrations
 - [ ] Configure Identity
 - [ ] Implement the services
 - [ ] Implement controllers and views


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
    E-mail          | Password  |     Role
    ==========================================
    admin@gmail.com | test123   | Administrator
    user@gmail.com  | test123   |     User
 ```


## Notes

No restrictions in terms of the database design, we have full freedom as long as we meet said requirements.

**Deadline: 10.07.2023, 23:59h**
