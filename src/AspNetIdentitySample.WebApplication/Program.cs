// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

using Microsoft.AspNetCore.Identity;

using AspNetIdentitySample.ApplicationCore.Entities;
using AspNetIdentitySample.WebApplication.Stores;

var builder = WebApplication.CreateBuilder(args);

builder.Services.SetUpDatabase(builder.Configuration);
builder.Services.SetUpMvcPipeline();
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
