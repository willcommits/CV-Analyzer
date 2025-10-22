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

For production deployment, set the OpenAI API key as an environment variable instead of in appsettings.json:

```bash
export OpenAI__ApiKey="your-openai-api-key"
```

## Security Notes

- The OpenAI API key is currently in `appsettings.Development.json` for development
- For production, move this to environment variables or Azure Key Vault
- The application includes CORS configuration for localhost development
- File uploads are validated for type and size (PDF only, max 5MB)

## Troubleshooting

- Ensure .NET 8 SDK is installed
- Ensure Node.js v18+ is installed
- Check that ports 7095 and 5173 are not in use
- Verify OpenAI API key is correctly configured
- Check browser console for any CORS issues