using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FMMSReportAPI.Models
{
    public class FaultModel
    {
        public string from { get; set; }
        public string to { get; set; }
        public string fault { get; set; }
        public string discoId { get; set; }
    }
}