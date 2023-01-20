﻿
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bookstore.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? ImageURL { get; set; }
        public int Price { get; set; }
        public int YearOfPublication { get; set; }
        public int AuthorId { get; set; }
        /*    public Author Author { get; set; }*/
        public int? OrderId { get; set; }
        /*       public Order? Order { get; set; }*/
        public int? BookGenreId { get; set; }
        public string? ApplicationUserId { get; set; }
        /*       public ApplicationUser? User { get; set; }*/
    }
}
