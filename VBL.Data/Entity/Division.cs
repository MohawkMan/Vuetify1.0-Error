﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VBL.Data
{
    public partial class Division 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte Order { get; set; }
        public bool IsPublic { get; set; }
    }
}