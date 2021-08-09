# Monster Hunter Shop

### How to Install and run the application

#### Step 1) Opening the solution

If you have installed or cloned this project from the repository. 

Open up Visual Studio (I used Visual Studio 2019) ensuring that you have the **Data storage and processing** toolset installed with your Visual Studio as seen in the image below

![](/Images/VisualStudioToolset.jpg)

If that is completed then you should see in your solution explorer something similar to this

![](/Images/SolutionExplorerContents.jpg)

If you do not have the solution explorer open the under **View** on the top toolbar there will be an option called Solution Explorer

![](/Images/SolutionExplorerLocation.jpg)

#### Step 2) Creating the database

The next step to running the application is to create the database

Within the repo folder that you have cloned there is a folder named **SQL Queries**

![](/Images/SQLQueriesLocation.jpg)

Open the file in that folder into Visual Studio and you will see something similar to this

![](/Images/SQLQueryContents.jpg)



Execute the SQL queries (the default shortcut is **Ctrl+Shift+E**), if it is your first time running the SQL query which is very likely then you will be asked to connect to a server for this demonstration purpose I will connect my Database to a local server as shown in this image

![](/Images/ConnectingToDatabase.jpg)

After connecting and attempting to execute the queries again if it all succeeds then opening your SQL Server Object Explorer (which is under the same **View** toolbar see [this](/Images/SQLServerExplorer.jpg) if you are unsure) 

Your SQL Server Object Explorer should now have a MonsterHunter database with 4 tables of which the Hunters Table should have 11 rows of data and the Products Table should have 104 rows of data.

![](/Images/DatabaseLocation.jpg)

#### Step 3) Connecting the Entity Framework to the database

We now have the Database created and the Entity framework (ShopData project) retrieved from the repo. All that is left is to connect the MonsterHunterContext to the database.

Start off by opening the MonsterHunterContext.cs file and at line 29 within the if statement, replace the "Data Source=[connection string]" with the connection string of your newly formed database.

![](/Images/DataSourceStringLocation.jpg)

To find the connection string of your database, first in your SQL Server Object Explorer.

Expand the MonsterHunter database tree and then **Right Click >> Properties** and then on the right-hand side a new window will popup called Properties, within that window you want to find a field called **Connection String** once you have found that copy and paste the Connection String into the MonsterHunterContext.cs file.

![](/Images/ConnectionString.jpg)

#### Step 4) Running the application

Once that is all completed, switch the start up project to ShopGUI on the top toolbar.

![](/Images/ActiveProject.jpg)

Then all that's left to do is click the Run button that is directly next to it.

If all is working then this lovely window will appear in the centre of your screen.

![](/Images/MainWindow.jpg)

You have now successfully installed and opened my application. You are now free to play around with it.

**!! Note !!**: Running a new instance of the application and pressing Enter will take some time as it has to load all of the data form the database, after the initial loads then the application will run smoother. 

