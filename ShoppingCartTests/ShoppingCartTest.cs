using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ShoppingCartTests
{
    [TestClass]
    public class ShoppingCartTest
    {
        private BookShelves _bookshelves = new BookShelves();

        [TestMethod]
        public void Series_1_Buy_1_Total_Price_Should_Equal_To_100()
        {
            // Arrange
            var targetShoppingCart = new ShoppingCart();
            targetShoppingCart.Add(_bookshelves["1"], 1);

            // Action
            decimal actualTotalPrice = targetShoppingCart.GetTotalPrice();
            decimal expectedTotalPrice = 100;

            // Assert
            actualTotalPrice.Should().Be(expectedTotalPrice);
        }

        [TestMethod]
        public void Series_1_Buy_1_And_Series_2_Buy_1_Total_Price_Should_Equal_190()
        {
            // Arrange
            var targetShoppingCart = new ShoppingCart();
            targetShoppingCart.Add(_bookshelves["1"], 1);
            targetShoppingCart.Add(_bookshelves["2"], 1);

            // Action
            decimal actualTotalPrice = targetShoppingCart.GetTotalPrice();
            decimal expectedTotalPrice = 100 * 2 * 0.95m;

            // Assert
            actualTotalPrice.Should().Be(expectedTotalPrice);
        }

        [TestMethod]
        public void Series_1_Buy_1_Series_2_Buy_1_Series_3_Buy_1_Total_Price_Should_Equal_270()
        {
            // Arrange
            var targetShoppingCart = new ShoppingCart();
            targetShoppingCart.Add(_bookshelves["1"], 1);
            targetShoppingCart.Add(_bookshelves["2"], 1);
            targetShoppingCart.Add(_bookshelves["3"], 1);

            // Action
            decimal actualTotalPrice = targetShoppingCart.GetTotalPrice();
            decimal expectedTotalPrice = 100 * 3 * 0.9m;

            // Assert
            actualTotalPrice.Should().Be(expectedTotalPrice);
        }

        [TestMethod]
        public void Series_1_Buy_1_Series_2_Buy_1_Series_3_Buy_1_Series_4_Buy_1_Total_Price_Should_Equal_320()
        {
            // Arrange
            var targetShoppingCart = new ShoppingCart();
            targetShoppingCart.Add(_bookshelves["1"], 1);
            targetShoppingCart.Add(_bookshelves["2"], 1);
            targetShoppingCart.Add(_bookshelves["3"], 1);
            targetShoppingCart.Add(_bookshelves["4"], 1);

            // Action
            decimal actualTotalPrice = targetShoppingCart.GetTotalPrice();
            decimal expectedTotalPrice = 100 * 4 * 0.8m;

            // Assert
            actualTotalPrice.Should().Be(expectedTotalPrice);
        }

        [TestMethod]
        public void Series_1_Buy_1_Series_2_Buy_1_Series_3_Buy_1_Series_4_Buy_1_Series_5_Buy_1_Total_Price_Should_Equal_375()
        {
            // Arrange
            var targetShoppingCart = new ShoppingCart();
            targetShoppingCart.Add(_bookshelves["1"], 1);
            targetShoppingCart.Add(_bookshelves["2"], 1);
            targetShoppingCart.Add(_bookshelves["3"], 1);
            targetShoppingCart.Add(_bookshelves["4"], 1);
            targetShoppingCart.Add(_bookshelves["5"], 1);

            // Action
            decimal actualTotalPrice = targetShoppingCart.GetTotalPrice();
            decimal expectedTotalPrice = 100 * 5 * 0.75m;

            // Assert
            actualTotalPrice.Should().Be(expectedTotalPrice);
        }

        [TestMethod]
        public void Series_1_Buy_1_Series_2_Buy_1_Series_3_Buy_2_Total_Price_Should_Equal_370()
        {
            // Arrange
            var targetShoppingCart = new ShoppingCart();
            targetShoppingCart.Add(_bookshelves["1"], 1);
            targetShoppingCart.Add(_bookshelves["2"], 1);
            targetShoppingCart.Add(_bookshelves["3"], 2);

            // Action
            decimal actualTotalPrice = targetShoppingCart.GetTotalPrice();
            decimal expectedTotalPrice = 100 * 3 * 0.9m + 100;

            // Assert
            actualTotalPrice.Should().Be(expectedTotalPrice);
        }

        [TestMethod]
        public void Series_1_Buy_1_Series_2_Buy_2_Series_3_Buy_2_Total_Price_Should_Equal_370()
        {
            // Arrange
            var targetShoppingCart = new ShoppingCart();
            targetShoppingCart.Add(_bookshelves["1"], 1);
            targetShoppingCart.Add(_bookshelves["2"], 2);
            targetShoppingCart.Add(_bookshelves["3"], 2);

            // Action
            decimal actualTotalPrice = targetShoppingCart.GetTotalPrice();
            decimal expectedTotalPrice = 100 * 3 * 0.9m + 100 * 2 * 0.95m;

            // Assert
            actualTotalPrice.Should().Be(expectedTotalPrice);
        }

        [TestMethod]
        public void None_Buy_Book_Total_Price_Should_Equal_0()
        {
            // Arrange
            var targetShoppingCart = new ShoppingCart();

            // Action
            decimal actualTotalPrice = targetShoppingCart.GetTotalPrice();
            decimal expectedTotalPrice = 0;

            // Assert
            actualTotalPrice.Should().Be(expectedTotalPrice);
        }
    }

    internal class ShoppingCart
    {
        private List<Books> _cart;

        public ShoppingCart()
        {
            _cart = new List<Books>();
        }

        public void Add(BookInfo bookInfo, int amount)
        {
            _cart.Add(new Books { Info = bookInfo, Amount = amount });
        }

        internal decimal GetTotalPrice()
        {
            var booksSets = new List<BooksSet>();
            foreach (var books in _cart)
            {
                for (int i = 0; i < books.Amount; i++)
                {
                    if (booksSets.Count <= i) booksSets.Add(new BooksSet());
                    var bookSet = booksSets[i];
                    bookSet.Add(books.Info);
                }
            }

            return booksSets.Sum(booksSet => booksSet.GetTotalPrice());
        }

        private class BooksSet : List<BookInfo>
        {
            private static Dictionary<int, decimal> discounts = new Dictionary<int, decimal>
            {
                {1, 1m},
                {2, 0.95m},
                {3, 0.9m},
                {4, 0.8m},
                {5, 0.75m},
            };

            public decimal GetTotalPrice()
            {
                var discount = discounts[Count];
                return this.Sum(book => book.Price) * discount;
            }
        }
    }

    internal class Books
    {
        public int Amount { get; set; }
        public BookInfo Info { get; set; }
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
                        Position = 1,
                        Price = 100
                    }
                },
                {
                    "2",
                    new BookInfo
                    {
                        ISBN = "2",
                        Name = "HARRY POTTER AND THE CHAMBER OF SECRETS",
                        SeriesOf = "Harry Potter",
                        Position = 2,
                        Price = 100
                    }
                },
                {
                    "3",
                    new BookInfo
                    {
                        ISBN = "3",
                        Name = "HARRY POTTER AND THE PRISONER OF AZKABAN",
                        SeriesOf = "Harry Potter",
                        Position = 3,
                        Price = 100
                    }
                },
                {
                    "4",
                    new BookInfo
                    {
                        ISBN = "4",
                        Name = "HARRY POTTER AND THE GOBLET OF FIRE",
                        SeriesOf = "Harry Potter",
                        Position = 4,
                        Price = 100
                    }
                },
                {
                    "5",
                    new BookInfo
                    {
                        ISBN = "5",
                        Name = "HARRY POTTER AND THE ORDER OF THE PHOENIX",
                        SeriesOf = "Harry Potter",
                        Position = 2,
                        Price = 100
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
        public decimal Price { get; set; }
    }
}



