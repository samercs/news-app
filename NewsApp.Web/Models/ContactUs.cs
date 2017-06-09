using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NewsApp.Web.Models
{
    public class ContactUs
    {
        public int ContactUsId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        [DefaultValue(false)]
        public bool IsRead { get; set; }
        [DefaultValue(typeof(DateTime), "")]
        public DateTime AddedDate { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

    }
}