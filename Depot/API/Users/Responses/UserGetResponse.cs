﻿namespace Depot.API.Users.Responses;

public class UserGetResponse
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Role { get; set; }
    public int EmployeeId { get; set; }
}