// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetIdentitySample.WebApplication.Defaults
{
  /// <summary>Provides values of routes.</summary>
  public static class Routing
  {
    /// <summary>A value that represents a base route for the user endpoints.</summary>
    public const string UserRoute = "user";

    /// <summary>A value that represents a base route for the account endpoints.</summary>
    public const string AccountRoute = "account";

    /// <summary>A value that represents an endpoint for an access denied page.</summary>
    public const string AccessDeniedEndpoint = "access-denied";

    /// <summary>A value that represents an endpoint for a sign in page.</summary>
    public const string SignInEndpoint = "signin";

    /// <summary>A value that represents an endpoint for a sign out page.</summary>
    public const string SignOutEndpoint = "signout";

    /// <summary>A value that represents an endpoint for a user page.</summary>
    public const string UserEndpoint = "{userId}";

    /// <summary>A value that represents an endpoint for a new user page.</summary>
    public const string NewUserEndpoint = "new";
  }
}
