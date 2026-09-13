# Domain model

```mermaid
classDiagram
    class User {
        int Id
        string UserName
        string Password
    }

    class Post {
        int Id
        string Title
        string Body
        int UserId
    }

    class Comment {
        int Id
        string Body
        int UserId
        int PostId
    }

    User "1" --> "0..*" Post : writes
    User "1" --> "0..*" Comment : writes
    Post "1" --> "0..*" Comment : has
```

Relationships are implemented with the `UserId` and `PostId` foreign-key
properties. The entity classes intentionally contain no association or
collection properties in Parts 1 and 2.
