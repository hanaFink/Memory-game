using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Memor__Game
{

    class Program
    {
        static void intro (out int [,] matrix, out int size)
        {
            Console.WriteLine("Hello, welcome to the memory game");
            Console.WriteLine("To start the game please enter size of the table - size of the table must to be even and positive");
            size = Convert.ToInt32(Console.ReadLine());
            while (size % 2 != 0 || size <= 0)
            {
                Console.WriteLine("You entered odd and/or negative number, please try again");
                size = Convert.ToInt32(Console.ReadLine());

            }
            matrix = new int[size, size];
        }
        static void printMatrix(int[,] arr, params int? [] arr1)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {

                    if (arr[i, j] == 0)
                    {

                        Console.Write(" O |");
                    }
                    else if (arr1.Length == 4 && arr1[0] == i && arr1[1] == j)
                    {

                        Console.Write($" {arr[i, j]} |");
                    }
                    else if (arr1.Length == 4 && arr1[2]  == i && arr1[3]  == j)
                    {

                        Console.Write($" {arr[i, j]} |");
                    }

                    else
                    {
                        Console.Write(" X |");
                    }
                }
                Console.WriteLine();
                Console.WriteLine();
                
            }

        }
        /// <summary>
        /// first fill of table by random function
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="matrix_size"></param>
        static void fillMatrix(int[,] arr, int matrix_size)
        {
            for (int i = 1; i < matrix_size * matrix_size / 2 + 1; i++)
            {
                int j = 0;
                Random p = new Random();
                while (j < 2)
                {

                    int row = p.Next(0, matrix_size);
                    int column = p.Next(0, matrix_size);

                    if (arr[row, column] == 0)
                    {
                        arr[row, column] = i;
                        j++;
                    }
                }

            }
        }
            static void selectTheCard( int [,] arr, int size,out int row, out int column)
            {
            do
            {
                Console.WriteLine($"please select number of row from 1 to {size}");
                row = Convert.ToInt32(Console.ReadLine())-1;
                Console.WriteLine($"please select number of column from 1 to {size}");
                column = Convert.ToInt32(Console.ReadLine())-1;
                if (row >= size || column >= size || (arr[row, column] == 0))
                    {
                    Console.WriteLine("wrong enter select another card");
                }
            } while (row >= size || column >= size  || arr[row, column] == 0);

        
            }


        static void Main(string[] args)
        {
            int score1 = 0, score2 = 0; // scores for every player
            bool player = true;// player1 = true, player2 = false
            bool win ; // true player win this round, false player lost this round



            intro(out int[,] matrix, out int size);
            fillMatrix(matrix, size);
            printMatrix(matrix);


            ///game ends when the score is size*size/2

            while ((score1 + score2) < size * size / 2)
            {
                if (player == true) // whith player is playing 
                    Console.Write("Now playing Player One, ");
                else
                    Console.Write("Now playing Player Two, ");

                do 
                {
                    win = false;
                    // select first card
                    Console.WriteLine("please select first card");
                    selectTheCard(matrix, size, out int row, out int column);
                    int?[] array = { row, column, null, null };
                    printMatrix(matrix, array);



                    // select second card
                    Console.WriteLine("please select second card");
                    selectTheCard(matrix, size, out row, out column);
                    array[2] = row;
                    array[3] = column;

                    ///if player choose same row and colomn(same place) like the first card
                    ///select anoter card
                    while (array[0] == array[2] && array[1] == array[3])
                    {
                        Console.WriteLine("Wrong enter");
                        selectTheCard(matrix, size, out row, out column);
                        array[2] = row;
                        array[3] = column;
                    }
                    printMatrix(matrix, array);



                    if (matrix[(int)array[0], (int)array[1]] == matrix[(int)array[2], (int)array[3]])
                    {
                        Console.WriteLine("you win this round");
                        win = true;
                        matrix[(int)array[0], (int)array[1]] = 0;
                        matrix[(int)array[2], (int)array[3]] = 0;
                        if (player == true)
                        {
                            score1++;
                        }
                        else
                        {
                            score2++;
                        }
                    }
                    else
                    {
                        Console.WriteLine(" you loose this round, turn goes to another player");
                    }
                        
                } while (win == true && (score1 + score2) < size * size / 2);

                Console.WriteLine();

                ///changing player 
                ///first player true
                ///second player false

                if (player == true)
                {
                    player = false;
                }
                else
                {
                    player = true;
                }
                Console.WriteLine("Press enter to continue");
                Console.ReadLine();
                Console.Clear();
                Console.WriteLine();
                printMatrix(matrix);

            }

            ///ending of the game

            Console.WriteLine($" score of player one is {score1}");
            Console.WriteLine($" score of player two is {score2}");
            if (score1 > score2)
                {
                Console.WriteLine("Player one is the WINNER ");
            }
          else if( score2 > score1)
            {
                Console.WriteLine("Player two is the WINNER ");
            }
            
          else
            {
                Console.WriteLine("The score is even , maybe another time"  );
            }
        }
    }
}
