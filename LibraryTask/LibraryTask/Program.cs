using System;

namespace LibraryTask
{
    class Program
    {
        static bool Who() // проверяет покупатель или продавец
        {
            Console.Write("Кто вы? >> ");
            string ui = Console.ReadLine().ToLower();
            if (ui == "продавец") return true;
            return false;
        }

        public static void Main(string[] args)
        {
            //Создаю 1 фирму с 3 филиалами как основу
            List<Book> books1 = new List<Book>();
            books1.Add(new Book(new Author("автор1"), "книга1", 10, 1000));
            books1.Add(new Book(new Author("автор2"), "книга2", 10, 1500));
            books1.Add(new Book(new Author("автор3"), "книга3", 10, 2000));

            List<Book> books2 = new List<Book>();
            books2.Add(new Book(new Author("автор1"), "книга1", 10, 2000));
            books2.Add(new Book(new Author("автор2"), "книга2", 10, 2500));
            books2.Add(new Book(new Author("автор3"), "книга3", 10, 3000));

            List<Book> books3 = new List<Book>();
            books3.Add(new Book(new Author("автор1"), "книга1", 10, 1500));
            books3.Add(new Book(new Author("автор2"), "книга2", 10, 2000));
            books3.Add(new Book(new Author("автор3"), "книга3", 10, 3000));

            List<Branch> branches = new List<Branch>();
            branches.Add(new Branch("адрес1",books1));
            branches.Add(new Branch("адрес2",books2));
            branches.Add(new Branch("адрес3",books3));

            Company lib = new Company("библиотека1",branches);
            //закончили упражнение

            //переходим к интерфейсу программы
            Console.WriteLine("Программа Библиотека Онлайн\n-----------------------");
            if (Who())
            {
                Console.Write("Вы авторизовались как продавец, введите адрес филиала >> ");
                string adress = Console.ReadLine().ToLower();
                Console.Write("Выберите действие >> ");
                string command = Console.ReadLine().ToLower();
                while (command != "выход")
                {
                    switch (command)
                    {
                        case "добавить": lib.branches.Find(item => item.adress == adress).AddBook(); break;
                        case "изменить цену": lib.branches.Find(item => item.adress == adress).ChangePrice(); break;
                        case "изменить кол": lib.branches.Find(item => item.adress == adress).ChangeQuantity(); break;
                        default: Console.WriteLine("Incorrect Input"); break; //аналог try-catch
                    }
                    Console.Write("Выберите действие или напишите выход >> ");
                    command = Console.ReadLine().ToLower();
                }
                Console.WriteLine("------------------\nВывод измененного каталога\n------------------");
                lib.branches.Find(item => item.adress == adress).PrintCatalog();
            }
            else
            {
                Console.Write("Вы были авторизованы как покупатель, введите адрес филиала >> ");
                string adress = Console.ReadLine().ToLower();

                lib.branches.Find(item => item.adress == adress).PrintCatalog();

                List<string> output = new List<string>();
                int sum = 0;

                Console.Write("-----------------\nВведите книгу и количество через пробел или автор и имя автора >> ");
                string[] temp = Console.ReadLine().ToLower().Split(' ');

                while (temp[0] != "выход")
                {
                    if (temp[0] == "автор")
                    {
                        Console.WriteLine("Каталог автора {0}\n------------", temp[1]);
                        foreach(Book book in lib.branches.Find(item => item.adress == adress).books.FindAll(item => item.author.name == temp[1]))
                        {
                            Console.WriteLine(book.name+" "+book.author.name+" "+book.quantity+" шт. "+book.price+" руб.");
                        }
                        Console.WriteLine("-----------------");
                        Console.Write("Введите книгу и количество через пробел или напишите выход >> ");
                        temp = Console.ReadLine().ToLower().Split(' ');
                    }
                    else
                    {
                        try
                        {
                            if (Convert.ToInt32(temp[1]) > lib.branches.Find(item => item.adress == adress).books.Find(item => item.name == temp[0]).quantity)
                            {
                                temp[1] = Convert.ToString(lib.branches.Find(item => item.adress == adress).books.Find(item => item.name == temp[0]).quantity);
                            }

                            sum += lib.branches.Find(item => item.adress == adress).books.Find(item => item.name == temp[0]).price * Convert.ToInt32(temp[1]);

                            lib.branches.Find(item => item.adress == adress).books.Find(item => item.name == temp[0]).quantity -= Convert.ToInt32(temp[1]);

                            output.Add(lib.branches.Find(item => item.adress == adress).books.Find(item => item.name == temp[0]).name + " " + lib.branches.Find(item => item.adress == adress).books.Find(item => item.name == temp[0]).price + " * " + temp[1] + " = " + lib.branches.Find(item => item.adress == adress).books.Find(item => item.name == temp[0]).price * Convert.ToInt32(temp[1]));
                        }
                        catch
                        {
                            Console.WriteLine("Incorrect Input");
                        }
                        Console.Write("Введите книгу и количество через пробел или напишите выход >> ");
                        temp = Console.ReadLine().ToLower().Split(' ');
                    }
                }
                Console.WriteLine("\nВывод чека\n---------------------");
                foreach(string str in output)
                {
                    Console.WriteLine(str);
                }
                Console.WriteLine("--------------------\nИтог: "+sum);
            }
        }
    }
}