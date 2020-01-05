# C# - ASP.NET MVC Technotip Tutorials

About the application technologies and operation:

### Technologies:
- Programming Language: C#
- FrontEnd Side: ASP.NET MVC
- BackEnd Side: .NET Framework 4.5.2.
- Descriptive Language: HTML5
- Style Description Language: CSS (Bootstrap 3.0.0)
- Database: SQL Server (Database First)
- Other used modul: Entity Framework 6.0.0.0

### Installation/ Configuration:

1. Open CMD and Create a new **SQLLocalDB Instance** named **LocalDBExample**

   ```
   SQLLocalDB create LocalDBExample
   ```

2. Connect to **LocalDBExample Instance** with **Windows Authentication**

   ```
   (LocalDB)\LocalDBExample
   ```
   
3. **CREATE** necessary **DATABASE** with the following **SCRIPT**

   ```SQL
   CREATE DATABASE EmployeesDB;
   ```
   
4. **CREATE** necessary **TABLES** with the following **SCRIPT** (The scripts can be found in the following folder: **DB TABLES**)

   ```SQL
   USE EmployeesDB;

   CREATE TABLE [dbo].[Department] (
       [DepartmentID] INT           IDENTITY (1, 1) NOT NULL,
       [Name]         VARCHAR (255) NULL,
       PRIMARY KEY CLUSTERED ([DepartmentID] ASC)
   );

   CREATE TABLE [dbo].[Employee] (
       [EmployeeID]   INT           IDENTITY (1, 1) NOT NULL,
       [Name]         VARCHAR (255) NULL,
       [DepartmentID] INT           NOT NULL,
       [Adress]       VARCHAR (255) NULL,
       PRIMARY KEY CLUSTERED ([EmployeeID] ASC),
       FOREIGN KEY ([DepartmentID]) REFERENCES [dbo].[Department] ([DepartmentID])
   );

   CREATE TABLE [dbo].[Country] (
       [CountryID]   INT           IDENTITY (1, 1) NOT NULL,
       [CountryName] VARCHAR (150) NULL,
       CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED ([CountryID] ASC)
   );

   CREATE TABLE [dbo].[Sites] (
       [SitesID]    INT           IDENTITY (1, 1) NOT NULL,
       [EmployeeID] INT           NOT NULL,
       [SiteName]   VARCHAR (255) NULL,
       PRIMARY KEY CLUSTERED ([SitesID] ASC),
       FOREIGN KEY ([EmployeeID]) REFERENCES [dbo].[Employee] ([EmployeeID])
   );

   CREATE TABLE [dbo].[UserRole] (
       [RoleID]   INT           IDENTITY (1, 1) NOT NULL,
       [RoleName] NVARCHAR (50) NULL,
       CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED ([RoleID] ASC)
   );

   CREATE TABLE [dbo].[SiteUser] (
       [UserID]   INT            IDENTITY (1, 1) NOT NULL,
       [UserName] NVARCHAR (50)  NULL,
       [EmailID]  NVARCHAR (50)  NULL,
       [Password] NVARCHAR (50)  NULL,
       [Address]  NVARCHAR (150) NULL,
       [RoleID]   INT            NULL,
       CONSTRAINT [PK_SiteUser] PRIMARY KEY CLUSTERED ([UserID] ASC),
       CONSTRAINT [FK_SiteUser_UserRole] FOREIGN KEY ([RoleID]) REFERENCES [dbo].[UserRole] ([RoleID])
   );

   CREATE TABLE [dbo].[State] (
       [StateID]   INT            IDENTITY (1, 1) NOT NULL,
       [StateName] NVARCHAR (150) NULL,
       [CountryID] INT            NULL,
       CONSTRAINT [PK_State] PRIMARY KEY CLUSTERED ([StateID] ASC),
       CONSTRAINT [FK_State_Country] FOREIGN KEY ([CountryID]) REFERENCES [dbo].[Country] ([CountryID])
   );

   CREATE TABLE [dbo].[ImageStore] (
       [ImageID]   INT             IDENTITY (1, 1) NOT NULL,
       [ImageName] NVARCHAR (1000) NULL,
       [ImageByte] IMAGE           NULL,
       [ImagePath] NVARCHAR (1000) NULL,
       CONSTRAINT [PK_ImageStore] PRIMARY KEY CLUSTERED ([ImageID] ASC)
   );
   ```
   
