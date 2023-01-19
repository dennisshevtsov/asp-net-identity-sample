namespace AspNetIdentitySample.WebApplication.ViewModels
{
  /// <summary>Represents a base of an account view model.</summary>
  public abstract class AccountViewModelBase
  {
    /// <summary>Gets/sets an object that represents a URL to that the app should return after an successful login.</summary>
    [FromQuery]
    public string ReturnUrl { get; set; } = "/";
  }
}
