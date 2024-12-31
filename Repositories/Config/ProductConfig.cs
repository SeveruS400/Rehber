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

            builder.HasData(
                new Products() { Id = 1, Name = "Ahmet", Address = "Address", PhoneNumber = "05061111111", ShowCase=false, CategoryId = 1, EducationStatusId = 1, ReferanceId = 1, ConnStatus = false },
                new Products() { Id = 2, Name = "Mehmet", Address = "Address", PhoneNumber = "05061111111", ShowCase = false, CategoryId = 1, EducationStatusId = 1, ReferanceId = 2, ConnStatus = false },
                new Products() { Id = 3, Name = "Celal", Address = "Address", PhoneNumber = "05061111111", ShowCase = false , CategoryId = 2, EducationStatusId = 2, ReferanceId = 3, ConnStatus = false },
                new Products() { Id = 4, Name = "İsa",  Address = "Address", PhoneNumber = "05061111111", ShowCase = false , CategoryId = 2, EducationStatusId = 3, ReferanceId = 4, ConnStatus = false },
                new Products() { Id = 5, Name = "Musa", Address = "Address", PhoneNumber = "05061111111", ShowCase = true, CategoryId = 3, EducationStatusId = 4, ReferanceId = 5, ConnStatus = false },
                new Products() { Id = 6, Name = "Batu", Address = "Address", PhoneNumber = "05061111111", ShowCase = true, CategoryId = 3, EducationStatusId = 5, ReferanceId = 6 , ConnStatus = false },
                new Products() { Id = 7, Name = "Akif", Address = "Address", PhoneNumber = "05061111111", ShowCase = true , CategoryId = 3, EducationStatusId = 6, ReferanceId = 2, ConnStatus = false }
                );
        }
    }
}
