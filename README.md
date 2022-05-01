# Recruitment task
An application to download exchange rate data from the NBP, display it on the website and save it in the database.

## How to run it
1. Clone this repository.
2. Run with docker-compose.
3. File docker-compose.dev.yml can be used to override default docker-compose.yml and expose ports of all services to host machine.

## Projects
### ExchangeRatesSource:
Core projects to get exchange rates from external system and save it to database. When data exists in database, it can be accessed trough API of ExchangeRateSource without calling an actual external system.
### ExchangeRatesSource.Nbp:
Projects to get exxchange rates from NBP.
### Migrator:
Project to perform code first migrations on database.
### WebApi:
Projects to call ExchangeRatesSource for data and displaying it to user.
