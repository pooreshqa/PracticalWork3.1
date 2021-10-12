using System;

namespace course_work
{
    class Program
    {
        static void Main(string[] args)
        {
            gameManager.Instance.BaseUnitFactory = new BaseFactory();
            gameManager.Instance.MortalUnitFactory = new MortalFactory();
            // 5 5
            byte[] map1 = { 1, 1, 1, 1, 1,
                            1, 0, 0, 0, 1,
                            1, 0, 2, 0, 1,
                            1, 0, 0, 4, 1,
                            1, 1, 1, 1, 1
            };// 8 5
            byte[] map2 = { 1, 1, 1, 1, 1, 1, 1, 1,
                            1, 0, 1, 0, 0, 0, 0, 1,
                            1, 0, 1, 0, 1, 0, 0, 1,
                            1, 0, 0, 4, 1, 0, 2, 1,
                            1, 1, 1, 1, 1, 1, 1, 1
            };// 6 8
            byte[] map3 = { 1, 1, 1, 1, 1, 1,
                            1, 0, 0, 0, 0, 1,
                            1, 0, 1, 1, 0, 1,
                            1, 0, 1, 4, 0, 1,
                            1, 0, 1, 0, 0, 1,
                            1, 0, 1, 1, 1, 1,
                            1, 0, 0, 0, 0, 1,
                            1, 1, 1, 1, 1, 1
            };
            gameManager.Instance.grid = new Grid(map1, 5, 5);
            
            string message = "";
            while (true)
            {
                Console.Clear();
                // foreach (var item in gameManager.Instance.grid.Units)
                // {
                //     Console.WriteLine(item);
                // }
                if (message != "") { Console.WriteLine(message); message = ""; }
                gameManager.Instance.grid.draw();
                Console.WriteLine($"Здоровье: {gameManager.Instance.player.health}\nПеремещение:\n1 - Шаг вверх\n2 - Шаг вправо\n3 - Шаг вниз\n4 - Шаг влево\n0 - Выйти");
                string input = Console.ReadLine();
                bool success = Int32.TryParse(input, out int parsedInput);
                if (!success) { message = "Вводить можно только цифры"; continue; }
                if (parsedInput == 0) break;
                switch (parsedInput)
                {
                    case >=1 and <=4:
                        message = gameManager.Instance.player.move(parsedInput);
                        break;
                    default:
                        message = "Моя твоя не понимать, еретик!";
                        break;
                }
            }
        }
    }
}
