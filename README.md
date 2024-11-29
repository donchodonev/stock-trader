
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
      creates a portfolio for the user with his number of stocks and their price
      
    6.The user can hit the API gateway with a GET request which is then routed to
      the portfolio microservice to return the user's portfolio information.
      


## How to run
    1.Clone the repo
    2.Make sure you have Docker installed on your machine
    3.Open the project in VisualStudio 2022
    3.Add an Azure Service Bus connection string in the local.settings.json file for each project
    4.From the launch settings choose "Launch all"

    The Program.cs file of each microservice contains logic for creating a Docker container
    for each of the microservices' PostgreSQL databases.

    It also contains logic for running their respective migrations.
    
## FAQ

#### The pricing microservice doesn't run when launched with the "Launch all" option

From the launch settings choose to run just the pricing microservice
Once you see it running - you can again use the "Launch all" option
