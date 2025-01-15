var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.AuthWebService>("authwebservice");

builder.Build().Run();
