Remove-Item -Path "TestResults\*" -Recurse -Force
dotnet test --collect:"XPlat Code Coverage"
reportgenerator -reports:"TestResults/**/*.xml" -targetdir:"coverage-report" -reporttypes:Html
Start-Process "coverage-report/index.html"