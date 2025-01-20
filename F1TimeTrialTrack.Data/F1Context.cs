﻿using F1TimeTrialTrack.Entities;
using F1TimeTrialTrack.Entities.Dtos.Tracks;
using F1TimeTrialTrack.Entities.Entity_Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace F1TimeTrialTrack.Data
{
    public class F1Context : IdentityDbContext
    {
        public DbSet<Tracks> Tracks { get; set; }
        public DbSet<TracksRating> TracksRating { get; set; }
        public DbSet<TTs> TTs { get; set; }
        public DbSet<TTsRating> TTsRating { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<UploadedFile> UploadedFiles { get; set; }

        public F1Context(DbContextOptions<F1Context>ctx): base(ctx)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Tracks>()
                .HasMany(t=>t.TracksRatings)
                .WithOne(tr => tr.Track)
                .HasForeignKey(tr => tr.TrackId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<TTs>()
                .HasMany(t => t.TTsRatings)
                .WithOne(tr => tr.TTs)
                .HasForeignKey(tr => tr.TTsId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UploadedFile>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FileName).IsRequired();
                entity.Property(e => e.FilePath).IsRequired();
                entity.Property(e => e.TrackName).IsRequired();
               
            });

            base.OnModelCreating(builder);
        }
    }
}
