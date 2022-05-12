1. Download the project zip file named as: TaskManagement.zip from the download link provided in the email.

2. Open project in Visual Studio 2022 as this projct is using .Net 6 and Visual Studio 2022 has support for .Net 6.

3. Open Package Manager Console from Tools->NuGet Package Manager->Package Manager Console.

4. Run the command: Update-Datebase
 in Package Manager Console.

5. Then run the project. It will open the swagger UI. Swagger UI will have Post and Put endpoints.

6. For unit testing, Right click on TaskControllerTestcs.cs from TaskManagement.Test projct in Solution Exporer and click Run Tests. 

7. You will see the tests results in a separate window.

8. Demo data has already been inserted in Azure SQL Server Database. To test the Task Requirments from given document Project.docx, please visit this url: https://taskmanagemenet.azurewebsites.net/swagger/index.html

9. To update the Task, you will need to copy UUID from database and paste in Id field of Put endpoint.

10. To access the database on SQL Server, please use below credentials:

Server: tcp:tasksmanagementserver.database.windows.net,1433
Username: admintasksmanagement
Password: Database@123 

11. Database script is also being attached to the email.
