# 2FA Service API

## Requirements to run

- [.NET 7](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)

## How to Run
 Set docker compose as startup project in Visual Studio and start the app or run **_docker compose up_** in the solution folder.

Open your browser to url https://localhost:5001/swagger/index.html and if everything is correct you should see the swagger page


## Endpoints
### Send Verification Code
#### It will send a confirmation code to the requested phone number
Parameters:
- **countryCode**: **string** //Examples: +55, +1, +380 
- **phoneNumber**: **string** //Examples: 4567890, 8598415365, 5555678

**Json Example:** 

{
  "phone": "5551234567",
  "countryCode": "+1"
}


### Validate Code
#### It will validate the confirmation code sent in the request
Parameters:
- **countryCode**: **string** //Examples: +55, +1, +380 
- **phoneNumber**: **string** //Examples: 4567890, 8598415365, 5555678
- **confirmationCode**: **string** //Examples: 123456


**Json Example:** 

{
  "phoneNumber": "5551234567",
  "countryCode": "+1",
  "confirmationCode": "123456"
}
