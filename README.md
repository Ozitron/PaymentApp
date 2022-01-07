# IcePayment Payment API
This project is a technical assignment that is created for IcePay. The project is a .Net Core Web API application and it is developed using C# .Net 6.0

## How to run
-> Open api project (\IcePayment) and run: 
    docker-compose up 
-> Or on Visual Studio, IcePayment.API solution can be started directly

## How to use API methods
IcePayment_Api_Collection.json file can import to Postman

## About the Application
-> In-memory database used in this application
-> There are three endpoints:
   ~/Payment/Add : Creates a payment
   ~/Payment/GetAll : Gets all payments
   ~/Payment/GetById/{Id} : Return selected payment object for given payment Id
   
