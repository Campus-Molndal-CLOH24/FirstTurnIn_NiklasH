### Documentation for the first turn-in-assignment for CLO24  
## Student: Niklas Häll  
  
## Planning / concept:  
We are creating a software tool to help administrating a CD-enthusiast-shop.  
We have two employees with admin-rights and a guest-login for customers/enthusiasts.  
The software needs to be able to do the following:  
1) Add a CD  
2) Remove a CD  
3) Search for title and/or artist  
  
Possible problem: How do we handle multiple copies of the same CD? (add "quantity" to the CD class?)  

## Classes and class-diagram:  
Initial plan is to try to use the PlantUML extention in VS Code. Write the program in Visual Studio 2022, open the project folder in VS Code and then try to automaticly create the class diagram (and save as UML) via VSCode.  
  
## Flowchart / basic program layout:  
a) Feature: A main menu where we have the following:  
- admin login (we will display a password so the tester can simulate admin tools)  
- guest login (no pass required)  
- End: go back to log-in-screen OR end program  
  
b) Class: Main  
- contains menu (update: shortcuts to the control-class instead, read section 'e')  
  
c) Class: CDcollection  
- searchCD  
- addCD  
- removeCD 
- CDhandling: Since we are not creating a real database, I will simulate this by using a List-function (as we will want to add/remove/etc). This is vital so we keep the information intact when the program is shut down!  
  
d) * Class: User - scrapped! better to keep the tools within the CD-class  
- admin (search, add, remove tools) update: moved to the ShopControl class    
- guest (search tool only) update: moved to the ShopControl class    
  
e) Class: ShopControl-class added later in the project
- It made more sense and had better control to keep all tools for handling CD and the List in the CDcollection-class, so I moved out the shop control tools into its own class. This way we also keep Main clean from clutter.  
  
## Thoughts:
- Guest()-method will need a way to return to MainMenu() through a loop or option to log out. This is tied to the setting we do (admin/guest) for the Menu()-method. Keeping track in the Main() is key.  
- CDCollection only manage the collection  
- User ("guest", unless we create a real class for user handling as well! That could be an option to expand on in the future) only focus on user roles: do not mix responsibilites!  

## bool adminRights = false;  
This (having a bool setting for admin/guest control) entire concept has a few benefits:  
- It is easy to handle conceptually (Clean Code)  
- It needs no connection to a database or server, session control is a natural concept for databases but not here  
- Scalable: We can add more tools for admins without cluttering ShopMenu() with guest options!  
  
## Class-diagram
- To be added!
  
# Development phase one / version history:  
- Initial commit  
- Created CollectionOfCDs + User classes  
- Created basic ShopLogin()- and ShopEnd()-code  
- Updated ShopLogin() to handle admin/guest rights  
- Updated this document-file with the adminRight section above  
- Created basic code for the ShopMenu()-method  
- Added to the ShopEnd()-code  
- ShopMenu() added to, just need to be updated with the links to CD-methods when they are created  

# Next phase intro: Working the CollectionofCDs class:  
- Plan for implementation of a List where we can store the CDs, se below:  
1) I chose between creating this as a .txt, which would be easy to implement but is harder to structure (we would need to create comma-separated values), or to create this as a .json file. That would be easier to read/write data to - so I chose cdcollection.json  
2) I also decided to expand the class to include year of release and genre, as we want to search the class it seems like a good idea to have eras and genres! (plus, some albums could be a re-release with the same title)  
- Here I want to note that to see the files in Virtual Studio we have to shift+alt+a "add existing item" to see the documentation.md + cdcollection.json in solution explorer  

# Development phase two / version history:
- Added the List "CD", the relative file path and  
- ..also CollectionOfCDs-constructor, the primary role is to load LoadCDCollection, which is a method constructed to assist with handling loading the List (this we don't re-use code in the search/add/etc methods)  
- Added SaveCDCollection() to handle writing to file  
- Added NuGet Package: Newtonsoft.Json (required for Json serialization, which we want when we are loading/saving the Json file)  
- Added SearchCD()  
- Added ListCD()  
- Added AddCD() - it needs to go back to, to make validation checks  
- Created the CD class with getters and setters  
- Added RemoveCD()  
- Created the ShopControl()-class and moved all control methods there  

# Issues resolved  
-  One major issue I had before is that a file path might be correct (in the program folder) but the program will still not find it. This happened this time again, with cdcollection.json. The solution is reasonable easy, if you had experienced this before, but I had to ask ChatGPT for guidance the first time. This is the solution: rightclick the file you cannot find (cdcollection.json in this case). Chose properties. Then on Copy to Output directory, chose "Copy always"!  

# Development phase three / version history:
- Intro: We have a working program now, so this phase is about tweaking, finding potential stack overflows, secure exception risks and simply to refine code
- Corrected nine (!) warnings of null value risks, this can be seen in SearchCD and RemoveCD()-methods in the if (searchTerm != null) checks  
  

# To Do:  
- Intro: This list will contain additions I want to see to the program. Some might not make the cut before the release version, in that case they are considered "future upgrades".
- Class-diagram! Added to this file! High prio.    
- SearchCD() - sort the search result alphabetically
- SearchCD() - what if our searchTerm return multiple items, say we search "net" and find both "interNET" and "kenNETh", we'd want to display all of the search hits then  
- AddCD() - validation check if a CD already exists in the collection, if it does increase Quality +1 instead of adding a new CD (merge?)  
- AddCD() - make sure we loop back to the start of the method if input is invalid  
  
- Class: User - add this and create user logins + a list to handle them. This is very, very, very far down the work list so it is considered a bonus or future update  
- Bonus: Formatting the print/listed CD result so it looks more appealing to the eye  
  
  
   