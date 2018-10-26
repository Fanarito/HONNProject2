# HONNProject2 VideotapeGalore

## Requirements

- .NET Core >= 2.1.4
- Sqlite

## Building and Running
You can use Visual Studio or Rider to build and run through an IDE.
You can also run it using the command line by running this command
from the project root. 
```
dotnet run --project VideotapeGalore.WebApi/VideotapeGalore.WebApi.csproj
```

## Running Tests

### Unit Tests
All unit tests are stored in the Tests project and can be run by
going into that directory and running `dotnet test`

### Integration Tests
They are performed using Postman collections. They are stored in
the file `VideotapeGalore.postman_collection.json` and can be run
with either the GUI or CLI.
