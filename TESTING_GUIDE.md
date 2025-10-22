# Testing the Docker Build

This guide helps you verify the Docker containerization is working correctly before deploying to Render.

## Prerequisites

- Docker Desktop installed and running
- Git repository cloned locally
- OpenAI API key

## Local Testing Steps

### 1. Test Docker Build

Navigate to the project root and build the Docker image:

```bash
cd CV-Analyzer
docker build -t cv-analyzer-backend:test -f backend/Dockerfile backend
```

Expected output: Build should complete successfully with no errors.

### 2. Run the Container

```bash
docker run -d \
  -p 5161:5161 \
  -e OpenAI__ApiKey="your-openai-api-key" \
  -e ASPNETCORE_ENVIRONMENT=Development \
  --name cv-analyzer-test \
  cv-analyzer-backend:test
```

### 3. Verify Container is Running

```bash
docker ps
```

You should see `cv-analyzer-test` in the list of running containers.

### 4. Check Container Logs

```bash
docker logs cv-analyzer-test
```

Expected output should include:
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://0.0.0.0:5161
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
```

### 5. Test Health Check Endpoint

```bash
curl http://localhost:5161/health
```

Expected response:
```json
{
  "status": "healthy",
  "timestamp": "2024-10-22T19:00:00.000Z"
}
```

### 6. Test API Endpoint (Optional)

You can test the main endpoint with a PDF file:

```bash
curl -X POST http://localhost:5161/api/analyze-cv \
  -F "cvFile=@/path/to/your/cv.pdf" \
  -F "jobDescription=Software Engineer position..."
```

Expected response (if API key is valid):
```json
{
  "coverLetter": "Generated cover letter text...",
  "success": true,
  "errorMessage": null
}
```

### 7. Clean Up

Stop and remove the test container:

```bash
docker stop cv-analyzer-test
docker rm cv-analyzer-test
```

Remove the test image (optional):

```bash
docker rmi cv-analyzer-backend:test
```

## Testing with Docker Compose

For a simpler testing experience:

1. Create a `.env` file in the project root:
   ```
   OPENAI_API_KEY=your-openai-api-key
   ```

2. Start the service:
   ```bash
   docker-compose up
   ```

3. Test the endpoints (in another terminal):
   ```bash
   curl http://localhost:5161/health
   ```

4. Stop the service:
   ```bash
   docker-compose down
   ```

## Common Issues and Solutions

### Build Fails

**Issue**: "Failed to restore packages"
- **Solution**: Check internet connection, NuGet sources might be temporarily unavailable

**Issue**: "Cannot find Dockerfile"
- **Solution**: Ensure you're running the build command from the project root, or adjust the path

### Container Won't Start

**Issue**: "Port 5161 already in use"
- **Solution**: Stop other services using that port or use a different port:
  ```bash
  docker run -p 8080:5161 ...
  ```

**Issue**: "OpenAI API key not configured"
- **Solution**: Ensure the environment variable is set correctly:
  ```bash
  docker run -e OpenAI__ApiKey="sk-..." ...
  ```

### API Returns Errors

**Issue**: 500 error when calling `/api/analyze-cv`
- **Solution**: Check the OpenAI API key is valid and has credits
- Check container logs for detailed error messages

**Issue**: CORS errors in browser
- **Solution**: The frontend origin needs to be added to CORS policy in `Program.cs`

## Verification Checklist

Before deploying to Render, verify:

- [ ] Docker build completes successfully
- [ ] Container starts without errors
- [ ] Health check endpoint returns 200 OK
- [ ] Container logs show no errors
- [ ] API accepts PDF uploads (with valid OpenAI key)
- [ ] Environment variables are correctly configured
- [ ] Port mapping works correctly

## Next Steps

Once local testing is successful:

1. Push changes to GitHub
2. Follow the [RENDER_QUICKSTART.md](RENDER_QUICKSTART.md) guide
3. Deploy to Render using the Blueprint or manual method
4. Test the deployed service using the Render URL

## Troubleshooting Render Deployment

If the deployment fails on Render:

1. Check the build logs in Render dashboard
2. Verify environment variables are set correctly
3. Ensure the Dockerfile path is correct in render.yaml
4. Review the [DOCKER_DEPLOYMENT.md](DOCKER_DEPLOYMENT.md) for detailed configuration

## Performance Notes

- First build takes 5-10 minutes due to downloading .NET SDK and packages
- Subsequent builds are faster due to Docker layer caching
- Container size: ~200-250 MB (optimized with multi-stage build)
- Startup time: 2-5 seconds (cold start on Render free tier: ~30 seconds)
