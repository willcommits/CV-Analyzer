# Quick Start: Deploy to Render

This guide will help you deploy the CV Analyzer backend API to Render in just a few steps.

## Prerequisites

1. A GitHub account with this repository
2. A Render account (free tier available at https://render.com)
3. An OpenAI API key

## Deployment Steps

### Option 1: One-Click Deploy with Blueprint (Recommended)

1. **Push to GitHub**
   ```bash
   git push origin main
   ```

2. **Deploy to Render**
   - Go to https://dashboard.render.com
   - Click "New +" → "Blueprint"
   - Select your repository
   - Render will detect the `render.yaml` file
   - Add your `OpenAI__ApiKey` as an environment variable
   - Click "Apply"

3. **Wait for Deployment**
   - Render will automatically build the Docker image
   - First deployment takes 5-10 minutes
   - You'll get a URL like: `https://cv-analyzer-backend.onrender.com`

4. **Test Your Deployment**
   ```bash
   curl https://your-app-url.onrender.com/health
   ```

### Option 2: Manual Deployment

1. **Create Web Service**
   - Go to Render Dashboard
   - Click "New +" → "Web Service"
   - Connect your GitHub repository

2. **Configure Service**
   - **Name**: cv-analyzer-backend
   - **Region**: Choose closest to you
   - **Branch**: main
   - **Root Directory**: backend
   - **Runtime**: Docker
   - **Dockerfile Path**: Dockerfile

3. **Add Environment Variable**
   - Key: `OpenAI__ApiKey`
   - Value: Your OpenAI API key

4. **Deploy**
   - Click "Create Web Service"
   - Wait for build to complete

## Post-Deployment

### Update Frontend

Update your frontend to point to the new backend URL:

```javascript
// In your frontend API service
const API_URL = 'https://your-app-url.onrender.com';
```

### Monitor Your Application

- View logs in the Render dashboard
- Check health endpoint: `/health`
- Monitor API usage on OpenAI dashboard

### Update CORS (if needed)

If deploying the frontend to a different domain, update CORS in `Program.cs`:

```csharp
policy.WithOrigins(
    "https://your-frontend-domain.com",
    "https://*.onrender.com"
)
```

## Troubleshooting

### Build Fails
- Check logs in Render dashboard
- Ensure all files are committed to GitHub
- Verify Dockerfile path is correct

### Container Won't Start
- Check that `OpenAI__ApiKey` is set
- Review application logs
- Verify health check endpoint returns 200

### API Errors
- Validate OpenAI API key is correct
- Check API has sufficient credits
- Review error logs in Render

## Free Tier Limitations

Render's free tier includes:
- 750 hours/month of runtime
- Services spin down after 15 minutes of inactivity
- First request after spin-down takes ~30 seconds
- 512 MB RAM
- Shared CPU

For production use, consider upgrading to a paid plan.

## Updating Your Deployment

To deploy updates:
```bash
git add .
git commit -m "Your update message"
git push origin main
```

Render will automatically detect changes and redeploy.

## Support

For issues:
- Check [Render Documentation](https://render.com/docs)
- Review application logs
- See DOCKER_DEPLOYMENT.md for detailed information
