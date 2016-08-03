# WikiSynonyms
Leveraging WikiPedia as a source for generation of synonyms beyond just typical english words.

This is an example of how to leverage the WikiPedia data to create a set of synonyms.  Unlike traditional Thesauruses, this dataset allows for the creations of synonyms over words such as microsoft, bill gates, ISS, apartment, sauna, enzyme or toronto making it a really good solution to use in Full Text Search applications where you want to match what the user is typing to relevant synonyms in the content.

## Console Application

The application included under \src allows you to provide a word which is then sent to a SQLite database to return matched synonyms.  The usage for the application is as follows:

> WikiSynonym.exe sauna

where the output will be: 

aufguss, 
dry sauna, 
saun, 
sauna culture, 
sudatory, 
sweatbath, 
swedish bath, 
theremae

In addition, you can pass multi-word terms in double quotes such as: 

>WikiSynonym.exe "Bill Gates"

which will return results such as:

bil gates, 
bill gate, 
bill gates, 
bill gates (microsoft), 
bill gates iii, 
bill gates wealth index, 
billgatesiii, 
gate bill, 
gates & co., 
gates, bill, 
gates, william henry, iii, 
iii gates william henry, 
mr. gates, 
phoebe adele gates, 
sir bill gates, 
sir william henry gates iii, 
william gates iv, 
william gates, iii, 
william h gates, 
william h gates iii, 
william h. gates iii, 
william h. gates, iii, 
william henry gates, 
william henry gates 3, 
william henry gates iii, 
william henry gates iv, 
william henry, iii gates, 
willy gates

## Data Set
Within the \data directory you will find a tab separated file that contains all of the WikiPedia extracted synonyms.  This can be useful if you want to place this content in a different data store.

