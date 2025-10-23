# Running the CV-to-Cover Letter Application

## Quick Start Guide

### 1. Backend Setup (.NET API)

Open a terminal and navigate to the backend API directory:

```bash
cd "backend/CvAnalyzer.Api"
dotnet run
```

The API will start at `https://localhost:7095` and `http://localhost:5161`
- Swagger documentation will be available at: `https://localhost:7095/swagger`

### 2. Frontend Setup (React)

Open a second terminal and navigate to the frontend directory:

```bash
cd frontend
npm run dev
```

The React application will start at `http://localhost:5173`

### 3. Using the Application

1. Open your browser to `http://localhost:5173`
2. Upload a PDF CV file using the drag-and-drop interface
3. Enter a job description in the text area
4. Click "Generate Cover Letter"
5. Wait for the AI to process and generate your personalized cover letter
6. Copy the result to your clipboard or create another cover letter

## Production Build

### Backend
```bash
cd backend
dotnet publish -c Release
```

### Frontend
```bash
cd frontend
npm run build
```

The built files will be in the `dist/` folder and can be deployed to any static hosting service.

## Environment Variables

⚠️ **IMPORTANT**: Never commit API keys to the repository. See [SECURITY_NOTICE.md](SECURITY_NOTICE.md) for details.

### Required Configuration

The OpenAI API key **must** be set as an environment variable:

```bash
export OpenAI__ApiKey="your-openai-api-key"
```

**OR** create a local configuration file (this file is in .gitignore and won't be committed):

```bash
cd backend/CvAnalyzer.Api
cp appsettings.Development.json.example appsettings.Development.json
# Then edit the file and add your API key
```

Get your API key from: https://platform.openai.com/api-keys

## Security Notes

- **API Key Protection**: Never commit API keys - always use environment variables or secure configuration stores
- The OpenAI API key must be set as an environment variable (see above)
- For production, use Azure Key Vault, AWS Secrets Manager, or similar secure storage
- The application includes CORS configuration for localhost development
- File uploads are validated for type and size (PDF only, max 5MB)
- See [SECURITY_NOTICE.md](SECURITY_NOTICE.md) for security best practices

## Troubleshooting

- Ensure .NET 8 SDK is installed
- Ensure Node.js v18+ is installed
- Check that ports 7095 and 5173 are not in use
- Verify OpenAI API key is correctly configured
- Check browser console for any CORS issues