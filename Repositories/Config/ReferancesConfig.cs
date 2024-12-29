using Entities.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Repositories.Config
{
    internal class ReferancesConfig : IEntityTypeConfiguration<Referance>
    {

        public void Configure(EntityTypeBuilder<Referance> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.ReferanceName).IsRequired();

            builder.HasData(
                new Referance() { Id = 1, ReferanceName = "Ahmet" },
                new Referance() { Id = 2, ReferanceName = "Mehmet" },
                new Referance() { Id = 3, ReferanceName = "Emin" },
                new Referance() { Id = 4, ReferanceName = "Kerem" },
                new Referance() { Id = 5, ReferanceName = "Yusuf" },
                new Referance() { Id = 6, ReferanceName = "Mahsun" }
            );
        }
    }
}
