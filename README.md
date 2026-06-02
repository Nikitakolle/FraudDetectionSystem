# Fraud Detection System

## Real-Time Fraud Analysis using C#, ASP.NET Core Web API, React, SQL Server, and ML.NET

### Overview

Fraud Detection System is a full-stack web application developed using C#, ASP.NET Core Web API, React, SQL Server, Entity Framework Core, JWT Authentication, and ML.NET.

The system analyzes financial transactions in real time and predicts the probability of fraudulent activity using machine learning. Users can securely authenticate, submit transactions, view fraud predictions, access historical transaction data, and monitor fraud analytics through an interactive dashboard.

This project demonstrates modern .NET development practices, machine learning integration, RESTful API development, authentication and authorization, database management, and frontend-backend communication.

---

## Key Features

### User Authentication & Security

* User Registration
* User Login
* JWT Authentication
* Protected Routes
* User-specific Transaction History

### Fraud Detection

* Real-Time Fraud Prediction
* Fraud Probability Analysis
* Risk Classification
* Recommendation Generation
* Risk Explanation Engine

### Analytics Dashboard

* Fraud Trend Visualization
* Fraud Distribution Analysis
* Transaction Statistics
* Historical Fraud Monitoring

### Data Management

* SQL Server Database
* Entity Framework Core
* Database Migrations
* Transaction History Storage

---

## Technology Stack

### Backend

* C#
* .NET
* ASP.NET Core Web API
* Entity Framework Core
* LINQ
* JWT Authentication
* Dependency Injection

### Frontend

* React
* Vite
* Axios
* Chart.js

### Database

* SQL Server

### Machine Learning

* ML.NET
* LightGBM
* FastTree
* SDCA Logistic Regression

### Version Control

* Git
* GitHub

---

## System Architecture

```text
React Frontend
       │
       ▼
ASP.NET Core Web API
       │
       ├────────► ML.NET Prediction Engine
       │
       ▼
SQL Server Database
```

---

## Backend Architecture

### Controllers

Responsible for handling HTTP requests and returning API responses.

Examples:

* AuthController
* FraudDetectionController

### Services

Contains business logic and fraud prediction functionality.

Examples:

* IFraudPredictionService
* FraudPredictionService

### DTOs

Used to transfer data between client and server.

Examples:

* LoginRequestDto
* RegisterRequestDto
* TransactionRequestDto
* FraudPredictionResponseDto

### Entity Framework Core

Used for database access and object-relational mapping (ORM).

Examples:

* User Entity
* TransactionHistory Entity
* ApplicationDbContext

---

## Machine Learning Workflow

### Data Preparation

* Data Cleaning
* Feature Selection
* Feature Engineering

### Data Transformation

* Boolean to Numeric Conversion
* One-Hot Encoding
* Feature Concatenation

### Model Training

The following machine learning algorithms were evaluated:

1. FastTree
2. LightGBM
3. SDCA Logistic Regression

### Evaluation Metrics

* Accuracy
* Precision
* Recall
* F1 Score
* Area Under Curve (AUC)

---

## Model Comparison Results

| Model                    | Accuracy | Precision | Recall | F1 Score |
| ------------------------ | -------- | --------- | ------ | -------- |
| FastTree                 | 99.95%   | 99.64%    | 97.98% | 98.80%   |
| LightGBM                 | 99.95%   | 99.49%    | 98.23% | 98.86%   |
| SDCA Logistic Regression | 98.01%   | 0.00%     | 0.00%  | 0.00%    |

### Selected Production Model

LightGBM was selected as the final production model because it achieved the highest overall performance and provided the best balance between Precision, Recall, and F1 Score.

---

## Project Structure

```text
FraudDetectionSystem
│
├── FraudDetection.Api
│   ├── Controllers
│   ├── Services
│   ├── DTOs
│   ├── Entities
│   ├── Data
│   ├── Migrations
│   └── MLModels
│
├── FraudDetection.ML
│   ├── Data
│   ├── Models
│   ├── BalancedData
│   └── Training Pipeline
│
└── FraudDetection.UI
    ├── Components
    ├── Pages
    ├── Assets
    └── Services
```

---

## Running the Application

### Backend

```bash
dotnet restore
dotnet build
dotnet run
```

### Frontend

```bash
npm install
npm run dev
```

### Database Migration

```powershell
Update-Database
```

---

## Future Improvements

* Docker Containerization
* Docker Compose
* GitHub Actions CI/CD
* Kubernetes Deployment
* Azure Cloud Deployment
* Real-Time Fraud Streaming
* Explainable AI Integration

---

## Skills Demonstrated

* C#
* .NET
* ASP.NET Core Web API
* Entity Framework Core
* LINQ
* SQL Server
* JWT Authentication
* React
* REST API Development
* Machine Learning with ML.NET
* Software Architecture
* Full-Stack Development
* Git & GitHub

---

## Author

**Nikita Kolle**

Master's Student – Software Engineering

Fraud Detection System developed using C#, ASP.NET Core Web API, React, SQL Server, and ML.NET.
