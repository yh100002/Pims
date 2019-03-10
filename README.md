### Improved
- Re-naming modules and re-locate some sources
- Applied swagger for each three services
- Changed Angular UI 
- Added some simple unit tests

### Answer to feedback comments (Many thanks)
- Liked the Docker usage, really good!
>>> I always start with the project and when I design it for even small project like this assignment, it is easy to configure and upgrade and deploy. Of course, there are many technical tools like Kubernetes, Helm for convenient management of bunch of containers.
 
- The code is not badly written but the separation goes over the top and confusing. I think the candidate tried to do much more than it was requested, started separating and over optimising and breaking down the system and overdoing it in the end without actually nothing working on my machine.
- In the end I cannot have the UI working (which was not asked for). I have an error when trying to run the UI.
- The Interface and Implementation separation is good. Some quirks in the coding style but nothing that makes it break for me.
>>> My intention was to show the explicit concept of cqrs using message bus. There should be various ways for worker cosumer like windows service, normal process or like this seperate web service. Of course, aims are all same but indeed looks complex.
- There is no endpoint documentation.
>>> I added swagger documentation for those three backed service.
- contradicts himself on the README file vs the code…says that controllers should not have logic and then he implements the controller with logic inside.
- tries to use and separate Model View and Controller but then mixes the controller with logic instead of separating it in a service implementation that the controller invokes instead.
- tries to do some good separation in components but then violates the separation by having some functionalities logic implemented in the controller but others routed to the services downstream which are actually API’s with controllers and no actual service class containing it. Some consistency is lacking.
>>> I admit I was in rush and lazy a bit.:) 
- There is some inconsistency in naming, Web module has product upload logic implemented. If he followed the good separation he would have created another service just for the upload or at least put the upload functionality inside the Command.Api project in the Product Controller (or in a new controller).
- Why not use an out of the box Web API router project instead of building your own?
- WebCommon module naming is a bit off… it does not implement any functionality regarding Web but instead Parsing of CSV files and Upload. 
- Overall naming is a bit off…. Does not make sense to me to name modules Query and Command. Naming should reflect functionality, the name of the pattern architecture is not the functionality itself.
- The upload functionality will take a lot of time and will consume a lot of memory because he is putting the whole csv rows in memory and holding the Web API execution while it consumes the file. Would be better off using the rabbit MQ to put each line in and process them in the background by another process that read from it. 
>>> Of course, I know but once you start importing, you can move to Products menu and you can see the list of product increasingly.
- I see events being put in the rabbit MQ but where are they being consumed? Inside the SPA/FrontEnd application? I can’t easily follow the code for the frontend because is angular generated and not meant to be analysed in this scope.
>>> I use Angular for front-end development because it's very clear than other spa stacks.

### Features
- Import Product Data
- Export Seclected Product Data To Excel File
- CQRS Architectural Pattern With RabbitMQ
- API Versioning for api gateway
- Docker Deploy 
- Pagination


### Applied Architect (CQRS)
![image](https://drive.google.com/uc?export=view&id=1tnToN4C3DzzjWAOVI4WA26qM-fVWi-FO)


### Technical Points

- .NET Core & Visual Studio Code
>This is sort of technical trends. 
>Even though they have a bit incomvenience for debugging, unit test and so on, those are light and cheap. 
>This gives us and company a good oppotunity to have cost effective projects. 
>Furthermore technically, these are actually pretty fancy and rapidly glowing. 
>For example, you as a .Net developer can make some server modules running on IoT device based on Linux.

- Structure Of Module & Projects
>It was divided into as many as possible according to the role of each module. 
>In my experiences through several projects, seperation of concerns is the most important factor for maintenance with many different developers. Especially, I think controllers should not contain many logic in itself.

- Unit Test On Backend
>To be honest, I just didn't make unit test cases for API module with like moq and xunit, since I wanted to submit this assignmet asap and more focus on architect itself.
>I am unit test believer and so I always create interfaces and injections except dtos.
>I believe every project should cover Sonar Quebe coverage 75%.

- Ms SQL Linux
>I assumed that we all prefer to use linux than windows for back-end.

- RabbitMQ
>I decided to implement CQRS architect so in order to synchonization between command and query layers we need message bus for event process and RabbitMQ is just one of good message bus based on AMQP protocol.

- Docker
>As a backend guy, I love Docker and Kubernetes.

- Angular 7
>One of fancy SPA front-end stacks

- Data
>I didn't implement to import 'Categories', since I wanted to put more focused on architect itself in limited time.


### Read Me
- Prerequisite
> - .Net Core 2.2.103
> - Docker 18.09.1
> - NodeJs 10.15.1
> - Npm 6.4.1
> - Angular CLI 7.2.3 

- How To Build
> On Angular project, run npm -i to install all dependencies.
> After that, you will be able to build. (run ng serve)

- How To Run
> - Make sure the docker service running on your machine 
> - After above, Run the docker image that is in the root in branch following steps
>> - docker-compose up 'on the same folder of branch root'
>> - ms sql server and rabbimq and db deployment will be downloaded and run.
> - After running Docker image, you can run 'dotnet run' for each microservices.
>> - Run 'Web' which is a kind of API gateway
>> - Run 'Command.Api' and 'Query.Api'
>> - Run 'SPA' (run 'ng serve' and open 'http://localhost:4200')
> - Thats it!

- How To Use
> You have run three services as backends.
> - ApiGateway : https://localhost:5001 (by using dotnet run)
> - ProductWriter.Api : https://localhost:5002(by using dotnet run)
> - ProductReader.Api : https://localhost:5003(by using dotnet run)
> - PimFront-End: http://localhost:4200(by using ng serve)
> - RabbitMQ Management : http://localhost:15672
>> Above all, you have import product data and of course it will take long time so after triggering import data, you go to Product List page and see the data list.

###End
