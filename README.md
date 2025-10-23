# CV-to-Cover Letter Generator

A full-stack web application that generates personalized cover letters by analyzing a CV (PDF) and matching it with job descriptions using OpenAI's GPT technology.
The CV-to-Cover Letter Generator can be accessed [here](https://ornate-beijinho-ecf718.netlify.app/).

## Features

- **PDF Upload**: Drag-and-drop interface for CV uploads with validation (PDF only, max 5MB)
- **Job Description Input**: Multi-line text area for detailed job descriptions
- **AI-Powered Generation**: Uses OpenAI GPT-3.5-turbo to generate personalized cover letters
- **Real-time Processing**: Loading states and error handling for smooth user experience
- **Responsive Design**: Works seamlessly on desktop and mobile devices
- **Copy to Clipboard**: One-click copying of generated cover letters

## Technology Stack

### Backend (.NET 8)
- **ASP.NET Core Web API**: RESTful API with minimal API endpoints
- **iText7**: PDF text extraction library
- **OpenAI .NET SDK**: Integration with OpenAI's GPT models
- **Clean Architecture**: Separated into Core, Infrastructure, and API layers

### Frontend (React + TypeScript)
- **React 18**: Modern React with functional components and hooks
- **TypeScript**: Type-safe JavaScript for better development experience
- **Vite**: Fast build tool and development server
- **CSS3**: Custom responsive styling with modern design principles

## Project Structure

```
cv-analyzer/
├── backend/
│   ├── CvAnalyzer.Api/           # Web API project
│   ├── CvAnalyzer.Core/          # Business logic & models
│   └── CvAnalyzer.Infrastructure/ # PDF processing & AI services
├── frontend/
│   ├── src/
│   │   ├── components/           # React components
│   │   ├── services/            # API client
│   │   └── types/               # TypeScript interfaces
│   └── dist/                    # Production build
└── README.md
```

## Getting Started

### Prerequisites
- .NET 8 SDK
- Node.js (v18 or higher)
- OpenAI API key

### Backend Setup

1. Navigate to the backend directory:
   ```bash
   cd backend
   ```

2. Configure your OpenAI API key in `CvAnalyzer.Api/appsettings.Development.json`

3. Restore dependencies and build:
   ```bash
   dotnet build
   ```

4. Run the API:
   ```bash
   cd CvAnalyzer.Api
   dotnet run
   ```

   The API will be available at `https://localhost:7041`

### Frontend Setup

1. Navigate to the frontend directory:
   ```bash
   cd frontend
   ```

2. Install dependencies:
   ```bash
   npm install
   ```

3. Start the development server:
   ```bash
   npm run dev
   ```

   The frontend will be available at `http://localhost:5173`

## API Endpoints

### POST `/api/analyze-cv`

Analyzes a CV PDF and job description to generate a personalized cover letter.

**Request**: Multipart form data
- `cvFile`: PDF file (max 5MB)
- `jobDescription`: Job description text

**Response**: JSON
```json
{
  "coverLetter": "Generated cover letter text...",
  "success": true,
  "errorMessage": null
}
```

## How It Works

1. **PDF Processing**: The application extracts text from uploaded PDF files using iText7
2. **AI Analysis**: OpenAI's GPT-3.5-turbo analyzes the CV content and job description
3. **Cover Letter Generation**: The AI generates a 2-3 paragraph personalized cover letter
4. **User Experience**: Results are displayed with options to copy or start a new analysis

## Security Features

- File type validation (PDF only)
- File size limits (5MB maximum)
- CORS configuration for secure cross-origin requests
- Input validation and sanitization
- Error handling with user-friendly messages

## Development Approach

This application was built with a focus on:

- **Clean Architecture**: Separated concerns with distinct layers
- **Type Safety**: TypeScript throughout the frontend for better maintainability
- **Modern UI/UX**: Responsive design with intuitive user interactions
- **Error Handling**: Comprehensive error handling at all levels
- **Performance**: Efficient PDF processing and API calls
- **Security**: Input validation and secure API key management

## Production Deployment

### Backend

#### Using Docker (Deployed on Render)
The backend is fully containerized and ready for deployment.

**Important:** You **must** set the `OpenAI__ApiKey` environment variable (with double underscores) for the application to work. Get your API key from [OpenAI Platform](https://platform.openai.com/api-keys).

Quick start with Docker:
```bash
cd backend
docker build -t cv-analyzer-backend .
docker run -p 5161:5161 -e OpenAI__ApiKey="your-key" cv-analyzer-backend
```

Or using docker-compose:
```bash
# Create .env file with your OpenAI API key
docker-compose up -d
```
### Frontend
- Build for production: `npm run build`
- Deploy to Netlify, Vercel, or similar static hosting
- Configure API proxy for production backend URL
