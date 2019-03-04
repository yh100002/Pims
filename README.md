
###Features
- Import Product Data
- Export Seclected Product Data To Excel File
- CQRS Architectural Pattern With RabbitMQ
- API Versioning for api gateway
- Docker Deploy 
- Pagination

### Applied Architect (CQRS)
![Image of CQRS](https://www.codeproject.com/KB/architecture/555855/CQRS.jpg)
* Images from codeproject

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
> - Pims SPA Site: http://localhost:4200
> - RabbitMQ Management : http://localhost:15672
>> Above all, you have import product data and of course it will take long time so after triggering import data, you go to Product List page and see the data list.

###End
