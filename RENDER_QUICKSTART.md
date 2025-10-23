# Quick Start: Deploy to Render

This guide will help you deploy the CV Analyzer backend API to Render in just a few steps.

## Prerequisites

1. A GitHub account with this repository
2. A Render account (free tier available at https://render.com)
3. An OpenAI API key (get one at https://platform.openai.com/api-keys)

## ⚠️ REQUIRED: Environment Variable Configuration

**YES, you MUST set the OpenAI API key as an environment variable on Render.**

The application **requires** the following environment variable to function:

| Variable Name | Value | Required |
|---------------|-------|----------|
| `OpenAI__ApiKey` | Your OpenAI API key | **YES** |

**Important Notes:**
- The variable name uses **double underscores** (`__`): `OpenAI__ApiKey`
- Without this variable, the application will fail to start with an error
- You can set this during deployment or add it later in the Render dashboard
- Never commit your API key to source control

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
   - **IMPORTANT**: Click on "Environment" tab and add the required variable:
     - Key: `OpenAI__ApiKey` (note the double underscores)
     - Value: Your OpenAI API key from https://platform.openai.com/api-keys
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

3. **Add Environment Variable** (REQUIRED)
   - Go to "Environment" section
   - Click "Add Environment Variable"
   - Key: `OpenAI__ApiKey` (exactly as shown, with double underscores)
   - Value: Your OpenAI API key (paste from https://platform.openai.com/api-keys)
   - **Without this, your application will not start!**

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

The backend is pre-configured to allow requests from:
- Local development servers (localhost:3000, 5173, 5174)
- Production frontend at `https://ornate-beijinho-ecf718.netlify.app`

If you deploy your frontend to a different domain, you have two options:

**Option 1: Using Environment Variable (Recommended)**

Add the `CORS_ALLOWED_ORIGINS` environment variable in Render:
- Go to your service in Render Dashboard
- Navigate to "Environment" section
- Click "Add Environment Variable"
- Key: `CORS_ALLOWED_ORIGINS`
- Value: Comma-separated list of your frontend URLs (e.g., `https://my-app.netlify.app,https://another-app.vercel.app`)
- Click "Save Changes"

**Option 2: Update Code**

Modify `backend/CvAnalyzer.Api/Program.cs` and add your domain to the `allowedOrigins` list:

```csharp
var allowedOrigins = new List<string>
{
    "http://localhost:5173", 
    "http://localhost:5174", 
    "http://localhost:3000",
    "https://ornate-beijinho-ecf718.netlify.app",
    "https://your-frontend-domain.com" // Add your domain here
};
```

Then commit and push the changes.

## Troubleshooting

### Build Fails
- Check logs in Render dashboard
- Ensure all files are committed to GitHub
- Verify Dockerfile path is correct

### Container Won't Start
- **Most common issue**: Check that `OpenAI__ApiKey` environment variable is set correctly
- Verify the variable name uses double underscores: `OpenAI__ApiKey` (not single underscore)
- Ensure your OpenAI API key is valid and active
- Review application logs for the specific error message
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
