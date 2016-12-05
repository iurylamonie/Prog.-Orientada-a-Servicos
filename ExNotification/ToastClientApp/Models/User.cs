﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace ToastClientApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Uri { get; set; }
        public override string ToString()
        {
            return $"{Id} - {Nome} - {Uri}";
        }
    }
}