A sample Web API:
- Language: C# with Visual Studio in ASP.NET Core
- Connect port: 8088
- Function: saves the header and body of the request in 2 files. Create file name with timestamp.
- Requirement: No exact URL and no defined protocol is required for addressing the web API. 
Any request with any protocol (e.g. SOAP, JSON, etc.) arriving on port 8088 must be accepted.

Method: 
- Use Middleware to access Http request in pineline.

Reference: 
- https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-5.0
- https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware/write?view=aspnetcore-5.0
