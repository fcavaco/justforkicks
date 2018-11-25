#! /bin/bash
SOLNAME=$1

mkdir $SOLNAME
cd $SOLNAME
dotnet new sln
dotnet new classlib -n $SOLNAME
dotnet sln add ./$SOLNAME/$SOLNAME.csproj
dotnet new nunit -n $SOLNAME.tests
dotnet sln add ./$SOLNAME.tests/$SOLNAME.tests.csproj
cd ./$SOLNAME.tests
dotnet add reference ../$SOLNAME/$SOLNAME.csproj
dotnet add package Microsoft.DotNet.Watcher.Tools --version 2.1.0-preview1-final
dotnet add package Moq
dotnet add package FluentAssertions
dotnet restore
dotnet install tool -g dotnet-watch --version 2.1.0-preview1-final
cp /mnt/c/projects/scripts/.gitignore ./.gitignore  
cd ./$SOLNAME.tests
dotnet restore

dotnet watch test
