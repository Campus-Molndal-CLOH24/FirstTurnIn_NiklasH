### Documentation for the first turn-in-assigmnet for CLO24  
## Student: Niklas Häll  
  
# Planning / concept:  
We are creating a software tool to help administration a CD-enthusiast-shop.  
We have two employees with admin-rights and a guest-login for customers/enthusiasts.  
The software needs to be able to do the following:  
1) Add a CD  
2) Remove a CD  
3) Search for title and/or artist  
Problem: How do we handle multiple copies of the same CD?  

# Classes and class-diagram:  
Initial plan is to try to use the PlantUML extention in VS Code. Write the program in Visual Studio 2022, open the project folder in VS Code and then try to automaticly create the class diagram (and save as UML) via VSCode.  
  
# Flowchart / basic program layout:  
a) A main menu where we have the following:  
- admin login (we will display a password so the tester can simulate admin tools)  
- guest login (no pass required)    
b) Classes:  
b1) Main
- menu   
b2) CDcollection  
- searchCD  
- addCD  
- removeCD  
b3) User  
- admin (search, add, remove tools)  
- guest (search tool only)  

