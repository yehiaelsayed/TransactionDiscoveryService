
# Transaction Discovery Service




## How To Run 
#####Please add your SQL server database connection string under appsetting.json in this path
```
 ConnectionStrings:DefaultConnection
```
##### Base Url and Ports 

``Run on IISExpress Https : https://localhost:44300/``

``Run on TransactionDiscoveryService Https : https://localhost:5001/``

``Run on TransactionDiscoveryService Http : https://localhost:5000/``


## EndPoints 
#### All end points with request parameters are represented in this file :
`TransactionsDiscovery.postman_collection.json`
* Base URL : `api/TransactionDiscovery/AddAccounts`  
* /AddAccounts
>accecpt list of account Ids and save them in the database then start to get all payments for each account and save them in the databas  
* /History 
>accept account Id and return .CSV transactions history 
