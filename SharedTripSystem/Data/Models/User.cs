using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static SharedTripSystem.Data.DataConstants.Default;
namespace SharedTripSystem.Data.Models
{
    public class User:IdentityUser
    {
        [MaxLength(FullNameMaxLength)]
        public string FullName { get; set; }
    }
}
