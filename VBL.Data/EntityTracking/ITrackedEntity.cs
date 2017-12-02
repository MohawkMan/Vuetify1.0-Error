using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VBL.Data
{
    public interface ITrackedEntity
    {
        DateTime? DtCreated { get; set; }
        int? UserIdCreated { get; set; }
        DateTime? DtModified { get; set; }
        int? UserIdModified { get; set; }

        [Timestamp]
        byte[] RowVersion { get; set; }
    }
}
