# FlowerChainAPI

For the database we use MySQL. You need to create the database manually.
Launch MAMP/XAMPP/something else and create a database called “FlowerChainAPI”. Make sure to set your port to 8889.

When you’ve created your database, enter “dotnet ef database update” in the terminal. Data will be added.

To make changes in the database, launch swagger by typing “dotnet run” in the terminal.
CTRL + left click one of the localhosts to launch swagger in your browser.

# To Do

- algemeen overzicht welke boeketten het beste verkopen
- overzicht welk boeket het beste verkoopt per winkel
- overzicht van welke winkels het meeste omzet draaien
- overzicht van hoe een winkel het doet qua sales met andere winkels in de buurt (zelfde regio)
- unit testing