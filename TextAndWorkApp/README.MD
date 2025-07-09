## Configuration

This application requires the following configuration values to be set:

- Authentication:Microsoft:ClientId
- Authentication:Microsoft:ClientSecret
- Authentication:Google:ClientId
- Authentication:Google:ClientSecret
- Stripe:SecretKey
- ConnectionStrings:DefaultConnection

These can be provided through:
- User Secrets (development)
- Environment Variables (production)
- Azure Key Vault or similar (production)