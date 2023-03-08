Hierarchy project
This is an online catalog of company employees that displays a hierarchy of employees using Tree Grid, supports CRUD operations, and includes an authorization system.

Technologies used:
? .NET 6.0
? MySQL 
? ASP.NET CORE 6
? SyncFusion Library
? Bogus random data generator

Completed technical tasks:
Created View that displays the hierarchy of employees using Tree Grid.The number of hierarchy levels depends only on the number of positions in the company, the artificially created and filled database will contain seven levels by default


Optional tasks and their implementation:
1. The database was created using Entity Framework Migrations.

2. Bogus Random Generator is used to fill the database.

3. The Bootswatch "Lux" theme was used for the design.

4, 5, 6. The employee page displays comprehensive information about each employee, including their photo. The page includes filtering, sorting, and searching functionality without having to reload the page.

7,8. The authorization system is implemented using Microsoft Identity, blocking and hiding the CRUD functionality from unauthorized users.

9,10. A CRUD Functionality has been created with the ability to edit all employee data, including his photo.

11. The employee boss changeallows employees to be reassigned to onther chiefs. However, there are some limitations to the implementation, as it is possible to change the boss to anyone, regardless of their position. When a boss is removed, their subordinates are randomly redistributed among other bosses of the same position.

12. Lazy loading is implemented to handle a large number of employees.

13. Drag-n-Drop functionality was not implemented due to insufficient understanding of Javascript.