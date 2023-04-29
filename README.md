# TourPlanner_4_SWENII
### This is Group4 which is planning a great TourPlanner.

First we formed a team of two students +1 confused One to develop an application based on the GUI frameworks C# / WPF.
The user creates (bike-, hike-, running- or vacation-) tours in advance and manages  the logs and 
statistical data of accomplished tours..

We startet creating a WPF Application [using IDE Visual Studio 2022; .NET 7.0 Version]. 

### Layered Architecture (in form of Subprojects [WPF Class Library] added)
- BL
- DAL
- Models

We added an NUnit Testproject (called TourPlanner_4_SWENII.Test); changed - as by the lecturer mentioned -
the     <TargetFramework>net7.0-windows</TargetFramework> .
- installed Microsoft.NET.Test.Sdk 
- installed NUnit package

- Npqsl [included in the DAL, for database access]

- itext7  [included in the BL, for Creating pdf-Reports]
- 


[MapQuest link0<----------------------]

_______________notes: 
### Database PostgreSQL 

- Database named: 
- postgres image running on Docker
- Docker compose .yml file included in the project


// TODO: include UML | https://drive.google.com/file/d/15EUxkCWOCbWE_4D4CVqPU1yTH-qm9TNI/view?ts=6411c8db


