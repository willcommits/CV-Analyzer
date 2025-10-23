# ANSWER: Do I Need to Pass OpenAI API Key on Render?

## Short Answer

**YES!** You absolutely MUST set the OpenAI API key as an environment variable on Render.

## The Variable Name

```
OpenAI__ApiKey
```

⚠️ **Note the DOUBLE underscores** (`__`) - this is critical!

## How to Set It on Render

### During Initial Deployment (Blueprint Method)

1. Go to https://dashboard.render.com
2. Click "New +" → "Blueprint"
3. Select your repository
4. When Render shows the configuration:
   - Click on the **"Environment"** tab
   - Add environment variable:
     - **Key**: `OpenAI__ApiKey`
     - **Value**: Your OpenAI API key from https://platform.openai.com/api-keys
5. Click "Apply"

### After Deployment or Manual Setup

1. Go to your service in Render Dashboard
2. Click on "Environment" in the left menu
3. Click "Add Environment Variable"
4. Enter:
   - **Key**: `OpenAI__ApiKey` (exactly as shown, with double underscores)
   - **Value**: Your actual OpenAI API key
5. Click "Save Changes"
6. Render will automatically redeploy your service with the new variable

## What Happens Without It?

If you don't set this variable:
- ❌ Your application will **fail to start**
- ❌ You'll see an error: "OpenAI API key not configured"
- ❌ The health check will fail
- ❌ Your service won't be accessible

## Common Mistakes to Avoid

1. ❌ Using single underscore: `OpenAI_ApiKey` - **Wrong!**
   ✅ Use double underscores: `OpenAI__ApiKey` - **Correct!**

2. ❌ Forgetting to set it at all
   ✅ Always set it before or immediately after deployment

3. ❌ Setting it in the wrong place (like in code or config files)
   ✅ Set it as an environment variable in Render's dashboard

4. ❌ Committing it to GitHub
   ✅ Only set it in Render's environment variables (secure)

## Why This Format?

The double underscore (`__`) is .NET's way of representing nested configuration:
- In JSON config: `"OpenAI": { "ApiKey": "..." }`
- As environment variable: `OpenAI__ApiKey`

## Need More Help?

See these guides for detailed instructions:
- [ENVIRONMENT_VARIABLES.md](ENVIRONMENT_VARIABLES.md) - Complete environment variable guide
- [RENDER_QUICKSTART.md](RENDER_QUICKSTART.md) - Step-by-step Render deployment
- [DOCKER_DEPLOYMENT.md](DOCKER_DEPLOYMENT.md) - Docker and deployment details

## Quick Reference

| What | Value |
|------|-------|
| **Must I set it?** | YES |
| **Variable name** | `OpenAI__ApiKey` |
| **Where to get key** | https://platform.openai.com/api-keys |
| **Where to set it** | Render Dashboard → Your Service → Environment |
| **What if I don't?** | Application won't start |
