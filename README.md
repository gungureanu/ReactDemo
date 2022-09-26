# ReactDemo
Basic Web Application built with Microsoft SQL Server, ASP.NET Core (MVC) 6.0, and React (Part 1: Backend) 


## Business Requirements
1.	 Build a basic web application that is uses the following technologies: SQL Server, ASP.NET CORE, C#, with React. 
2.	 Display a webpage where the user can upload Excel spreadsheets with Employee Salary data. 
3.	 Provide file validation to ensure that that the user can only upload Excel files (max. 1 MB in size) and nothing else. 
4.  Ensure that the Excel files only have the following columns: Employee Number, Employee Name, and Employee Salary. 
5.  Upload the files directly within the SQL database so that the files are stored directly into the database. 
6.  Upload the Employee Salary data into the database, where the web app provides a preview of the data that's in the database as well.
7.  Provide documentation that explains what you did and why you choose to do it that way as well. 


## Evaluation Criteria
1.  Build this demo application using the following technologies: SQL Server, ASP.NET CORE, C#, and React. 
2.  This application should be built in accordance with industry best practices. 
3.  When building this application, security concerns should be addressed according to best practices as well. 
4.  Ensure that the Excel files have the correct columns in the correct order as shown above. 


## Project Published Online 
This demo project has been published here: https://www.reactdemo.net 
 
SSL certificates were installed to ensure that security concerns were properly addressed. In addition, a login page was built where a username and password is required to access this web application. Currently, no sign-up option is provided; therefore, you'll have to receive the login credentials from the Administrator. 

Once you have the login credentials, then you may login and to see this demo website in action for yourself. In other words, the source code that's provided here has been fully published on Microsoft Azure so that you can ensure that the business requirements have been fully met. 


## User Interface (UI) Layer
HTML5 is a markup language that was used for this ReactDemo project. HTML5 is normally used for structuring and presenting content on the Internet. For this project, I used the following two HTML files: index.html and dashboard.html. In this case, in order to keep things simple, I used the **index.html** file as the login page and the **dashboard.html** page as the page where all of the main functionality for this ReactDemo project occurs. 

