﻿using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using diegomoreno.Brq.domain.Entities;
using diegomoreno.Brq.Repository.Mappings;
using DomainValidation.Validation;

namespace diegomoreno.Brq.Repository.Contexts.Entity;

[ExcludeFromCodeCoverage]
public class TrucksDbContext : DbContext
{

    public TrucksDbContext()
    {

    }
    public TrucksDbContext(DbContextOptions<TrucksDbContext> options) : base(options)
    {

    }

    public DbSet<Truck> Trucks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TruckMapping());
        modelBuilder.Ignore<ValidationResult>();
        base.OnModelCreating(modelBuilder);
    }
}