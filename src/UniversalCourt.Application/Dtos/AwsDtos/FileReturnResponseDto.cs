﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalCourt.Application.Dtos.AwsDtos
{
    public class FileReturnResponseDto
    {
        public string Message { get; set; }
        public string Key { get; set; }
        public string Url { get; set; }
    }
}
