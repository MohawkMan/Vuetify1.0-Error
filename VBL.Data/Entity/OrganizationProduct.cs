using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data
{
    public partial class OrganizationProduct

    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }

    }
}
