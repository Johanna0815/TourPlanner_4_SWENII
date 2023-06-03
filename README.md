# TourPlanner_4_SWENII
### This is Group4 which is planning a great TourPlanner.

First we formed a team of two students +1 confused One to develop an application based on the GUI frameworks microsoft.NET WPF in C#.
The user creates (bike-, hike-, running- or vacation-) tours in advance and manages the logs and 
statistical data of accomplished tours..<br>

We startet creating a WPF Application [using IDE Visual Studio 2022; .NET 7.0 Version]. As in the Repo displayed, we worked a lot with GIT by having the HEAD as a main branch and other branches like dev with some feature/branches.  


## App architecture (layers and layer contents/functionality)
- Uses markup-Based UI framework

 microsoft .NET Windows Presentation Foundation; on windows 
- Uses MVVM for UI

Model View ViewModel [Folders: ViewModels and Views in the BaseProject]
- Implements a layer-based architecture (UI/BL/DAL)


### Layered Architecture (in form of Subprojects [WPF Class Library] added)
- BL
- DAL
- Models
- Utils
- Test
- Implements at least one design pattern







## Use Case Diagram:


## UX, library decisions (where applicable), lessons learned

## Description of the implemented design pattern

## Unit Testing decisions. Why those tests was chosen:

## Description of the unique Feature:


## Time Tracked: 

<table>
  <thead>
    <tr>
      <th>Miriam</th>
      <th>Yousef</th>
      <th>Johanna</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td>x hours </td>
      <td>x hours </td>
      <td>55 hours</td>
    </tr>
    
  </tbody>
</table>

## Link to GIT

https://github.com/Johanna0815/TourPlanner_4_SWENII


##____________________________________________________________
_____________|||||||_________________________



- Uses a Postgres Database for storing Tour Data
- Does not allow for SQL injection
- Uses an OR-Mapping Library
- Uses a config file that stores at minimum the DB connection string
- Integrates the MapQuest API
- Integrates log4j/log4net or similar Log Libraries
- Integrates a report-generation library
- Implements at least 20 Unit Tests


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




