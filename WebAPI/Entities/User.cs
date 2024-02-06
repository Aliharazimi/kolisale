namespace WebApi.Entities;
using System.Text.Json.Serialization;
using WebApi.Models.Products;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public int RoleId { get; set; }
    public string Role { get; set; }
    public int CartId { get; set; }
    public Cart Cart { get; set; }

    [JsonIgnore]
    public string PasswordHash { get; set; }

}