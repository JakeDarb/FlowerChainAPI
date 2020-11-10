using System;


    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Test!");
            int addNumber = Add  (4,5);
            int minusNumber = Minus(10,7);

            Console.WriteLine(addNumber);
            Console.WriteLine(minusNumber);
            Console.WriteLine(isOdd(5));

           
        }

        public static int Add(int x, int y){
            return x + y;
        }

        public static int Minus(int a, int b){
            return a - b;
        }

        public static Boolean isOdd(int value){
            return value % 2 == 1;
        }

        public static Boolean isEven(int value){
            return value % 2 == 0;
        }
    }

