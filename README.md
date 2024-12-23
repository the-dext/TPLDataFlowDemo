# TPLDataFlowDemo
A small example of using TPL dataflow to process a pipeline of work.

The project loads a list of organisations from a csv files.
The example csv provided contains 100,000 records. 
But you can download a file containing upto 2 million records from 
https://www.datablist.com/learn/csv/download-sample-csv-files#organizations-dataset

Once downloaded rename the file to organisations.csv and replace the one in the solution (in the infrastructure folder).


## What does the project do?
Each organisation is read in from the csv file line by line and put into the workflow input buffer where it waits to be procesed.

The first stage of the pipeline filters out companies that are not in a list of `"Cosmetics", "plastics", "Research Industry"`

Organisations that pass this stage will then be checked for a number of rules within the second stage.
The rules are
	`Founded after 1985,`
	`Not within the United States of America`
	`Greater than 5,000 Employees`

The final stage will output the selected organisations and they will be will be printed to the console.