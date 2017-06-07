using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ShoppingCartTests
{
    [TestClass]
    public class ShoppingCartTest
    {
        private BookShelves _bookshelves = new BookShelves();

        [TestMethod]
        public void Series_1_To_Buy_1_Total_Price_Should_Equal_To_100()
        {
            // Arrange
            var targetShoppingCart = new ShoppingCart();

            // Action

            // Assert
        }
    }

    internal class ShoppingCart
    {
        public ShoppingCart()
        {
        }
    }

    internal class BookShelves
    {
        private Dictionary<string, BookInfo> _bookshelves;

        public BookShelves()
        {
            _bookshelves = new Dictionary<string, BookInfo>
            {
                {
                    "1",
                    new BookInfo
                    {
                        ISBN = "1",
                        Name = "HARRY POTTER AND THE SORCERER’S STONE",
                        SeriesOf = "Harry Potter",
                        Position = 1
                    }
                },
                {
                    "2",
                    new BookInfo
                    {
                        ISBN = "2",
                        Name = "HARRY POTTER AND THE CHAMBER OF SECRETS",
                        SeriesOf = "Harry Potter",
                        Position = 2
                    }
                },
                {
                    "3",
                    new BookInfo
                    {
                        ISBN = "3",
                        Name = "HARRY POTTER AND THE PRISONER OF AZKABAN",
                        SeriesOf = "Harry Potter",
                        Position = 3
                    }
                },
                {
                    "4",
                    new BookInfo
                    {
                        ISBN = "4",
                        Name = "HARRY POTTER AND THE GOBLET OF FIRE",
                        SeriesOf = "Harry Potter",
                        Position = 4
                    }
                },
                {
                    "5",
                    new BookInfo
                    {
                        ISBN = "5",
                        Name = "HARRY POTTER AND THE ORDER OF THE PHOENIX",
                        SeriesOf = "Harry Potter",
                        Position = 2
                    }
                }
            };
        }

        public BookInfo this[string bookId]
        {
            get
            {
                return _bookshelves[bookId];
            }
        }
    }

    internal class BookInfo
    {
        public string ISBN { get; set; }
        public string Name { get; set; }
        public int Position { get; set; }
        public string SeriesOf { get; internal set; }
    }
}

