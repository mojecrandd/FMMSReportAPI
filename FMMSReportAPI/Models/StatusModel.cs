﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FMMSReportAPI.Models
{
    public class StatusModel
    {
        public string from { get; set; }
        public string to { get; set; }
        public string status { get; set; }
        public string discoId { get; set; }
    }
}