# BetManager

## Overview
 This application is made to find solution that you won't defeted by in gamble.When you input odds of horse or roulette and so on, this app will tell you solution you will win absolutely. However, there are cases that solution is not found sometimes.

## Requirement
 windowsOS
 csc v4.0.30319

## Description
 This application will find solution that you won't be defeted by in gamble. Input number of trails, name of row(No), and odds. When you want to add rows, press a add button. When you want to delete rows, press a delete button. When press a delete button, select a cell that there is in a row which you want to delete. Press a execute button. You will find solution. This is column whose backgroud's color is lightblue.

 I will explain the algorithm. If there are pairs of previous bet and odds, the bet of this elements is added one. If there is not, bets of all elements is added one. This is one attempt. This is repeated as many times as the number of number of trails is input. However, there are cases that solution is not found sometimes.

There are some visual problem. I'm sorry about it. Please use at your own risk.

## Usage
Execute on Windows or VisualStudio.I'll introduce how to execute on Windows.

A base of this way is to be set csc's Environment variable. Download from github to any directory. On your cmd, move to the dhirectory.

Execute this command to compile.
```
C:\Directory>csc Program.cs Form1.cs Form1.Designer.cs
```

There will be "Program.exe" in the directory. To execute program, execute this command.
```
C:\Directory>Program.exe
```

You will be able to execute this program.

## License
MIT
