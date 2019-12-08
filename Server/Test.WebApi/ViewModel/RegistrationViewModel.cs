using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Test.Entities.Models;
using Test.WebApi.Config;

namespace Test.WebApi.ViewModel
{
    public class RegistrationViewModel
    {
        public IFormFile file { get; set; }
        //public string ModelData { get; set; }

        [FromJson]
        public User ModelData { get; set; }
    }
}