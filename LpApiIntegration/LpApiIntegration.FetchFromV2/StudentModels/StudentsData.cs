﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpApiIntegration.FetchFromV2.StudentModels
{
    internal class StudentsData
    {
        public FullStudent[] Students { get; set; }
        public StudentsReferenceData ReferenceData { get; set; }
    }
}
