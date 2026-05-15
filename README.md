# TinyURL Application

A full-stack TinyURL application built using:

* Angular 17
* ASP.NET Core Web API (.NET 8)
* Azure Static Web Apps
* Terraform
* GitHub Actions CI/CD

---

# GitHub Repository

Repository URL:

[TinyURL GitHub Repository](https://github.com/nauroto-saske/TinyUrl)

---

# Frontend Deployment

Frontend application has been successfully deployed using Azure Static Web Apps.

Frontend URL:

https://proud-sand-0e39f9700.7.azurestaticapps.net/

---

# CI/CD Implementation

GitHub Actions CI/CD pipeline has been implemented for frontend deployment.

Features:

* Automatic deployment on push to `main`
* Angular production build
* Azure Static Web Apps deployment

Workflow file:

```text id="0tbv6o"
.github/workflows/azure-static-web-apps-proud-sand-0e39f9700.yml
```

---

# Project Structure

```text id="u4s6lr"
TinyURL/
│
├── TinyUrlClient/          # Angular Frontend
├── TinyUrlAPI/             # ASP.NET Core Web API
├── infrastructure/         # Terraform configuration
└── .github/workflows/      # GitHub Actions workflows
```

---

# Frontend Setup (Angular)

Navigate to frontend project:

```bash id="uh2bje"
cd TinyUrlClient
```

Install dependencies:

```bash id="gqf9rr"
npm install
```

Run locally:

```bash id="tf1vli"
ng serve
```

Frontend runs on:

```text id="x2l50u"
http://localhost:4200
```

---

# Backend Setup (.NET Web API)

Navigate to API project:

```bash id="4a6gcn"
cd TinyUrlAPI
```

Restore packages:

```bash id="80jvso"
dotnet restore
```

Run API:

```bash id="s7w3lz"
dotnet run
```

Swagger URL:

```text id="5rbk2w"
https://localhost:7123/swagger
```

API URLs:

```text id="f8wzbm"
https://localhost:7123
http://localhost:5135
```

---


# Current Limitation

Due to Azure Free Subscription quota limitations, complete deployment of:

* ASP.NET Core Web API
* App Service resources

could not be completed using Terraform within the available timeline.

Frontend deployment and CI/CD implementation are completed successfully.

---

# Technologies Used

* Angular 17
* TypeScript
* ASP.NET Core Web API (.NET 8)
* Terraform
* Azure
* GitHub Actions

---

# Author

Subramani
