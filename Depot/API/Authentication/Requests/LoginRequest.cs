using System.ComponentModel.DataAnnotations;

namespace Depot.API.Authentication.Requests;

public record LoginRequest
(
    [Required(ErrorMessage = "User Name is required")]
    string? Username,

    [Required(ErrorMessage = "Password is required")]
    string? Password
);