# SECURITY NOTICE - API Key Exposure

## ⚠️ CRITICAL SECURITY ISSUE RESOLVED

### What Happened

An OpenAI API key was accidentally committed to the repository in the file `backend/CvAnalyzer.Api/appsettings.Development.json`. This key has been removed from the repository.

### Actions Taken

1. ✅ Removed hardcoded API key from `appsettings.Development.json`
2. ✅ Updated `.gitignore` to prevent future commits of sensitive configuration files
3. ✅ Created `appsettings.Development.json.example` as a template file
4. ✅ Updated documentation to emphasize proper API key management

### Actions Required

**🔴 IMPORTANT: The exposed API key MUST be revoked immediately!**

If you are the owner of the OpenAI API key that was committed:

1. **Revoke the exposed API key** at https://platform.openai.com/api-keys
   - The key starts with: `sk-proj-qsODwJG9rt9v9bdqO3bsKCGLMnNYqJvtiDlvNEAJpEL9RhcUj8NXKNgrXtS4UAOZUEab...`
   
2. **Generate a new API key** from the OpenAI platform

3. **Set the new key as an environment variable** (NEVER commit it):
   ```bash
   # For local development
   export OpenAI__ApiKey="your-new-api-key-here"
   
   # Or create appsettings.Development.json locally (this file is now in .gitignore)
   cp backend/CvAnalyzer.Api/appsettings.Development.json.example backend/CvAnalyzer.Api/appsettings.Development.json
   # Then edit and add your key
   ```

4. **For production/Render deployment**, set the environment variable:
   - Variable name: `OpenAI__ApiKey` (note the double underscores)
   - See [OPENAI_API_KEY_ANSWER.md](OPENAI_API_KEY_ANSWER.md) for detailed instructions

### Why This Matters

- **Financial Risk**: Exposed API keys can be used by others, potentially incurring charges to your account
- **Security Risk**: Unauthorized access to your OpenAI resources
- **Best Practice Violation**: API keys and secrets should NEVER be committed to version control

### Prevention

The following measures are now in place:

1. **`.gitignore` Updated**: `appsettings.Development.json` and similar files are now ignored
2. **Template File**: Use `appsettings.Development.json.example` as a reference
3. **Documentation**: Clear instructions on proper API key management
4. **Code Review**: Always check for secrets before committing

### How to Use API Keys Properly

#### For Local Development

**Option 1: Environment Variable (Recommended)**
```bash
export OpenAI__ApiKey="your-api-key-here"
cd backend/CvAnalyzer.Api
dotnet run
```

**Option 2: Local Configuration File**
```bash
# Copy the example file
cp backend/CvAnalyzer.Api/appsettings.Development.json.example backend/CvAnalyzer.Api/appsettings.Development.json

# Edit the file and add your API key
# This file will NOT be committed (it's in .gitignore)
```

#### For Production/Render

Set the environment variable `OpenAI__ApiKey` in your deployment platform. See [OPENAI_API_KEY_ANSWER.md](OPENAI_API_KEY_ANSWER.md) for step-by-step instructions.

### Additional Resources

- [OpenAI API Keys Management](https://platform.openai.com/api-keys)
- [ENVIRONMENT_VARIABLES.md](ENVIRONMENT_VARIABLES.md) - Environment variable guide
- [RENDER_QUICKSTART.md](RENDER_QUICKSTART.md) - Deployment instructions

### Questions?

If you have any questions about this security issue or need help setting up your API key properly, please create an issue in the repository.

---

**Last Updated**: 2025-10-23  
**Severity**: Critical  
**Status**: Resolved (with required action)
