# To start the project
1. Git clone this repository
2. Start the Back-End project with the command ``` dotnet run --project .\e-commerce\ ```
3. Start the Angular server with this command
   ``` cd .\front\src\app\ |  ng serve --o ```. <br>If you are already in the app folder, just type ``` ng serve --o ```

## Back-End
<img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white" /> &nbsp; <img src="https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white" />

- E-commerce for API (Controllers)
- E-commerce.Business for Business Layer (Services)
- E-commerce.Data for Data Layer (Repositories)

<br>

## Front-End
<img src="https://img.shields.io/badge/Angular-DD0031?style=for-the-badge&logo=angular&logoColor=white" /> &nbsp; <img src="https://img.shields.io/badge/TypeScript-007ACC?style=for-the-badge&logo=typescript&logoColor=white" />
 &nbsp;  <img src="https://img.shields.io/badge/Sass-CC6699?style=for-the-badge&logo=sass&logoColor=white" /> &nbsp; <img src="https://img.shields.io/badge/HTML5-E34F26?style=for-the-badge&logo=html5&logoColor=white" />

- Angular using Typescript to create Module, Services and Components
- Customize Components Templates with HTML & SCSS
  
<br>

---

<br>

### Modèle de données :
```mermaid
classDiagram
    User --|> Reviews
    User --|> Wish
    User --|> Rate
    User --|> ShoppingCart
    Product <|-- Category
    Product <|-- ProductList
    ShoppingCart <|-- ProductList
    Reviews <|-- Product
    Rate <|-- Product
    Wish <|-- Product
    Sale <|-- Product
    Sale <|-- User

    class Sale{
        int Id
        int ProductId
        int UserId
        string ActivationCode
        dateOnly Date
    }
    class Wish{
        int Id
        int ProductId
        int UserId
    }
    class Product{
        int Id
        string ImagePath
        string Name
        float Price
        int Quantity
        int CategoryId
    }
    class Rate{
        int Id
        int Value
        int ProductId
        int UserId
    }
    class ProductList{
        int Id
        int ProductId
        int ShoppingCartId
    }
    class ShoppingCart{
        int Id
        int UserId
    }
    class Category{
        int Id
        string CategoryName
    }
    class User{
        int Id
        string Firstname
        string Lastname
        string Pseudo
        string Email
        string Password
        DateTime Birthdate
        double Money
    }
    class Reviews{
        int Id
        string Title
        string Description
        int Rate
        int ProductId
        int UserId
    }
    class Admin{
        int Id
        string Pseudo
        string Email
        string Password
        int Permission
    }
```
