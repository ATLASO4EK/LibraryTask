using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryTask
{
    public class Author // класс автора книги
    {
        public string name { get; set; }
        public Author(string name)
        {
            this.name = name;
        }
    }
    public class Book // класс книги
    {
        public string name { get; set; }
        public int quantity { get; set; }
        public int price { get; set; }

        public Author author { get; set; }

        public Book(Author author, string name, int quantity, int price)
        {
            this.name = name;
            this.quantity = quantity;
            this.price = price;
            this.author = author;
        }
    }
    public class Branch // класс филиала
    {
        public string adress { get; set; }

        public List<Book> books { get; set; }

        public Branch(string adress, List<Book> books)
        {
            this.adress = adress;
            this.books = books;
        }
        //методы для интерфейса продавца
        public void AddBook()
        {
            Console.Write("Введите автора, название, количество, цену через пробел >> ");
            string[] temp = Console.ReadLine().ToLower().Split(' ');
            try
            {
                books.Add(new Book(new Author(temp[0]), temp[1], Convert.ToInt32(temp[2]), Convert.ToInt32(temp[3])));
            }
            catch
            {
                Console.WriteLine("Incorrect Input");
            }
        }
        public void PrintCatalog()
        {
            Console.WriteLine("Каталог\n-----------------");
            foreach (Book book in books)
            {
                Console.WriteLine(book.name + " " + book.author.name + " " + book.quantity + " шт.  " + book.price + " руб.");
            }
        }
        public void ChangePrice()
        {
            Console.Write("Введите название книги и новую цену через пробел >> ");
            string[] temp = Console.ReadLine().ToLower().Split(' ');
            try
            {
                books.Find(item => item.name == temp[0]).price = Convert.ToInt32(temp[1]);
            }
            catch
            {
                Console.WriteLine("Incorrect Input");
            }
        }
        public void ChangeQuantity()
        {
            Console.Write("Введите название книги и новое количество через пробел >> ");
            string[] temp = Console.ReadLine().ToLower().Split(' ');
            try
            {
                books.Find(item => item.name == temp[0]).quantity = Convert.ToInt32(temp[1]);
            }
            catch
            {
                Console.WriteLine("Incorrect Input");
            }
        }
    }

        public class Company // класс компании, типо разные библиотеки можно делать хъ)
        {
            public string name { get; set; }
            public List<Branch> branches { get; set; }

            public Company(string name, List<Branch> branches)
            {
                this.name = name;
                this.branches = branches;
            }
        }
}
