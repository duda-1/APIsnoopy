namespace APIsnoopy.Models.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Artista> Artistas { get; set; }
    public DbSet<Album> Albuns { get; set; }
    public DbSet<Musica> Musicas { get; set; }
    public DbSet<Playlist> Playlists { get; set; }
    public DbSet<PlaylistMusica> PlaylistMusicas { get; set; }
}