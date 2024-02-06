using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models
{
    public class User
    {
        public string Id {get; set;}
        public string LocaleId {get; set;}
        public string Name {get; set;}
        public string Address {get; set;}
        public string Zip {get; set;}
        public string Country {get; set;}
        public string Region {get; set;}
        public int Status {get; set;}
        public string Password {get; set;}
        public string Picture {get; set;}
        public string Email {get; set;}
        public string Phone {get; set;}
        public string Code {get; set;}
        public string Idfile {get; set;}
        public string Token {get; set;}
        public string Idno {get; set;}
        public string Idtype {get; set;}
        // public DateTime Created {get; set;}
        public bool IsDeleting { get; set; }

         public User(User user)
        {
            Name = user.Name;
            Id = user.Id;
            Email = user.Email;
        }
        
         public User()
        {
        }
    }
}