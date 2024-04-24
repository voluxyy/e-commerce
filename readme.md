## Back-End
<img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white" /> &nbsp; Manipulate our database 

<br>

## Front-End
<img src="https://img.shields.io/badge/Angular-DD0031?style=for-the-badge&logo=angular&logoColor=white" /> &nbsp; Get the data from our database and show it to the user <br>
<img src="https://img.shields.io/badge/Sass-CC6699?style=for-the-badge&logo=sass&logoColor=white" /> &nbsp; Giving some style to our front

<br>

---

<br>

### Few [Gitmoji](https://gitmoji.dev) for understand our conventionnal commit
**:construction:**  -  *'Work in progress.'*<br>
**:card_file_box:**  -  *'Perform database related changes.'*<br>
**:memo:** -  *'Add or update documentation.'*

### Modèle de données :
```mermaid
classDiagram
    User --|> Comment
    User --|> Wish
    User --|> Rate
    
    User <|-- ShoppingCart

    Product <|-- Category
    Product <|-- ProductList

    ShoppingCart <|-- ProductList

    Comment <|-- Product

    Rate <|-- Product

    Wish <|-- Product

    WishList <|-- Wish

    class Wish{
        int Id
        int ProductId
        int UserId
    }
    class WishList{
        int Id
        int WishId
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
    class Comment{
        int Id
        string Title
        string Description
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