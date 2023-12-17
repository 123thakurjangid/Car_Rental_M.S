using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental.Business.Model
{
    public class LoginModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "* Please Enter Email !!")]
        public string? User_Email { get; set; }
        [Required(ErrorMessage = "* Please Enter Password !!")]
        public string? User_Password { get; set; }
        public string? AttachmentUrl { get; set; }
        public IFormFile? Attachmentfile { get; set; }
    }
}
