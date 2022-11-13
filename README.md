# Payment API
The project is a .Net Core Web API application and it is developed using C# .Net 6.0


## About the Application
-> In-memory database used in this application\
-> xUnit used for unit testing


There are three endpoints:\
[HttpGet]  **/payments** : Gets all payments\
[HttpGet]  **/payments/{id}** : Return selected payment object for given payment Id\
[HttpPost] **/payments** : Creates a payment


## How to run
* With using Docker, open the API project in file explorer (\IcePayment) and run the command below: 

> docker-compose up 

* With using Visual Studio, IcePayment.API solution can be started directly
![SS1](https://user-images.githubusercontent.com/9204813/148570589-92263ad9-60b3-402f-8e34-347c7a31fe62.JPG)


## How to test API methods
To test API methods quickly, IcePayment_Api_Collection.json file can be imported into Postman 

![image](https://user-images.githubusercontent.com/9204813/151472808-5d9ab97c-6e6b-4022-a765-2e69f255f026.png)

   
