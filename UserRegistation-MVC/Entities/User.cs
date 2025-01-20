

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace UserRegistation_MVC.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        [Required()]
        public string Name { get; set; }
        [Required()]
        public string Surname { get; set; }
        [Required()]
        public DateTime BirthDate { get; set; }
        [Required()]
        public string Number { get; set; }
    }
}
