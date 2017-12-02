using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VBL.Data
{
    public partial class AgeType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsPublic { get; set; } = false;
    }
}
