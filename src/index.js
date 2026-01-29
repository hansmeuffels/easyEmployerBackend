/**
 * Cloudflare Worker Entrypoint Stub
 * 
 * IMPORTANT: This repository contains a C# ASP.NET Core backend application,
 * which is NOT compatible with Cloudflare Workers without additional tooling.
 * 
 * Cloudflare Workers is designed for JavaScript/TypeScript applications.
 * To deploy a .NET backend, consider:
 * 
 * 1. Using traditional hosting (Azure App Service, AWS, etc.)
 * 2. Using Cloudflare Workers with WASI support (experimental)
 * 3. Creating a JavaScript/TypeScript proxy that forwards to your .NET backend
 * 4. Rewriting the backend in JavaScript/TypeScript
 * 
 * This stub exists only to satisfy Wrangler's entrypoint requirement.
 */

export default {
  async fetch(request, env, ctx) {
    return new Response(
      JSON.stringify({
        error: "Configuration Error",
        message: "This is a C# ASP.NET Core backend. It cannot run directly on Cloudflare Workers.",
        documentation: "See README.md for proper deployment instructions.",
        backend_type: ".NET Core 10.0",
        suitable_platforms: ["Azure App Service", "AWS", "Google Cloud Run", "Traditional hosting"]
      }),
      {
        status: 503,
        headers: {
          "Content-Type": "application/json"
        }
      }
    );
  }
};
