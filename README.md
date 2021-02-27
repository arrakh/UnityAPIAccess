# Unity API Access
This repository is made to showcase the ability to access an API, downloading a JSON in Unity for Week 1 Multiplayer Online class's assignment.

Below is a demo gif. As per the assignment's requirement, the id "23" is shown below.

![](https://github.com/arrakh/UnityAPIAccess/blob/main/Assets/Files/jsonreq.gif)

## Program Flow
The main script that handles all the process is the RandomUserScript.cs (Yes, i'm aware this violates the Single Responsibility principle. If given time and incentive, will definitely revamp)
The flow of the script is as following:
1. Create and Process request using unity's WWW
3. Retrieve json, and cache data into a List of UserData to a dictionary.
4. Randomly select from dictionary to display.
5. If user inputs id and presses the button, display user with that id.
