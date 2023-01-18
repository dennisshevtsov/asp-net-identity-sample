// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;

using AspNetIdentitySample.ApplicationCore.Entities;
using AspNetIdentitySample.WebApplication.Stores;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.Configure<RazorViewEngineOptions>(options =>
{
  options.ViewLocationFormats.Clear();
  options.ViewLocationFormats.Add($"/Views/{{0}}{RazorViewEngine.ViewExtension}");
});
builder.Services.AddIdentity<UserEntity, RoleEntity>()
                .AddUserStore<UserStore>()
                .AddRoleStore<RoleStore>()
                .AddDefaultTokenProviders();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.MapControllers();

app.Run();
