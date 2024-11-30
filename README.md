
# Stock trader microservices

Possible program flows:

    1.The pricing microservice send messages to Azure Service Bus topic
      while also saving a local copy of the price entity in PostgreSQL database

    2.The order microservice subscribes to the topic that contains the pricing message and 
      creates a copy of the price for it's own use

    3.The portfolio microservice subscribes to the topic that contains the pricing
      message and creates a copy of the price for it's own use

    4.A person hits the API gateway with a POST request which is then routed the order
      microservice (supports message and HTTP communication). The microservice
      saves the order and then send a message to the portfolio microservice.

    5.The portfolio microservice consumes the message from the order service and 
      creates a portfolio for the user with his number of stocks and their price.

      5a.Whether the operation fails or succeeds the order status is updated asynchronously
      by having a message sent back to the order service asynchronously
      
    6.The user can hit the API gateway with a GET request which is then routed to
      the portfolio microservice to return the user's portfolio information.

    7.The user can hit the API gateway with a GET request which is then routed to
    the order microservice to return the order information.
      


## How to run
    0.Make sure you have .NET8
    1.Clone the repo
    2.Make sure you have Docker installed on your machine
    3.Open the project in VisualStudio 2022
    3.Add an Azure Service Bus connection string in the local.settings.json file for each project
      or preferrably do it via user secrets
    4.From the launch settings choose "Launch all"
    
    NB! If you don't have PostgreSQL docker image downloaded - the first run might take longer.

    The Program.cs file of each microservice contains logic for creating a Docker container
    for each of the microservices' PostgreSQL databases.

    It also contains logic for running their respective migrations.
    
## FAQ

#### The pricing microservice doesn't run when launched with the "Launch all" option

From the launch settings choose to run just the pricing microservice
Once you see it running - you can again use the "Launch all" option

#### Error - "There is no Functions runtime available that matches the version project specified by the project"

Go to Visual Studio 2022 -> Tools -> Options -> Projects & Solutions -> Azure functions and click on the "Check for updates" button.
Then select "Download & Install"

## Supported HTTP requests

#### Placing an order

POST localhost:5003/api/order
{
    "personId": 1,
    "ticker": "AAPL",
    "quantity": 1,
    "orderAction": 1
}

#### Fetching person portfolio
GET localhost:5003/api/person/{personId}/portfolio

#### Fetching order status (orderId is returned from the API after placing the order)
GET localhost:5003/api/order/{orderId} 

![alt text](https://github.com/donchodonev/stock-trader/blob/main/architecture%20diagram.drawio.png?raw=true)
