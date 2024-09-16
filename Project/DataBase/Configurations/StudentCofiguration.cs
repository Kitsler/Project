﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.DataBase.Helpers;
using Project.Models;

namespace Project.DataBase.Configurations
{
	public class StudentCofiguration : IEntityTypeConfiguration<Student>
	{
		private const string TableName = "cd_student";

		public void Configure(EntityTypeBuilder<Student> builder)
		{
			builder.HasKey(p => p.StudentId).HasName($"pk_{TableName}_student_id");

			builder.Property(p => p.StudentId).ValueGeneratedOnAdd();

			builder.Property(p => p.StudentId).HasColumnName("student_id").HasComment("Идентификатор записи студента");

			builder.Property(p => p.FirstName).IsRequired().HasColumnName("c_student_firstname")
				.HasColumnType(ColumnType.String).HasMaxLength(100)
				.HasComment("Имя Студента");

			builder.Property(p=>p.LastName).IsRequired().HasColumnName("c_student_lastname")
				.HasColumnType(ColumnType.String).HasMaxLength(100)
				.HasComment("Фамилия Студента");

			builder.ToTable(TableName)
				.HasOne(p => p.Group).WithMany()
				.HasForeignKey(p => p.GroupId)
				.HasConstraintName("fk_f_group_id")
				.OnDelete(DeleteBehavior.Cascade);

			builder.ToTable(TableName).HasIndex(p => p.StudentId, $"idx_{TableName}_fk_f_group_id");

			builder.Navigation(p => p.Group).AutoInclude();
		}

	}
}
