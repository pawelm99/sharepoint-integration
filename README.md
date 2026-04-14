📁 SharePoint Permission Assignment API
🔗 Overview

This integration enables automated assignment of folder permissions in SharePoint via a secure API endpoint using OAuth 2.0.
It is designed to be triggered from Microsoft Power Automate and uses cloud-native services for security and scalability.

⚙️ Architecture

The solution consists of the following components:
Power Automate – event trigger and orchestration
API (Azure Function / Web API) – business logic layer
Azure Key Vault – secure storage for secrets
Microsoft Entra ID – OAuth 2.0 authentication provider
Microsoft Graph API – SharePoint access and permission management
🔐 Authentication

The API is secured using OAuth 2.0 (Client Credentials Flow).

Token Request

Clients must obtain an access token from Microsoft Entra ID:

POST https://login.microsoftonline.com/{tenant-id}/oauth2/v2.0/token
Required Header
Authorization: Bearer <access_token>
🔑 Secret Management

Sensitive values such as client_secret are securely stored in:

Azure Key Vault

The API retrieves secrets dynamically at runtime, ensuring no credentials are hardcoded.

📡 API Endpoint
POST /assignPermissionToUser

Assigns permissions to a user for a specific folder in SharePoint.

Request Body
{
  "userName": "user@company.com",
  "folderPath": "/Shared Documents/FolderA"
}

Headers
Authorization: Bearer <access_token>
Content-Type: application/json

🔄 Flow
Power Automate detects an event (e.g., user added to a team)
Retrieves an OAuth 2.0 access token from Microsoft Entra ID
Sends a request to the API endpoint

The API:
Retrieves secrets from Azure Key Vault
Authenticates with Microsoft Graph API
Assigns permissions to the specified SharePoint folder

🧠 Design Principles

🔐 Security-first – OAuth 2.0 + Key Vault

⚡ Event-driven – triggered by Power Automate

🧩 Decoupled architecture – automation separated from business logic

📦 Scalable – serverless or API-based backend

🚀 Technologies Used
Microsoft Power Automate
Microsoft Entra ID
Azure Key Vault
Microsoft Graph API
SharePoint
