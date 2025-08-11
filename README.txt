#run StayTrackPro client 
dotnet run --project StayTrackPro

#run StayTrackPro api
dotnet run --project StayTrackPro.API

#curl requests 
curl http://localhost:5007/api/suites

#swagger
http://localhost:5007/swagger

#run tests in API.test
dotnet test

# Rebuild and deploy 
dotnet build
az webapp up --name staytrackpro-api --resource-group StayTrackProRG --runtime "dotnet:8"
