# Tutorial

## Running the App

Execute in Powershell 

```
build.ps1
```

Run 

```
docker-compose up
```

If you have any problem try 

```
docker-compose up --force-recreate
```

## Manual testing the App

### Will be empty at first

GET http://localhost:5000/api/Greetings

### Now add a Greetings

POST http://localhost:5000/api/Greetings

Content-Type: application/json

{
    "Message" : "Hello World"
}

### Now see that greeting

GET http://localhost:5000/api/Greetings/{xxxx-xxxx-xxxx-xxx}


### Now add some more greetings

POST http://localhost:5000/api/Greetings

Content-Type: application/json

{
    "Message" : "Hello NetCoreConf"
}

### Now see all the greeetings

GET http://localhost:5000/api/Greetings

### Now delete one greeetings

DELETE http://localhost:5000/api/Greetings/{xxxx-xxxx-xxxx-xxx}


