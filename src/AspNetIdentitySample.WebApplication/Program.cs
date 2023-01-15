// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

using Microsoft.AspNetCore.Identity;

using AspNetIdentitySample.ApplicationCore.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentity<UserEntity, RoleEntity>()
                .AddDefaultTokenProviders();

var app = builder.Build();
app.Run();
