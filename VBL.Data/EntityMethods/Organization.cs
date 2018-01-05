using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VBL.Data
{
    public partial class Organization
    {
        public List<string> CoverPhotoUrls => Photos.Where(w => w.IsCover).Select(s => s.Url).ToList();
    }
}
