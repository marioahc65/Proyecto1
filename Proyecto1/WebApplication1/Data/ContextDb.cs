using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Proyecto1API.Models;

namespace Proyecto1API.Data;

public partial class ContextDb : DbContext
{
    public ContextDb()
    {
    }

    public ContextDb(DbContextOptions<ContextDb> options)
        : base(options) => Database.EnsureCreated();

    public virtual DbSet<Clinica> Clinicas { get; set; }

    public virtual DbSet<Enfermedade> Enfermedades { get; set; }

    public virtual DbSet<Medico> Medicos { get; set; }

    public virtual DbSet<MedicoPaciente> MedicoPacientes { get; set; }

    public virtual DbSet<Paciente> Pacientes { get; set; }

    public virtual DbSet<Vacuna> Vacunas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=SQLServerConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Clinica>(entity =>
        {
            entity.HasKey(e => e.ClinicaId).HasName("PK__Clinica__08A0EBDE3D5422F1");

            entity.ToTable("Clinica");

            entity.Property(e => e.ClinicaId).HasColumnName("Clinica_Id");
            entity.Property(e => e.Cjuridica).HasColumnName("CJuridica");
            entity.Property(e => e.Correo).IsUnicode(false);
            entity.Property(e => e.Distrito).IsUnicode(false);
            entity.Property(e => e.Estado).IsUnicode(false);
            entity.Property(e => e.Nombre).IsUnicode(false);
            entity.Property(e => e.Pais).IsUnicode(false);
            entity.Property(e => e.SitioWeb).IsUnicode(false);
        });

        modelBuilder.Entity<Enfermedade>(entity =>
        {
            entity.HasKey(e => e.EnfermedadesId).HasName("PK__Enfermed__5F8383560E8BF52B");

            entity.Property(e => e.EnfermedadesId).HasColumnName("Enfermedades_Id");
            entity.Property(e => e.PacienteId).HasColumnName("Paciente_Id");
            entity.Property(e => e.Sintomas).IsUnicode(false);
            entity.Property(e => e.Alergias).IsUnicode(false);
            entity.Property(e => e.NuevasEnfermedades).IsUnicode(false);
            entity.Property(e => e.OtrasCondiciones).IsUnicode(false);
            entity.Property(e => e.RCancer).IsUnicode(false);
            entity.Property(e => e.OtrosSintomas).IsUnicode(false);



            entity.HasOne(d => d.Paciente).WithMany(p => p.Enfermedades)
                .HasForeignKey(d => d.PacienteId)
                .HasConstraintName("FK__Enfermeda__Pacie__403A8C7D");
        });

        modelBuilder.Entity<Medico>(entity =>
        {
            entity.HasKey(e => e.MedicoId).HasName("PK__Medico__9550B5911AB7CC9A");

            entity.ToTable("Medico");

            entity.Property(e => e.MedicoId).HasColumnName("Medico_Id");
            entity.Property(e => e.Apellido1).IsUnicode(false);
            entity.Property(e => e.Apellido2).IsUnicode(false);
            entity.Property(e => e.Cprofesional).HasColumnName("CProfesional");
            entity.Property(e => e.Email).IsUnicode(false);
            entity.Property(e => e.Estado).IsUnicode(false);
            entity.Property(e => e.Nombre).IsUnicode(false);
            entity.Property(e => e.Pais).IsUnicode(false);
        });

        modelBuilder.Entity<MedicoPaciente>(entity =>
        {
            entity.HasKey(e => e.MedicoPacienteId).HasName("PK__MedicoPa__B4C328B1DB53503A");

            entity.ToTable("MedicoPaciente");

            entity.Property(e => e.MedicoPacienteId).HasColumnName("MedicoPaciente_Id");
            entity.Property(e => e.Fregistro)
                .HasColumnType("date")
                .HasColumnName("FRegistro");
            entity.Property(e => e.MedicoId).HasColumnName("Medico_Id");
            entity.Property(e => e.PacienteId).HasColumnName("Paciente_Id");
            entity.Property(e => e.ClinicaId).HasColumnName("Clinica_Id");

            entity.HasOne(d => d.Medico).WithMany(p => p.MedicoPacientes)
                .HasForeignKey(d => d.MedicoId)
                .HasConstraintName("FK__MedicoPac__Medic__440B1D61");

            entity.HasOne(d => d.Paciente).WithMany(p => p.MedicoPacientes)
                .HasForeignKey(d => d.PacienteId)
                .HasConstraintName("FK__MedicoPac__Pacie__4316F928");
            entity.HasOne(d => d.Clinica).WithMany(p => p.MedicoPacientes)
                .HasForeignKey(d => d.ClinicaId)
                .HasConstraintName("FK__MedicoPac__Clini");
        });

        modelBuilder.Entity<Paciente>(entity =>
        {
            entity.HasKey(e => e.PacienteId).HasName("PK__Paciente__B784FC24CA9AFA6A");

            entity.ToTable("Paciente");

            entity.Property(e => e.PacienteId).HasColumnName("Paciente_Id");
            entity.Property(e => e.Apellido1).IsUnicode(false);
            entity.Property(e => e.Apellido2).IsUnicode(false);
            entity.Property(e => e.Distrito).IsUnicode(false);
            entity.Property(e => e.Ecivil)
                .IsUnicode(false)
                .HasColumnName("ECivil");
            entity.Property(e => e.Email).IsUnicode(false);
            entity.Property(e => e.Estado).IsUnicode(false);
            entity.Property(e => e.Fnacimiento)
                .HasColumnType("date")
                .HasColumnName("FNacimiento");
            entity.Property(e => e.Nombre).IsUnicode(false);
            entity.Property(e => e.Ocupacion).IsUnicode(false);
            entity.Property(e => e.Pais).IsUnicode(false);
            entity.Property(e => e.Sexo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<Vacuna>(entity =>
        {
            entity.HasKey(e => e.VacunasId).HasName("PK__Vacunas__0E37FBC895F5EE86");

            entity.Property(e => e.VacunasId).HasColumnName("Vacunas_Id");
            entity.Property(e => e.IsHepatitisAb).HasColumnName("IsHepatitisAB");
            entity.Property(e => e.PacienteId).HasColumnName("Paciente_Id");
            entity.Property(e => e.RazonCovid).IsUnicode(false);

            entity.HasOne(d => d.Paciente).WithMany(p => p.Vacunas)
                .HasForeignKey(d => d.PacienteId)
                .HasConstraintName("FK__Vacunas__Pacient__3D5E1FD2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
