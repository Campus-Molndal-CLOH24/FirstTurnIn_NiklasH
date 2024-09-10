### Documentation for the first turn-in-assigmnet for CLO24  
## Student: Niklas Häll  
  
## Planning / concept:  
We are creating a software tool to help administration a CD-enthusiast-shop.  
We have two employees with admin-rights and a guest-login for customers/enthusiasts.  
The software needs to be able to do the following:  
1) Add a CD  
2) Remove a CD  
3) Search for title and/or artist  
Problem: How do we handle multiple copies of the same CD? (add "quantity" to the CD class?)  

## Classes and class-diagram:  
Initial plan is to try to use the PlantUML extention in VS Code. Write the program in Visual Studio 2022, open the project folder in VS Code and then try to automaticly create the class diagram (and save as UML) via VSCode.  
  
## Flowchart / basic program layout:  
a) A main menu where we have the following:  
- admin login (we will display a password so the tester can simulate admin tools)  
- guest login (no pass required)  
- End: go back to log-in-screen OR end program  
b) Classes:  
b1) Main
- menu   
b2) CDcollection  
- searchCD  
- addCD  
- removeCD 
- CDhandling: Since we are not creating a real database, I will simulate this by using a List-function (as we will want to add/remove/etc). This is vital so we keep the information intact when the program is shut down!  
b3) User  
- admin (search, add, remove tools)  
- guest (search tool only)  
  
  
## Thoughts:
- Guest()-method will need a way to return to MainMenu() through a loop or option to log out. This is tied to the setting we do (admin/guest) for the Menu()-method. Keeping track in the Main() is key.  
- CDCollection only manage the collection  
- User only focus on user roles: do not mix responsibilites!  

## bool adminRights = false;  
This entire concept has a few benefits:  
- It is easy to handle conceptually (Clean Code)  
- It needs no connection to a database or server, session control is a natural concept for databases but not here  
- Scalable: We can add more tools for admins without cluttering ShopMenu() with guest options!  
  
# Development / version history:  
- Initial commit  
- Created CollectionOfCDs + User classes  
- Created Methods  
- Basic ShopLogin()- and ShopEnd()-code  
- Updated ShopLogin() to handle admin/guest rights  
- Updated this file with the adminRight section  
- Basic code for the ShopMenu()-method  
- Added the ShopEnd()-code  
-  


