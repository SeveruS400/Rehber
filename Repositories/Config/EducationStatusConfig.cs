using Entities.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Config
{
	public class EducationStatusConfig : IEntityTypeConfiguration<EducationStatus>
	{

		public void Configure(EntityTypeBuilder<EducationStatus> builder)
		{
			builder.HasKey(p => p.Id);
			builder.Property(p => p.EducationStatusName).IsRequired();

			builder.HasData(
				new EducationStatus() { Id = 1, EducationStatusName = "İlkokul" },
				new EducationStatus() { Id = 2, EducationStatusName = "Ortaokul" },
				new EducationStatus() { Id = 3, EducationStatusName = "Lise" },
				new EducationStatus() { Id = 4, EducationStatusName = "Üniversite" },
				new EducationStatus() { Id = 5, EducationStatusName = "Yüksek Lisans" },
				new EducationStatus() { Id = 6, EducationStatusName = "Doktora" }
			);
		}
	}
}
