// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Razor;

using AspNetIdentitySample.ApplicationCore.Entities;
using AspNetIdentitySample.WebApplication.Stores;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddControllersWithViews(options =>
{
  var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                                               .Build();
  var filter = new AuthorizeFilter(policy);

  options.Filters.Add(filter);
});
builder.Services.Configure<RazorViewEngineOptions>(options =>
{
  options.ViewLocationFormats.Clear();
  options.ViewLocationFormats.Add($"/Views/{{0}}{RazorViewEngine.ViewExtension}");
});
builder.Services.AddIdentity<UserEntity, RoleEntity>()
                .AddUserStore<UserStore>()
                .AddRoleStore<RoleStore>()
                .AddDefaultTokenProviders();
builder.Services.ConfigureApplicationCookie(options =>
{
  options.LoginPath = "/account/signin";
  options.ReturnUrlParameter = "returnUrl";
});

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.InitializeDatabase();

app.Run();
