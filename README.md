# Brunch Louisville
## C# Console App
### This is a project for [Code Louisville](https://www.codelouisville.org) classes.
#### This console app is a small demo of the weekend Brunch and Buffet spots in Louisville KY.
#### Please note, this is work in progress.

### Directions on how to use app
	* Type 'Help' to see all commands.
	* Type 'Print All' to print all the restaurant data out.
	* Type 'Pull Reviews' to select one specific restaurant's reviews.
	* Type 'Exit' to exit.

### Required NuGet libraries
	* Newtonsoft.Json

### App Highlights
	1. I created my own sets of data.  There are two .csv files, restaurants.csv and Reviews.csv
	2. Both sets of data have their correlating classes, Restaurants.cs and Reviews.cs
		* They both have another class that is a List of restaurants and reviews, respectively.
	3. Within the command, 'Pull Reviews', the data for the specific restaurant is written to a Json file using a StreamWriter.
	4. This project has comments in the form of Intellisense.