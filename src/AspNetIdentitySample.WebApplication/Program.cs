// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

using Microsoft.AspNetCore.Rewrite;

var builder = WebApplication.CreateBuilder(args);

builder.Services.SetUpDatabase(builder.Configuration);
builder.Services.SetUpPipeline();
builder.Services.SetUpIdentity();
builder.Services.SetUpAuthorization();

var app = builder.Build();

app.UseStaticFiles();
app.UseRewriter(new RewriteOptions().AddRedirect("(.*)/$", "$1")
                                    .AddRedirect("^[/]?$", "home"));
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.SetUpDatabase();

app.Run();
