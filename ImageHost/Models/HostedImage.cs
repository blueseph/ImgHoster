﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ImageHost.Static_Classes;

namespace ImageHost.Models
{
    public class HostedImage
    {
        public virtual int Id { get; set; }

        public string EncodedName
        {
            get { return Base62.ToBase(this.Id); }
        }

        public string FileName { get; set; }
        public string FullServerPath { get; set; }
    }
}