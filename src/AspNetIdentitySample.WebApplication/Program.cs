// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

var builder = WebApplication.CreateBuilder(args);

builder.Services.SetUpDatabase(builder.Configuration);
builder.Services.SetUpMvcPipeline();
builder.Services.SetUpIdentity();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.InitializeDatabase();

app.Run();
