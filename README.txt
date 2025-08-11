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

#rebuild and deploy 
dotnet build
az webapp up --name staytrackpro-api --resource-group StayTrackProRG --runtime "dotnet:8"

#deployed backend 
https://staytrackpro-api-fxewggh3fvazd0ac.scm.indonesiacentral-01.azurewebsites.net/api/deployments/latest

#deployed frontend 
https://staytrackpro-frontend-fkgpfxhhbza4f9db.indonesiacentral-01.azurewebsites.net/Reservations

#restart App
az webapp restart `
  --resource-group staytrackpro-rg `
  --name staytrackpro-frontend


#step 1 — Rebuild & publish locally
dotnet publish -c Release -o ./publish

#step 2 — Zip the publish folder
Compress-Archive -Path .\publish\* -DestinationPath .\publish.zip -Force

#step 3 — Deploy to the same Azure Web App

az webapp deploy `
  --resource-group staytrackpro-rg `
  --name staytrackpro-api `
  --src-path .\publish.zip `
  --type zip

  #verify the URL
  az webapp show `
  --resource-group staytrackpro-rg `
  --name staytrackpro-frontend `
  --query defaultHostName `
  --output tsv


#running frontend connected with backend
https://staytrackpro-frontend-fkgpfxhhbza4f9db.indonesiacentral-01.azurewebsites.net/Reservations