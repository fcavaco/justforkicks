## to execute dotnet core
-located at PrizeDraw\bin\Release\netcoreapp2.0
``` > dotnet PrizeDraw.dll < input.txt ```
## to execute as .exe
- Note that you should find an executable already compiled at PrizeDraw\bin\Release\netcoreapp2.0\win10-x64

- because this is a dotnet core project, to create a windows executable you need to build it by:
at the solution root folder.
```> dotnet publish -c Release -r win10-x64```

## in addition

- I've added the option of processing a file given path passed as argument.