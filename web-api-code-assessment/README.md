# Web API Coding Challenge
Your task is to complete the attached API that provides standard CRUD functionality (add, update, list, delete) for **Players** and their **Skills**.

There is no strict time limit and you won't be judged on how long it took you to complete, however we would like you to spend no more than 1 to 2 day on this.

This project uses ASP.NET Core and .NET 6. Visual Studio 2022 is recommended but not required for completion.

## What is Already Completed
The scaffolding of Web API project has been created and endpoint routing has been set up along with an in-memory database structure using Entity Framework Core 6 has been setup for you. You will not need to manipulate anything with the `DbContext`. _NOTE that since this uses an in-memory database, every time the application is stopped, the database will be purged._

## What You Need To Do
What you will need to do is complete the **FIVE** not-implemented endpoints within `PlayerController.cs` that will manage the **Player** CRUD functionality along with adding validation rules that will be specified below.


## Specifications
### Data Requirements
**Player fields**
| Field Name      | Data Type  | Required | Validation                                                |
|--               |--          |--        |--                                                         |
| Name            | `string`   | true     | Cannot be null or empty                                   |
| Position        | `string`   | true     | Must be one of the {'defender', 'midfielder', 'forward' } |
| PlayerSkills    | `object`   | true     | Must contain at least one **PlayerSkill** object          |

**PlayerSkill fields**
| Field Name      | Data Type  | Required | Validation                                                                |
|--               |--          |--        |--                                                                         |
| Skill           | `string`   | true     | Must be one of the {'defense', 'attack', 'speed', 'strength', 'stamina' } |
| Value           | `int`      | true     | Must be between [1 and 99]                                                |

An example of a valid player in JSON format is:
```json
{
   "name": "player name",
   "position": "midfielder",
   "playerSkills": [
      {
         "skill": "defense",
         "value": 60
      },
      {
         "skill": "speed",
         "value": 80
      }
   ]
}
```

In the case a validation error occurrs, a 400 status code should be returned with an error message with the validation violation. Examples:

```json
{
    "message": "Invalid value for position: midfielder1"
}
```

```json
{
    "message": "Invalid value for player skill 'defense': 102"
}
```

### CRUD Requirements

**1. Creating a Player**

The endpoint `POST api/player` will support creating a new player and their skills.

Request body example:
```json
{
   "name": "player one",
   "position": "defender",
   "playerSkills": [
      {
         "skill": "defense",
         "value": 90
      },
      {
         "skill": "speed",
         "value": 45
      }
   ]
}


{
   "name": "player two",
   "position": "forward",
   "playerSkills": [
      {
         "skill": "defense",
         "value": 95
      },
      {
         "skill": "speed",
         "value": 55
      }
   ]
}
```

Expected response example:
```json
{
   "id": 1,
   "name": "player one",
   "position": "defender",
   "playerSkills": [
      {
         "id": 1,
         "skill": "defense",
         "value": 90,
         "playerId": 1,
      },
      {
         "id": 2,
         "skill": "speed",
         "value": 45,
         "playerId": 1,
      }
   ]
}
```

**2. Updating a Player**

The endpoint `PUT api/player/{id}` will support updating an existing player where `{id}` is the id of the player to be updated.

Request body example:
```json
{
   "name": "player name updated",
   "position": "defender",
   "playerSkills": [
      {
         "skill": "defense",
         "value": 45
      },
      {
         "skill": "speed",
         "value": 88
      }
   ]
}
```

Expected response example:
```json
{
   "id": 1,
   "name": "player name updated",
   "position": "defender",
   "playerSkills": [
      {
         "id": 1,
         "skill": "defense",
         "value": 45,
         "playerId": 1,
      },
      {
         "id": 2,
         "skill": "speed",
         "value": 88,
         "playerId": 1,
      }
   ]
}
```

**3. Deleting a Player**

The endpoint `DELETE api/player/{id}` will support deleting an existing player where `{id}` is the id of the player to be updated.

A 200 Status code should be return from this endpoint if no error occurs.

**4. Listing all Players**

The endpoint `GET api/player` will support listing all existing players and their skills.

Expected response example:
```json
[
    {
        "id": 1,
        "name": "player name 1",
        "position": "defender",
        "playerSkills": [
            {
                "id": 1,
                "skill": "defense",
                "value": 60,
                "playerId": 1
            },
            {
                "id": 2,
                "skill": "speed",
                "value": 80,
                "playerId": 1
            }
        ]
    },
    {
        "id": 2,
        "name": "player name 2",
        "position": "midfielder",
        "playerSkills": [
            {
                "id": 3,
                "skill": "attack",
                "value": 20,
                "playerId": 2
            },
            {
                "id": 4,
                "skill": "speed",
                "value": 70,
                "playerId": 2
            }
        ]
    }
]
```

**5. Listing Single Player**

The endpoint `GET api/player/{id}` will support returning an existing player where `{id}` is the id of the player that is being requested.

Expected response example:
```json
{
    "id": 1,
    "name": "player name 1",
    "position": "defender",
    "playerSkills": [
        {
            "id": 1,
            "skill": "defense",
            "value": 60,
            "playerId": 1
        },
        {
            "id": 2,
            "skill": "speed",
            "value": 80,
            "playerId": 1
        }
    ]
}
```

## Testing
The application uses Swagger UI (https://swagger.io/tools/swagger-ui/) for API visualization and interaction. Feel free to use other tools like Postman if you are more comfortable with them.