
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class User
{
    [Key]
    public int UserId { get; set; }// entry id 

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;


    [Required]
    [MinLength(5, ErrorMessage = "first name must have at least five characters")]
    public string FName { get; set; }

    [Required]
    [MinLength(2, ErrorMessage = "Last name must have at least 2 characters")]
    public string LName { get; set; }


    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MinLength(8, ErrorMessage = "Your Password must be 8 characters")]
    [DataType(DataType.Password)]
    public string Pass { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "please confirm your passcode")]
        [Compare("Pass")]
        [DataType(DataType.Password)]
    public string ConfPasword { get; set; }

}
    public class LogUser
    {
        [Required(ErrorMessage = "enter the email")]
        [EmailAddress(ErrorMessage = "please enter a valid email it has @ .com ")]
        public string logEm { get; set; }


        [Required(ErrorMessage = "A Password is needed ")]
        [MinLength(10, ErrorMessage = "10 characters needed here")]
        [DataType(DataType.Password)]
        public string logPass { get; set; }

    }






