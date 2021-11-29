using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeCaveConsole
{
    class Program
    {
        // Do's and Dont's
        /// <summary>
        /// 
        /// - regarding class and Method names you should use Pascal casing. e.g MainActivity.
        /// 
        /// - use camel casing for args and local var's e.g firstName || userAnswer.
        /// 
        /// - avoid abbreviations e.g userCommand !usrCmd || userControl !usrCtrl 
        //    avoiding abbreviations makes it much easier for the next person to read.

        /// - dont use numbers at the start of a variable.e.g int 1Goal = 5; 
        //    instead user the number at the end of the var e.g int goal1 = 5;
        /// - Try to avoid capital { String, Int32, Boolean }. 
        //    makes code more natural to read and also consistemt with the.Net framework.
        /// 
        /// - Try to use nouns for classes. coz they are objects and objects are usually nouns.
        //    as for methods they are actions so u can use verbs.
        /// 
        /// - learn more about c# coding standards go to doFactory.com coding standards.
        /// 
        /// </summary>

        static void Main(string[] args)
        {
            #region Change Console Color
            /*Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear(); */
            #endregion

            #region MyRegion
            //1. // Explain cw Using directive.
            Console.WriteLine("Welcome to the Code Cave. C# Basics 101 survival skills.");

            //2. // Method? is a code block that contains a series of statements.

            //3. // Understand Method syntax
            ///  < Access Specifier > <return type > < Method name > (paramater list)
            //   {
            //       Method Body
            //   }
            //
            // Always remember to {return} when working with return types.
            //

            //4. //Error Handling
            // Break Points 
            // Try Catch Finally


            // Lets Code Now
            /*string name = GreetMe();
            Console.WriteLine($"Welcome {name}");
            PrintAge();
            PrintUserInfo(name);*/
            /*SelectFavouriteMovie();
            Console.ReadKey();*/
            #endregion

            Console.WriteLine("Hi Please enter your Password.");
            string password = Console.ReadLine();
            VerifyPassword(password);
            Console.ReadKey();
        }

        private static void VerifyPassword(string password)
        {
            if (!string.IsNullOrWhiteSpace(password))
            {
                if (password.Equals(Helper.MyPassword))
                {
                    Console.WriteLine("The password entered is correct.");
                }
                else if (password == "admin")
                {
                    Console.WriteLine("Welcome Admin");
                }
                else
                {
                    Console.WriteLine("User password incorrect!");
                } 
            }
            else
            {
                Console.WriteLine("Enmty field detected!");
            }
        }

        #region Old Code
        #region return Type
        // return Type
        private static string GreetMe()
        {
            Console.WriteLine("Please enter your username.");
            string name = Console.ReadLine();
            return name;
        }
        #endregion

        #region void nothing to return
        // void nothing to return.
        private static void PrintAge()
        {
            Console.WriteLine("Please enter your age.");
            string age = Console.ReadLine();
            Console.WriteLine($"You are {age} years old.");
        }
        #endregion

        #region meth with param
        // meth with param
        private static void PrintUserInfo(string name)
        {
            Console.WriteLine("Please enter your eye color.");
            string color = Console.ReadLine();
            Console.WriteLine($"Your user name is {name} and your eye color is {color}.");
        }
        #endregion

        private static void SelectFavouriteMovie()
        {
            // symbol {\n} == new line.
            Console.WriteLine("Movies list.\n");

            // Calling from another class.
            List<string> list = Movies.LoadMoviesList();
            // => alternative //var list = Movies.LoadMoviesList();
            int c = 0;
            foreach (var item in list)
            {
                Console.WriteLine($"{c}: {item}");
                c++;
                if (c == list.Count)
                    Console.WriteLine("\n");
            }

            Console.WriteLine("Please select your fav movie.");

            string movieString = Console.ReadLine();

            //ex 1.
            var movie = list.FirstOrDefault(m => m.ToLowerInvariant() == movieString.ToLowerInvariant().Trim());
            // ex 2. 
            /*foreach (var item in list)
            {
                if (item == movieString)
                {
                    Console.WriteLine($"Movie Selected == {movie}");
                    break;
                }
            }*/

            try
            {
                if (movie != null && !movie.Equals("Joker"))
                {
                    Console.WriteLine($"Movie Selected == {movie}");
                }
                else if (movie != null && movie.Equals("Joker"))
                {
                    Console.WriteLine($"You're Cool. 100%");
                }
                else
                {
                    // Use break Points to find why it crashes.
                    throw new Exception($"Selected movie: {movie} not found. (404):)");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error:--------------------- {ex.Message}");
            }
            /*finally
            {
                Console.WriteLine("Crash or not I'm executed (☞ﾟヮﾟ)☞");
            }*/
        } 
        #endregion

    }

}
