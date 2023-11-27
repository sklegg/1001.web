namespace Web1001.Models.Forms;
using System.ComponentModel.DataAnnotations;  

public class LoginFormModel {
    [EmailAddress]
    [Required]
    public string? Email { get; set; }
}