5. In the Web.config file, configure the data needed to use the Email Service (from **43_HowToSendEmail** Project)

   ```XML
   <add key="SenderEmail" value="testemail@test.com" />
   <add key="SenderPassword" value="*****" />
   ```
  
### About the application:

Here you can start learning ASP.NET MVC from the scratch step-by-step through source code. This tutorial is 100% for beginners. Here you will come to know about how can you create a website using ASP.NET MVC.

In this tutorial, you explained the basics of ASP.NET MVC through source code and what is the tool you need to develop a basic website.

#### Steps:
- MODEL Binding in ASP.NET MVC (Model-View-Controller)
- Difference betweeen ViewBag and ViewData and TempData | Peek and Keep
- ASP.NET MVC Database Connection Using Entity Framework | Connect to SQL Server database
- Working with Multiple Tables in ASP.NET MVC (Model-View-Controller)
- Generate HYPERLINKS using ACTIONLINK Html Helper
- Razor View Engine in MVC | ASP.NET MVC Razor Syntax
- HTML Helpers in MVC 5 | Create Textbox, Dropdown, Hyperlinks using Html Helper Object
- Insert data into Database in ASP.NET MVC | Subimt a From and Save Record
- Insert data into MULTIPLE TABLES in an ASP.NET MVC Application
- Server Side and Client Side Validation in ASP.NET MVC
- Insert data into Database using jQuery AJAX in ASP.NET MVC Application
- How to create a Bootstrap Popup (Modal) and INSERT data using jQuery in ASP.NET MVC
- Delete operation in ASP.NET MVC using jQuery and Bootstrap Popup (Modal)
- What is Partial View in ASP.NET MVC 
- How to call a Partial View using jQuery in ASP.NET MVC
- Difference between Html.Partial and Html.RenderPartial in ASP.NET MVC
- Add Edit Record using Partial View, jQuery and Bootstrap Popup (Modal) in ASP.NET MVC
- Layout View in an ASP.NET MVC
- Style, Render, Script Render and BundleConfig file in ASP.NET MVC
- RenderBody, RenderSection and RenderPage method in ASP.NET MVC
- Divide page into several component using Bootstrap Grid System in ASP.NET MVC
- How to refresh Entity Framework after adding new table into SQL Database
- How to add Foreign Key relationship between two tables of SQL Database
- Create REGISTRATION form using Bootstrap and jQuery in ASP.NET MVC
- Create Login Page using Bootstrap and jQuery in ASP.NET MVC
- Client Side Validation using jQuery in ASP.NET MVC
- How to return multiple models to a view in ASP.NET MVC
- How to create dynamic Menu in ASP.NET MVC using Partial View and Bootstrap
- Preview Image before upload using jQuery in ASP.NET MVC
- Upload and display Image using jQuery in ASP.NET MVC | To file server
- Insert Image to SQL Server and Retrive Image from SQL Server using jQuery in ASP.NET MVC
- How to upload image by copying image link (URL) from google using jQuery in ASP.NET MVC
- Cascading dropdown list using jQuery and Partial View in ASP.NET MVC
- Search record in ASP.NET MVC using jQuery and Partial View
- Attribute routing in ASP.NET MVC
- How to display Multiple Checkbox with checked or unchecked value in ASP.NET MVC
- How to send Multiple checkbox IDs to the Controller using jQuery in ASP.NET MVC
- How to create responsive sortable photo gallery with jQuery in ASP.NET MVC
- Implement jQuery Autocomplete Textbox with data from Server using AJAX in ASP.NET MVC
- How to send Email in ASP.NET MVC
- Integrate jQuery DataTables plugin into ASP.NET MVC Application
- Display record from Database using jQuery DataTables plugin in ASP.NET MVC
- Add Edit record using jQuery DataTable plugin in ASP.NET MVC
- jQuery DataTables Example - Server Side Processing in ASP.NET MVC
- Searching - jQuery DataTable Server Side Processing in ASP.NET MVC
- Pagination using Skip and Take method | jQuery Server Side Processing in ASP.NET MVC
- Refresh DataTable after performing any action in ASP.NET MVC
