using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace ImgHoster.Models.Data
{
    public class HostedImageConfiguration : EntityTypeConfiguration<HostedImage>
    {
        public HostedImageConfiguration()
        {
            ToTable("img_hostedimages");
            Property(p => p.FileName);
            Property(p => p.FullServerPath);
        }
    }
}