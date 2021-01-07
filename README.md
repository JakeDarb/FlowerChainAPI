# FlowerChainAPI
## Creating the database
### MySQL
For the database we use MySQL. You need to create the database manually.
Launch MAMP/XAMPP/something else and create a database called “FlowerChainAPI”. Make sure to set your port to 8889.

When you’ve created your database, enter “dotnet ef database update” in the terminal. Data will be added.
### MongoDB
We used MongoDB to keep track of our sales. To use MongoDB do the following
-   Download the [MongoDB Community server](https://www.mongodb.com/try/download/community
-   Connect with database using the following information:
    Hostname: localhost
    Port: 27017 (or change the port on line 4 in the "appsettigns.json" file)
    SRV RECORD: off
    Authentication: None

## Usage
### Launch swagger
To make changes in the database, launch swagger by typing “dotnet run” in the terminal.
CTRL + left click one of the localhosts to launch swagger in your browser.
### Creating data
Open a POST funtion and press the "try it out" button.
You then have to complete the template given to you. When you're done, press the "Execute" button to add the data to the database.
### Reading data
Open a GET funcition and press the "try it out" button.
Using the "/FlowerChainAPI/FlowerBouquet/{id}" GET function, you can look up a bouquet using their ID.
When you're done, press the "Execute" button to add the data to the database.
### Updating data
Open the PATCH functiond and press the "try it out" button.
Give the ID of the item you want to change and change the given data fields. When you're done, press the "Execute" button to add the data to the database.
### Deleting data
Open a DELETE funcition and press the "try it out" button.
Enter the ID of the item you want to delete and press the "Execute" button to delete the data from the database.