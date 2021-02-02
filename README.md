# SearchWords
Test project. Implement SOLID principles.

This solution has been implemented in Net Core 3.1, with Visual Studio 2019.

It contains 4 projects:
1. Searchwords : Console application. Already configured as STARTING PROJECT.
2. Searchwords.BR : Class library. Contains the business rules.
3. Searchwords.Models : Class library. Defines the File and Folder models.
4. UnitTest.BR : Unit Test project, to test the "search" method, located in "Searchwords.BR" project.

HOW TO BUILD: Open "SearchWords.sln" from Visual Studio and press Control + Shift + B

HOW TO EXECUTE:
1. From Visual Studio: 
    - Open "SearchWords.sln" from Visual Studio
    - Navigate to : Properties of "SearchWords" Project ->  Debug  -> Type the path to check in "Application args" field
    - Press F5
    
2. From CMD:
    - Open "SearchWords.sln" from Visual Studio
    - Build the solution (Control + Shift + B)
    - From CMD, open the folder %solution folder%\SearchWords\bin\Debug\netcoreapp3.1\
    - Type "SearchWords.exe %path_to_check%"  (without " neither %)        
