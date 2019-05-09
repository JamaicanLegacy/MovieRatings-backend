using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRatings.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
            Actors = this.Set<Actor>();
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Actors = this.Set<Actor>();

        }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<MediaHouse> MediaHouses { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<MovieActor> MovieActors { get; set; }
        public DbSet<MovieDirector> MovieDirectors { get; set; }
        public DbSet<MovieMediaHouse> MovieMediaHouses { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MovieMediaHouse>()
                .ToTable("MoviesMediaHouse");

            modelBuilder.Entity<MovieDirector>()
                .ToTable("MoviesDirectors");

            modelBuilder.Entity<MovieGenre>()
                .ToTable("MovieGenre");

            modelBuilder.Entity<MovieActor>()
                .ToTable("MoviesActors");

            modelBuilder.Entity<MovieMediaHouse>()
                .HasKey(mmh => new { mmh.MovieId, mmh.MediaHouseId });

            modelBuilder.Entity<MovieDirector>()
                .HasKey(md => new { md.MovieId, md.DirectorId });

            modelBuilder.Entity<MovieGenre>()
                .HasKey(mg => new { mg.MovieId, mg.GenreId });

            modelBuilder.Entity<MovieActor>()
                .HasKey(ma => new { ma.MovieId, ma.ActorId });

            modelBuilder.Entity<Rating>()
                .HasKey(r => r.MovieId);

            // Many to Many
            modelBuilder.Entity<MovieMediaHouse>()
                .HasOne(mh => mh.MediaHouse)
                .WithMany(m => m.MovieMediaHouses)
                .HasForeignKey(mh => mh.MediaHouseId);

            modelBuilder.Entity<MovieMediaHouse>()
                .HasOne(mh => mh.Movie)
                .WithMany(m => m.MovieMediaHouses)
                .HasForeignKey(mh => mh.MovieId);

            modelBuilder.Entity<MovieActor>()
                .HasOne(ma => ma.Actor)
                .WithMany(m => m.MovieActors)
                .HasForeignKey(ma => ma.ActorId);

            modelBuilder.Entity<MovieActor>()
                .HasOne(ma => ma.Movie)
                .WithMany(m => m.MovieActors)
                .HasForeignKey(ma => ma.MovieId);

            modelBuilder.Entity<MovieDirector>()
                .HasOne(md => md.Director)
                .WithMany(m => m.MovieDirectors)
                .HasForeignKey(md => md.DirectorId);

            modelBuilder.Entity<MovieDirector>()
                .HasOne(md => md.Movie)
                .WithMany(m => m.MovieDirectors)
                .HasForeignKey(md => md.MovieId);

            modelBuilder.Entity<MovieGenre>()
                 .HasOne(mg => mg.Genre)
                 .WithMany(m => m.MovieGenres)
                 .HasForeignKey(mg => mg.GenreId);

            modelBuilder.Entity<MovieGenre>()
                .HasOne(mg => mg.Movie)
                .WithMany(m => m.MovieGenres)
                .HasForeignKey(mg => mg.MovieId);


            //modelBuilder.Entity<Movie>()
            //    .HasOne(r => r.Rating)
            //    .WithOne(m => m.Movie).IsRequired(false)
            //    ;




        }
    }
}
