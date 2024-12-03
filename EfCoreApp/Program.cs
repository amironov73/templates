using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

using var db = new KladovkaContext();
var books = db.Books;

var count = books.AsNoTracking().Count();
Console.WriteLine ($"Book count: {count}");

var mironBooks = books.AsNoTracking()
    .Where (one => one.Ticket == "р-1")
    .ToArray();

foreach (var book in mironBooks)
{
    Console.WriteLine ($"Book {book.Number} on {book.Moment}");
}

Console.WriteLine ("ALL DONE!");

internal sealed class KladovkaContext : DbContext
{
    public DbSet<Book> Books { get; set; } = null!;

    protected override void OnConfiguring
        (
            DbContextOptionsBuilder builder
        )
    {
        const string connectionString =
            "Data Source=172.20.1.2;Initial Catalog=a;"
            + "User ID=b;Password=c;"; 
        
        var connection = new SqlConnection (connectionString);

        builder.UseSqlServer (connection)
            .LogTo (Console.WriteLine, LogLevel.Information);
    }
}

[Table ("podsob")]
internal sealed class Book
{
    [Key]
    [Column ("invent")]
    public required int Number { get; set; }

    [MaxLength (50)] 
    [Column ("chb")] 
    public string? Ticket { get; set; }

    [MaxLength (50)]
    [Column ("ident")] 
    public string? Identifier { get; set; }

    [MaxLength (50)]
    [Column ("whe")] 
    public string? Moment { get; set; }

    [Column ("operator")] 
    public int? Operator { get; set; }

    [Column ("srok")] 
    public DateTime? Deadline { get; set; }

    [Column ("prodlen")] 
    public int? Prolongation { get; set; }

    [MaxLength (50)]
    [Column ("onhand")] 
    public string? OnHands { get; set; }

    [MaxLength (250)]
    [Column ("alert")] 
    public string? Alert { get; set; }

    [Column ("pilot")] 
    public char? Pilot { get; set; }

    [MaxLength (50)]
    [Column("sigla")] 
    public string? Sigla { get; set; }

    [Column ("seen")] 
    public DateTime? Seen { get; set; }
}
