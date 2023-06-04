# TourPlanner_4_SWENII
### This is Group4 which is planning a great TourPlanner.

First we formed a team of two students +1 confused One to develop an application based on the GUI frameworks microsoft.NET WPF in C#.
The user creates (bike-, hike-, running- or vacation-) tours in advance and manages the logs and 
statistical data of accomplished tours..<br>

We startet creating a WPF Application [using IDE Visual Studio 2022; .NET 7.0 Version]. As in the Repo displayed, we worked a lot with GIT by having the HEAD as a main branch and other branches like dev with some feature/branches.  

## App architecture (layers and layer contents/functionality) <br>
- Uses markup-Based UI framework <br>
 microsoft .NET Windows Presentation Foundation; on OperatingSystem: windows <br>
- Uses MVVM for UI <br>
Model View ViewModel [Folders: ViewModels and Views in the BaseProject]
- Implements a layer-based architecture (UI/BL/DAL) <br>
## Layered Architecture (in form of Subprojects [WPF Class Library] added)
- BL <br>
  - itext7  [included in the BL, for Creating pdf-Reports] <br>
- DAL <br>
  - Npqsl [included in the DAL, for database access] <br>
  - postgres image running on Docker <b>
  - Docker compose .yml file included in the project <br>
- Models <br>
- Utils <br>
- Test <br>

## Use Case Diagram: <br>
Link to Use Case Diagram: <br>
https://github.com/Johanna0815/TourPlanner_4_SWENII/wiki/TourPlanner_4_SWENII

## UX, library decisions (where applicable), lessons learned
As an UserExpirience may count the advantage, that we had last semester learned about serializing and desirilizing Objects, which means, that Export and Import an JSON was quite fast done. <br>
On the Other Hand to handle WPF correct and doing the Data Binding from the View to the Model and vice versa and staying in the correct Layer and doing decisions which is best practice for this project often led to small discussions in our group. 
For a Practically manner we decided to use the picture from the requirement as a template, that each GroupMember has an valid comparison to see if what has been done fits. Lessions also was learned during the hours in class, where we was including some Nuget Packages together, so it was easier for us to notice where they need to be included and also how to use. MapQuest was also a little bit tricky, because it was not that clear for us in the beginning, where the first Task starts. But then we tried out a few things to pass some values in the DB and to extract the JSON (some attrributes of a Route) and gone further with trying out the secound Task, becuase for us itw was a concern, that the project has async Tasks, beacuse we wanted this for our personal lession learned goals; to improve async programmming. 
<br>
- Uses an OR-Mapping Library: <br>
Entity Framework Core 6 (O/R Mapping)

<br>

## Description of the implemented design pattern <br>
- Implements at least one design pattern


<br>

> Observer is a behavioral design pattern that lets you define a 
> subscription mechanism to notify multiple objects about any 
> events that happen to the object they’re observing. [Quelle: https://moodle.technikum-wien.at/pluginfile.php/1718086/mod_resource/content/6/BIF4-SWEN2_04_Observer.pdf ]

<br>
In our Project we use the Observable Design Pattern within MVVM architecture. To make the communication between the ViewModel and the View better. The design Pattern ensures for that changes in the ViewModel are reflected to the View. <br>
Model: <br>
the data and business logic will be represented <br>
for example Model provides properties that needs to be to for changes observed <br>

ViewModel: <br>
it acts as an commiter between the View and the Model. There are properties contained whose are bounded to the View. THe Observable pattern is included by using an implementation of the INotifyPropertyChanged Interface OR a base ViewModel class whcih provides the necessary Method. <br>
View: <br>
In the View the data will be displayed which interacts with the user. To update the UI it listens to the PropertyChaneged event of the ViewModel for each change of the property changes. 
<br>
Conclusion it is a powerful combination (MVVM within ObservablePattern) to build good UI; because maintaining is easier with separating of concerns and promoting tests.
<br>


## Unit Testing decisions. Why those tests was chosen: <br>
We added an NUnit Testproject (called TourPlanner_4_SWENII.Test); <br>
changed - as by the lecturer mentioned - the <TargetFramework>net7.0-windows</TargetFramework> . <br>
- installed Microsoft.NET.Test.Sdk 
- installed NUnit package

## Description of the unique Feature: <br>


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
      <td>80 hours </td>
      <td>76 hours </td>
      <td>72 hours</td>
    </tr>
    
  </tbody>
</table>

## Link to GIT

https://github.com/Johanna0815/TourPlanner_4_SWENII
