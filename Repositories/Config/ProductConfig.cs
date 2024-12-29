using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Config
{
    public class ProductConfig : IEntityTypeConfiguration<Products>
    {
        public void Configure(EntityTypeBuilder<Products> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.SurName).IsRequired();

            builder.HasData(
                new Products() { Id = 1, Name = "Ahmet", SurName = "Ahmet", Address = "Address", PhoneNumber = "05061111111", ShowCase=false, CategoryId = 1, EducationStatusId = 1, ReferanceId = 1},
                new Products() { Id = 2, Name = "Mehmet", SurName = "Mehmet", Address = "Address", PhoneNumber = "05061111111", ShowCase = false, CategoryId = 1, EducationStatusId = 1, ReferanceId = 2 },
                new Products() { Id = 3, Name = "Celal", SurName = "Celal", Address = "Address", PhoneNumber = "05061111111", ShowCase = false , CategoryId = 2, EducationStatusId = 2, ReferanceId = 3 },
                new Products() { Id = 4, Name = "İsa", SurName = "Celal", Address = "Address", PhoneNumber = "05061111111", ShowCase = false , CategoryId = 2, EducationStatusId = 3, ReferanceId = 4 },
                new Products() { Id = 5, Name = "Musa", SurName = "Celal", Address = "Address", PhoneNumber = "05061111111", ShowCase = true, CategoryId = 3, EducationStatusId = 4, ReferanceId = 5 },
                new Products() { Id = 6, Name = "Batu", SurName = "Celal", Address = "Address", PhoneNumber = "05061111111", ShowCase = true, CategoryId = 3, EducationStatusId = 5, ReferanceId = 6 },
                new Products() { Id = 7, Name = "Akif", SurName = "Celal", Address = "Address", PhoneNumber = "05061111111", ShowCase = true , CategoryId = 3, EducationStatusId = 6, ReferanceId = 2 }
                );
        }
    }
}
