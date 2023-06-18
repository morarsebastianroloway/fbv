# FunBooksAndVideos E-commerce Shop

Welcome to FunBooksAndVideos, an e-commerce shop where customers can view books and watch online videos. Users can have memberships for the book club, the video club or for both clubs (premium). This project aims to provide an API for users to purchase a variety of books, videos and subscriptions.

## Features

- Purchase individual products or opt for club memberships.
- Activate memberships instantly upon purchase.
- Generate shipping slips for physical product orders.
- Manage customer accounts and orders.

## Technologies Used

- .NET Core: A cross-platform, open-source framework for building modern applications.
- Entity Framework Core: An object-relational mapper (ORM) for .NET that simplifies database operations.
- ASP.NET Core Web API: A framework for building HTTP-based services using .NET.
- AutoMapper: A library for object-to-object mapping.
- Handlebars.Net: A template engine for generating HTML templates.
- iTextSharp: A library for generating pdf files.
- Swagger: An open-source framework for documenting and testing APIs.

## API Documentation

The API is documented using Swagger. Once the project is running, you can access the Swagger UI to explore the available endpoints and test them. Open your web browser and navigate to `https://localhost:<port>/swagger` (e.g., `https://localhost:5001/swagger`).

## Contributing

Contributions to FunBooksAndVideos are welcome! If you find any bugs, have suggestions, or would like to add new features, please feel free to open an issue or submit a pull request. Make sure to follow the project's coding conventions and guidelines.

## License

This project is licensed under the [MIT License](LICENSE). You are free to use, modify, and distribute the codebase. Refer to the License file for more information.

## Acknowledgments

We would like to acknowledge the following resources and libraries that have been instrumental in the development of FunBooksAndVideos:

- [Microsoft Docs](https://docs.microsoft.com/): Official documentation for .NET Core and related technologies.
- [Entity Framework Core Docs](https://docs.microsoft.com/ef/core/): Documentation for Entity Framework Core.
- [AutoMapper Documentation](https://docs.automapper.org/): Documentation for AutoMapper.
- [iTextSharp](https://itextpdf.com/products/itextsharp): Documentation for iTextSharp
- [Handlebars.Net GitHub](https://github.com/rexm/Handlebars.Net): Official repository for Handlebars.Net.
- [Swagger Documentation](https://swagger.io/docs/): Documentation for Swagger.

## Further steps

- Implement authentication and authorization. In orther to keep the project simple, these were not build.
- Create a business logic project that will stay between API and DAL projects. The PO processor should be in there. I bypass it, for the sake of simplicity 
- Build PUT and DELETE methods for PO. For the sake of the example these methods were not build. 
- In order to better track the subscriptions a history table should be created, so when a PO is updated we need to deactivate the subscription in case PO was updated.
- Treat premium case: in case it has for membership for book club, when adding video membership it should change to premium (also when it has video and we're adding book club membership).
- Create all the necessary unit tests. Only few where created as an example
