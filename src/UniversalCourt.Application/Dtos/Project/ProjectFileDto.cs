﻿using UniversalCourt.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UniversalCourt.Application.Dtos.Project
{
    public class ProjectFileDto
    { 
        public string Title { get; set; }
          public string Url { get; set; }
        
    }
}
