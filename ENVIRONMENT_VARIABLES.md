# Environment Variables Configuration

## Quick Answer

**YES**, you **MUST** set the OpenAI API key as an environment variable when deploying to Render (or any other platform).

### Variable Name

```
OpenAI__ApiKey
```

**Important:** Use **double underscores** (`__`), not single underscore.

### Where to Get Your API Key

Get your OpenAI API key from: https://platform.openai.com/api-keys

## For Render Deployment

When deploying to Render, you need to set the environment variable:

1. **During Blueprint Deployment:**
   - After selecting your repository, click on the "Environment" tab
   - Add environment variable:
     - Key: `OpenAI__ApiKey`
     - Value: Your actual OpenAI API key

2. **After Deployment (or Manual Setup):**
   - Go to your service in Render Dashboard
   - Navigate to "Environment" section
   - Click "Add Environment Variable"
   - Key: `OpenAI__ApiKey`
   - Value: Your actual OpenAI API key
   - Click "Save Changes"
   - Your service will automatically redeploy with the new variable

## For Local Development

### Using Docker

```bash
docker run -p 5161:5161 -e OpenAI__ApiKey="your-actual-api-key" cv-analyzer-backend
```

### Using docker-compose

Create a `.env` file in the root directory:

```env
OPENAI_API_KEY=your-actual-api-key
```

Then run:
```bash
docker-compose up
```

### Using dotnet run

Set the environment variable before running:

**Linux/Mac:**
```bash
export OpenAI__ApiKey="your-actual-api-key"
cd backend/CvAnalyzer.Api
dotnet run
```

**Windows (PowerShell):**
```powershell
$env:OpenAI__ApiKey="your-actual-api-key"
cd backend\CvAnalyzer.Api
dotnet run
```

**Windows (CMD):**
```cmd
set OpenAI__ApiKey=your-actual-api-key
cd backend\CvAnalyzer.Api
dotnet run
```

## Why Double Underscores?

The double underscore (`__`) is .NET's convention for representing nested configuration keys in environment variables:

- Configuration JSON: `"OpenAI": { "ApiKey": "..." }`
- Environment Variable: `OpenAI__ApiKey`

This allows the application to read from either:
1. `appsettings.json` with key `OpenAI:ApiKey`
2. Environment variable `OpenAI__ApiKey`

## What Happens Without It?

If you don't set the `OpenAI__ApiKey` environment variable, the application will:
1. Fail to start
2. Show an error message: "OpenAI API key not configured. Please set OpenAI__ApiKey environment variable or OpenAI:ApiKey in appsettings.json"

## Security Best Practices

✅ **DO:**
- Store API keys as environment variables
- Use Render's secure environment variable storage
- Keep your API keys private and secure
- Rotate API keys periodically

❌ **DON'T:**
- Commit API keys to source control
- Share API keys in public forums or screenshots
- Hardcode API keys in your application code
- Use the same API key across multiple environments

## All Environment Variables

| Variable | Required | Default | Purpose |
|----------|----------|---------|---------|
| `OpenAI__ApiKey` | **YES** | None | OpenAI API authentication |
| `ASPNETCORE_ENVIRONMENT` | No | Production | Environment mode (Development/Production) |
| `PORT` | No | 5161 | Port the application listens on |
| `CORS_ALLOWED_ORIGINS` | No | See below | Additional CORS origins (comma-separated) |

### CORS Configuration

The application has built-in CORS support for:
- `http://localhost:5173` (Vite dev server)
- `http://localhost:5174` (Alternative Vite port)
- `http://localhost:3000` (Create React App)
- `https://ornate-beijinho-ecf718.netlify.app` (Production frontend)

To add additional allowed origins, set the `CORS_ALLOWED_ORIGINS` environment variable with a comma-separated list:

```bash
CORS_ALLOWED_ORIGINS=https://my-app.netlify.app,https://another-app.vercel.app
```

**Note:** When deploying a new frontend on a different domain, add it to `CORS_ALLOWED_ORIGINS` to avoid CORS errors.

## Troubleshooting

### "OpenAI API key not configured" Error

**Solution:** Set the `OpenAI__ApiKey` environment variable with your actual API key.

### Variable Name Doesn't Work

**Common mistake:** Using single underscore (`OpenAI_ApiKey`) instead of double (`OpenAI__ApiKey`)

**Solution:** Make sure you're using **double underscores**.

### Application Still Not Working

Check:
1. Variable name is exactly: `OpenAI__ApiKey` (case-sensitive)
2. API key is valid and active on OpenAI platform
3. API key has sufficient credits/quota
4. Service has been redeployed after adding the variable (on Render)

## Need Help?

- Render deployment: See [RENDER_QUICKSTART.md](RENDER_QUICKSTART.md)
- Docker deployment: See [DOCKER_DEPLOYMENT.md](DOCKER_DEPLOYMENT.md)
- Local development: See [README.md](README.md)
