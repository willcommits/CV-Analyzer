# Docker Deployment Guide

This guide explains how to build and deploy the CV Analyzer backend API using Docker.

## Prerequisites

- Docker installed on your system
- OpenAI API key

## Building the Docker Image

### From the backend directory:

```bash
cd backend
docker build -t cv-analyzer-backend .
```

### From the root directory:

```bash
docker build -t cv-analyzer-backend -f backend/Dockerfile backend
```

## Running the Container Locally

### Option 1: Using Docker directly

```bash
docker run -d \
  -p 5161:5161 \
  -e OpenAI__ApiKey="your-openai-api-key-here" \
  --name cv-analyzer-backend \
  cv-analyzer-backend
```

### Option 2: Using docker-compose

1. Create a `.env` file in the root directory:
```bash
OPENAI_API_KEY=your-openai-api-key-here
```

2. Run docker-compose:
```bash
docker-compose up -d
```

## Testing the Container

Once the container is running, you can access:
- API: http://localhost:5161
- Swagger UI: http://localhost:5161/swagger (if running in Development mode)

Test the API endpoint:
```bash
curl http://localhost:5161/api/analyze-cv
```

## Deploying to Render

### Step 1: Push your code to GitHub

Ensure your code is pushed to a GitHub repository with the Dockerfile.

### Step 2: Create a new Web Service on Render

1. Go to [Render Dashboard](https://dashboard.render.com/)
2. Click "New +" and select "Web Service"
3. Connect your GitHub repository
4. Configure the service:
   - **Name**: cv-analyzer-backend
   - **Region**: Choose your preferred region
   - **Branch**: main (or your default branch)
   - **Root Directory**: backend
   - **Environment**: Docker
   - **Dockerfile Path**: Dockerfile (relative to backend directory)
   - **Docker Build Context Directory**: backend

### Step 3: Configure Environment Variables

Add the following environment variable in Render:
- **Key**: `OpenAI__ApiKey`
- **Value**: Your OpenAI API key

### Step 4: Configure Advanced Settings

- **Port**: The application will automatically use the PORT environment variable provided by Render
- **Health Check Path**: `/api/analyze-cv` (optional, for monitoring)

### Step 5: Deploy

Click "Create Web Service" and Render will automatically build and deploy your application.

## Environment Variables

The following environment variables can be configured:

| Variable | Description | Required | Default |
|----------|-------------|----------|---------|
| `OpenAI__ApiKey` | OpenAI API key for GPT integration | Yes | - |
| `ASPNETCORE_ENVIRONMENT` | Application environment (Development/Production) | No | Production |
| `PORT` | Port number (automatically set by Render) | No | 5161 |

## CORS Configuration

The backend is configured to accept requests from:
- `http://localhost:5173` (React dev server)
- `http://localhost:5174`
- `http://localhost:3000`
- `https://*.onrender.com` (Render deployments)

Update the CORS policy in `Program.cs` if you need to allow additional origins.

## Health Checks

The API doesn't have a dedicated health check endpoint, but you can use the POST endpoint `/api/analyze-cv` to verify the service is running. For production, consider adding a simple GET endpoint like `/health` that returns a 200 status.

## Troubleshooting

### Container fails to start
- Check logs: `docker logs cv-analyzer-backend`
- Verify environment variables are set correctly
- Ensure port 5161 is not already in use

### Build fails
- Ensure all project files are present in the backend directory
- Check that .dockerignore is not excluding necessary files
- Verify you have a stable internet connection for downloading NuGet packages

### API returns errors
- Verify the OpenAI API key is valid and has sufficient credits
- Check the logs for detailed error messages
- Ensure uploaded PDF files are valid and under 5MB

## Production Considerations

1. **Security**:
   - Never commit API keys to source control
   - Use environment variables for sensitive data
   - Keep dependencies up to date

2. **Performance**:
   - The multi-stage Docker build minimizes image size
   - Consider implementing caching for frequently accessed data
   - Monitor API usage and set appropriate rate limits

3. **Monitoring**:
   - Set up logging aggregation
   - Configure alerts for errors and high usage
   - Monitor API response times

## Updating the Deployment

To update the deployed application:

1. Make changes to your code
2. Commit and push to GitHub
3. Render will automatically detect changes and redeploy
4. Or manually trigger a deploy from the Render dashboard

## Stopping the Container

```bash
# Using Docker
docker stop cv-analyzer-backend
docker rm cv-analyzer-backend

# Using docker-compose
docker-compose down
```
