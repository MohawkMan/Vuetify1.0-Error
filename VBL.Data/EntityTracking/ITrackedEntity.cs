using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VBL.Data
{
    public interface ITrackedEntity
    {
        DateTime? DtCreated { get; set; }
        string UserIdCreated { get; set; }
        [Timestamp]
        byte[] DtModified { get; set; }
        string UserIdModified { get; set; }
    }
}