![KHM-3413](https://user-images.githubusercontent.com/13016448/192271023-f36134e1-b6d9-4a93-b469-965abade2156.png)

CSS is an extremely powerful style sheet language which is used to control the look and feel of the content written in HTML, which is what I used in order to build the ReactDemo website. All of my custom style sheet rules have been incorporated within the **custom_style.css** file.  In addition, I'm also using a pre-built style sheet called **animate.css** that allows for smooth transitions of various elements. This is utilized on the **index.html** (login) page so that the login region is animated with a *fade in* effect.  

**NOTE:** jQuery is a fast, small, and feature-rich JavaScript library, which was also used in this ReactDemo project for the scrollbar functionality. 

![KHM-3414](https://user-images.githubusercontent.com/13016448/192270707-72bf0cb8-20f8-4c02-bf1b-1e9f07b79db9.png)

The Excel upload functionality has been implemented on the right-side panel that has been marked with the "Upload File" tab. Click on this tab to activate the Drag and Drop functionality that starts the upload process. This tab also displays all of the previous Excel uploads to the database as well. 


## Database Layer 
Since this project is a small web project, I have decided to utilize the Database-First approach, as opposed to the Code-First approach. Therefore, I have been able to create Microsoft SQL Database called "ReactDemo" and within this database, I created four tables: system_Users, content_Employees, content_Uploads, and system_ErrorsLog. 

### TABLE: system_Users
This table within the database has been created more for security concerns. This way we can provide login functionality so that only authorized users can upload and view the Employee Salary information. 

| Field Name       | Data Type            | Null     |
|------------------|:--------------------:|:--------:|
|UserID            | PK, uniqueidentifier | not null |
|EmailAddress      | nvarchar(100)        | not null |
|FirstName         | nvarchar(50)         | not null |
|LastName          | nvarchar(50)         | not null |
|Password          | nvarchar(100)        | not null |
|TotalLoginNumber  | int                  | not null |
|LastLoginAttempts | tinyint              | not null |
|Active            | bit                  | not null |
|CreatedDate       | datetime             | not null |
|ModifiedDate      | datetime             | not null |
|CreatedBy         | uniqueidentifier     | not null |
|LastUpdatedBy     | uniqueidentifier     | not null |

This table also tracks the total logins for auditing (security concerns). In addition, it also tracks the login attempts as well. This too is more for security concerns as well. However, while we capture this information, due to the nature of this small project, I may not fully finish implementing these features for now. However, at least this project is setup in such a matter where this can be implemented at a later time. 

I have also added an Active field so that a user account can easily be deactivated without deleting the user record. This is an industry best practice so I did include this field within this table as well. In addition, please notice that I also included the standard four fields for auditing and security concerns as well: CreatedDate, ModifiedDate, CreatedBy, and LastUpdatedBy. 

### TABLE: content_Employees
According to the business requirements, there are three different data elements that needs to be collected: Employee Number, Employee Name, and Employee Salary. Since having a Primary Key is a good industry best practice, I decided to use the Employee Number data element as the Primary Key as well. Especially since the Employee Number is generally a unique number within most organizations anyway. 

| Field Name       | Data Type            | Null     |
|------------------|:--------------------:|:--------:|
|EmployeeNumber    | PK, bigint           | not null |
|EmployeeName      | nvarchar(150)        | not null |
|EmployeeSalary    | decimal(18,2)        | not null | 
|CreatedDate       | datetime             | not null |
|ModifiedDate      | datetime             | not null |
|CreatedBy         | uniqueidentifier     | not null |
|LastUpdatedBy     | uniqueidentifier     | not null |

Therefore, within the "system_Employee" table I am utilizing the "EmployeeNumber" field as the Primary Key with a data type of bigint, which should provide us with unique numbers up to 9,223,372,036,854,775,807. Generally, it's a good practice to use bigint over int when the field is also a primary key. Of course, in a perfect case scenario, I would use the data type of "uniqueidentifier" for the primary key. However, because this is a small web application and because it will be inserting data from Excel files, I felt that the most reasonable way of doing this in this case is to use the Employee Number field as the unique identifier value. This way we keep this effort down to reasonable level. 

In addition, I have also created the following fields more for auditing and security concerns: CreatedDate, ModifiedDate, CreatedBy, and LastUpdatedBy. 

### TABLE: content_Uploads
In accordance with business requirements, this table will allow for the uploading and storing of all the Excel files that are uploaded within this application. 

| Field Name       | Data Type            | Null     |
|------------------|:--------------------:|:--------:|
|UploadID          | PK, uniqueidentifier | not null |
|UserID            | FK, uniqueidentifier | not null |
|FileName          | nvarchar(250)        | not null | 
|FileStorage       | varbinary(max)       | not null |
|CreatedDate       | datetime             | not null |
|ModifiedDate      | datetime             | not null |
|CreatedBy         | uniqueidentifier     | not null |
|LastUpdatedBy     | uniqueidentifier     | not null |

The varbinary data type will allow for storing of data within the database of Excel files that are unlimited in size. However, we will limit the upload size to 1 MB from the C# code itself. This table also tracks what user uploaded these Excel files to the database as well. 

I could have also created two additional fields: FileType and FileDateTime. However, in order to keep the scope of this effort to a minimum, I've decided not to collect this type of data for this project at this time.  

### TABLE: system_ErrorsLog
In accordance with industry best practices, this table will allow for the logging of errors throughout this application. 

| Field Name       | Data Type            | Null     |
|------------------|:--------------------:|:--------:|
|ErrorLogID        | PK, int              | not null |
|UserID            | FK, uniqueidentifier | not null |
|ErrorMode         | nvarchar(50)         | null     | 
|ErrorCode         | nvarchar(50)         | null     | 
|ErrorPage         | nvarchar(500)        | null     | 
|MethodName        | nvarchar(500)        | null     | 
|ErrorMessage      | nvarchar(max)        | null     | 
|Description       | nvarchar(max)        | null     | 
|CreatedDate       | datetime             | not null |

When something goes wrong and error is caught, then it will be reported upon within this table. The global error handler within .NET CORE 6.0 is used to catch all errors and remove the need for duplicated error handling code throughout the .NET API.  It's configured as middleware in the configure HTTP request pipeline section of the **program.cs** file. 


## MVC Design Pattern 
Model-View-Controller (MVC) is a software architectural pattern that is commonly used to separate an application into three main logical components: the model, the view, and the controller. Each of these components are built to handle specific development aspects of this application. MVC is one of the most frequently used industry-standard web development framework to create scalable and extensible projects. 

Since this is a leading-edge best practice within the industry right now, I've decided to utilize this design pattern within this application. Because of this, I have implemented **MVC** with ASP.NET CORE version 6.0. 

### Dependency Injection 
In addition, I have also implemented **Dependency Injection** and **Inversion of Control** to ensure that this code is utilizing the loose coupling design pattern, which is another industry best practice. Now, loose coupling is achieved by means of a design that promotes single-responsibility and separation of concerns (SOC). A loosely coupled class can be consumed and tested independently of other (concrete) classes. 

Now, on the other hand, tight coupling means classes and objects are dependent on one another. In general, tight coupling is usually not good because it reduces the flexibility and re-usability of the code while loose coupling means reducing the dependencies of a class that uses the different class directly. Loose coupling also means reducing dependencies of a class that uses a different class directly. 

Interfaces are a powerful tool to use for decoupling. Classes can communicate through interfaces rather than other concrete classes, and any class can be on the other end of that communication simply by implementing the interface, which is what I have done throughout my code. Here are some of the main interfaces that I have implemented within the code: 

1. IAccountService
2. ICryptographyService
3. IUtility
4. IFileUpload

### Inversion of Control 
Inversion of Control (IoC) is a design principle (although, some people refer to it as a pattern). As the name suggests, it is used to invert different kinds of controls in object-oriented design to achieve loose coupling. IoC is all about inverting the control. For example, let’s say that class A calls some method that’s within class B. So, class A cannot complete its task without class B and so you can say that Class A is dependent on class B or that class B is a dependency of class A. 

In the object-oriented design approach, classes need to interact with each other in order to complete one or more functionalities of any application. However, the IoC principle suggests inverting the control. This means to delegate the control to another class. In other words, invert the dependency creation control from class A to another class, such as a Factory class. So that way class A would use a Factory class to get an object of class B. Thus, we have inverted the dependent object creation from class A to the Factory class. Class A no longer creates an object of class B, instead it uses the factory class to get the object of class B. 


## Web API 
ASP.NET Core provides many improvements over ASP.NET MVC/Web API. First of all, it's now one framework and not two, which I really like because it is convenient and there is less confusion. Secondly, we now have logging and Dependency Injection (DI) containers without any additional libraries, which saves me time and allows me to concentrate on writing better code instead of choosing and analyzing the best libraries. 

### Query Processors
A query processor is an approach when all business logic relating to one entity of the system is encapsulated in one service and any access or actions with this entity are performed through this service. If necessary, a query processor includes CRUD (create, read, update, delete) methods for this entity. Usually, for each method, a separate query class is created, and in simple cases, it is possible (but not desirable) to reuse the query class. 

### API Layers
The diagram below shows that this system will have these four layers: 

![image](https://user-images.githubusercontent.com/13016448/192302143-b126275d-b517-495b-96c2-0509c02e0726.png)

1. Database Layer - this is where the data is stored (datastore).
 
2. Data Access Layer (DAL) - to access the data, I used the Unit of Work pattern and in the implementation, I used the ORM EF Core with a database first approach. 
 
3. Business logic - to encapsulate the business logic, I used query processors, only this layer processes business logic. 
 
4. REST API - the actual interface through which clients can work with our API will be implemented through ASP.NET Core. Route configurations are determined by attributes. 

In addition to the described layers, we have several important concepts. The first is the separation of data models. The client data model is mainly used in the REST API layer. It converts queries to domain models and vice versa from a domain model to a client data model, but query models can also be used in query processors. The conversion is done using AutoMapper. 

### Swagger
Swagger allows me to describe the structure of this project's APIs so that machines can read them. The ability of APIs to describe their own structure is the root of all awesomeness of Swagger. Why is it so great? Well, by reading my API's structure, we can automatically build beautiful and interactive API documentation. 

![image](https://user-images.githubusercontent.com/13016448/192302991-9165afcf-b98d-43b4-a18f-4f8ab25800d6.png)

The most common way to keep track of a signed in user in a web application is to use cookies. The normal flow is: the user clicks login, goes to a login page and after entering valid credentials the response that is sent to the user's browser contains a Set-Cookie header that contains encrypted information. Every time a cookie is set for a domain, on every subsequent request for that domain the browser will include the cookie automatically. On the server that cookie is decrypted, and its contents are used to create the user's identity. 

However, for this project instead of using a cookie, I used a token. Now, a token also represents the user, but when we use it, we don't rely on the browser's built-in mechanism to deal with cookies. We have to explicitly ask for a token, store it ourselves, and then manually send it with every request. 

The token format that I'm talking about here is JWT, which stands for JSON Web Token. A JWT token has the following format: 

**base64-encoded-header.base64-encoded-payload.signature.**

The signature is created by taking: “base64(header).base64(payload)” and cryptographically signing it using the algorithm specified in the header. For example, HMAC-SHA256. The signing part involves using a secret that is only known at the server.

It is important to be aware that the information contained in the JWT is not encrypted. To get the payload you just need to base64-decode it. You can even do that from your developer tools console (for example in Chrome). Use the atob method and pass the payload as an argument. You’ll see that you’ll get JSON back.

The signature guarantees that if someone tries to replace the payload, the token becomes invalid. For someone to be successful in replacing the payload and producing a valid token they would need to know the secret used in the signature, and that secret never goes to the client. This is something to have in mind when you decide what to put in the payload.

### Using JWT in ASP.NET Core
To use JWT in ASP.NET Core you need to know how to manually create JWT tokens, how to validate them and how to create an endpoint so that the client app can request them. The issuer represents the entity that generates the tokens, in this case it will be our ASP.NET Core web application. The audience is who the tokens are intended to, i.e. the client application. This way, you can validate both the issuer and the audience when you validate the token.

The notBefore and expires define after what point in time the token can be used and until when it is valid, respectively. Finally, in the signingCredentials I specified which security key and what algorithm I used to create the signature. 

After creating an instance of JwtSecurityToken, the way to actually generate the token is to call the WriteToken method of an instance of JwtSecurityTokenHandler and pass the JwtSecurityToken as a parameter:

**string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);**

The Web API client can be a desktop app, mobile or even a browser. First, to sign in you need to send a POST request to “/token” with a username and password. If all goes well with the POST you get the JWT back and you can save it somewhere, usually in local storage in the case of a web application. 

Finally, to logout you just need to remove the token from local storage:

**localStorage.removeItem("token")**

One thing to be aware of is that if the client performs an action that requires the user to be authenticated and there’s no (valid) Authorization header in the request, the server will respond to that request with a response with status code 401. 

**NOTE:** Due to time constrants, I was not able to fully implement JWT tokens as I originally planned. 

## The SDLC Methodology
The Software Development Life Cycle (SDLC) is a structured process that enables the production of high-quality, low-cost software, in the shortest possible production time. Now, I am very familiar with other methodologies such as Agile, Agile Scrum, or even Waterfall. However, for this small project I thought it best that I use the SDLC methodology, or a hybrid version of the SDLC to ensure that I am able to deliver this project in such a manner that it not only meets the established business requirements, but in some ways that it also exceeds the initial expectations and demands. For these reasons, this is the methodology (and processes) of how I chose to approach this project: 

**1. Requirements** - I was able to capture the business requirements from the customer upon our initial conversation. These business requirements are also documented within this document above. However, I did notice that the customer did leave some information out of the business requirements, which I assume that his intentions were for me to determine how to fill in these gaps as part of this challenge. Now, I've done my best to fill in these gaps in as reasonably as possible. Could I have done more? Yes, absolutely but then this project's scope could have grown way out of control. 

Instead, I have made every effort to limit things to what I felt was a "reasonable effort" given the short timeframe that I was given for this project. In the real world, I would have gotten clarification to the business requirements. In addition, a developer could always add additional features and functionality. But for the sake of this undertaking, let's just say that this is Version 1.0 of this ReactDemo project. 

**2. Design** - After I captured the business requirements, then I worked on the User Interface (UI) design first. I used Brackets 2.0, an open-source web development text editor which was made for web and front-end developers. Brackets may not be a visual drag and drop HTML and CSS webpage builder, but then then again this is why my HTML and CSS stylesheets don't have a bunch of junk code in there either. 

Anyway, after I finished establishing the two pages for this project: the index.html (login page) and the dashboard.html page. Then I worked on architecting the database structure for this project. For this I used SQL Server Management Studio, which is an integrated environment for managing any SQL infrastructure. This is when I created and established the ReactDemo database and the four tables that were needed for this effort: system_Users, content_Employees, content_Uploads, and system_ErrorsLog. 

**3. Development** - After I was done with the UI Design and the Database Architecture, then I started working on the ASP.NET CORE project within Visual Studio.NET 2022. It was at this point that I utilized the MVC design pattern with Dependency Injection and Inversion of Control (loC). I also setup the Web API services at this point as well. Once I reached this point, I paused my development efforts and implemented the https://www.reactdemo.net website on my Microsoft Azure account. 

This is when I purchased the reactdemo.net domain name through my GoDaddy.com account and two basic SSL certificates so that I can apply them to the https://www.reactdemo.net (or reactdemo.net) and to the https://api.reactdemo.net sub-domain as well. Since this project was on the fast track, I went ahead and did a preliminary publish so I can verify that my API's were in good working order. Once this was verified then I went back to the UI Interface where I worked on integrating the React code to the API's to ensure that the interface was also in good working order as well. 

**NOTE:** In an actual organization, I would have established three separate environments: development, testing, and production. However, in order to keep this effort down to a minimum, I have established only one environment which I have decided to use for development, testing, and production purposes.  

**4. Implementation** - Due to the nature of this fast-track project, I already started the implementation phase of this effort within the last "development" phase. However, within most development efforts with actual production environments, I would normally have verified that my project works within a development and testing environment and then I would work with other groups to establish an implementation effort to the actual production environment. At this point, I would write up and provide any required documentation as well. I would also establish a failover plan as well. 

**5. Verification** - Once my first release was completed, then I went through a verification and testing phase where I went through the application and documented any bugs that needed to be addressed. Under normal circumstances, I would utilize a Test-Driven Development (TDD) approach directly within the code and then I would put together Test Cases and Test Scripts as well. 

Nevertheless, due to the short-term nature of this project, I did my best to address any bugs that I have come across to ensure that this application works within reasonable parameters for delivery. 
