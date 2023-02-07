// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

using AspNetIdentitySample.WebApplication.MappingProfiles;

var builder = WebApplication.CreateBuilder(args);

builder.Services.SetUpApplicationCore();
builder.Services.SetUpDatabase(builder.Configuration);
builder.Services.SetUpPipeline();
builder.Services.SetUpIdentity();
builder.Services.SetUpAuthorization();
builder.Services.AddAutoMapper(configuration => configuration.AddProfile<UserMappingProfile>());

var app = builder.Build();

app.SetUpDatabase();
app.SetUpUrlRewriter();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
