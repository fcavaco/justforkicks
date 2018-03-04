# solution
- solution created in Windows Linux Subsystem (Ubuntu 16.4)
- Kata for solution creation as depicted steps below.
- Kata for Rover movement as defined by Codurance (Sandro Mancuso) - check Kata Readme file at solution level.

## solution kata
- I want to create a solution containing a library project and a nunit project using just ubuntu console. Want to include nunit templates on my .net installation.

### steps
- add nunit templates , based on [github repo readme](https://github.com/nunit/dotnet-new-nunit)

``` dotnet new -i NUnit3.DotNetNew.Template ```

- now create the solution: [all remaining steps are from here](https://docs.microsoft.com/en-gb/dotnet/core/testing/unit-testing-with-dotnet-test#creating-the-source-project)

``` mkdir Kata.Rover ```

- create a solution file

``` cd Kata.Rover ```

``` dotnet new sln ```

- create library project

``` dotnet new classlib -n Kata.Rover```

- add library project to the solution

``` cd ..```

``` dotnet sln add ./Kata.Rover/Kata.Rover.csproj```

- create unit tests project

``` dotnet new nunit -n Kata.Rover.Tests```

- add tests project to the solution

``` cd ..```

``` dotnet sln add ./Kata.Rover.Tests/Kata.Rover.Tests.csproj```

- add library reference to the tests project

``` cd Kata.Rover.Tests ```

``` dotnet add reference ../Kata.Rover/Kata.Rover.csproj```
