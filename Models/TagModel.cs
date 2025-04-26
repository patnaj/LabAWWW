using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Lab2.Models
{
    public class TagModel
    {
        [Key]
        [DisplayName("Nazwa")]
        public string Title { get; set; } = "";
    }
}