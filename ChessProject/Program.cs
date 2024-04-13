using System;
int[,] board = new int[,]
{
    {3, 4, 5, 2, 1, 5, 4, 3},
    {6, 6, 6, 6, 6, 6, 6, 6},
    {0, 0, 0, 0, 0, 0, 0, 0},
    {0, 0, 0, 0, 0, 0, 0, 0},
    {0, 0, 0, 0, 0, 0, 0, 0},
    {0, 0, 0, 0, 0, 0, 0, 0},
    {-6, -6, -6, -6, -6, -6, -6, -6},
    {-3, -4, -5, -2, -1, -5, -4, -3}
};
bool whiteTurn = true;
int move = 1;

void PrintBoard(int[,] board)
{
//Метод выводящий доску на экран
    Console.Clear();
    Console.WriteLine();
    int horiz = 1;
    char[] vert = {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h'};
    string[] shapes = {"king", "queen", "rook", "knight", "bishop", "pawn", "" }; 
    for (int i = 0; i < 8; i++)
    {
        Console.Write($"\t {vert[i]}");
    }
    Console.WriteLine();
    Console.WriteLine();
    Console.WriteLine("      + - - - + - - - + - - - + - - - + - - - + - - - + - - - + - - - +");
    for (int i = 0; i < board.GetLength(0); i++)
    {
        
        Console.Write($"  {horiz}     ");
        horiz++;
        for (int j = 0; j < board.GetLength(1); j++)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            //Белые Фигуры
            if (board[i, j] == 1) Console.Write($"{shapes[0]} \t");
            if (board[i, j] == 2) Console.Write($"{shapes[1]} \t");
            if (board[i, j] == 3) Console.Write($"{shapes[2]} \t");
            if (board[i, j] == 4) Console.Write($"{shapes[3]} \t");
            if (board[i, j] == 5) Console.Write($"{shapes[4]} \t");
            if (board[i, j] == 6) Console.Write($"{shapes[5]} \t");
            //Пустые клетки
            if (board[i, j] == 0) Console.Write($"{shapes[6]} \t");
            //Чёрные фигуры
            Console.ForegroundColor = ConsoleColor.Red;
            if (board[i, j] == -1) Console.Write($"{shapes[0]} \t");
            if (board[i, j] == -2) Console.Write($"{shapes[1]} \t");
            if (board[i, j] == -3) Console.Write($"{shapes[2]} \t");
            if (board[i, j] == -4) Console.Write($"{shapes[3]} \t");
            if (board[i, j] == -5) Console.Write($"{shapes[4]} \t");
            if (board[i, j] == -6) Console.Write($"{shapes[5]} \t");
            Console.ForegroundColor = ConsoleColor.White;
        }
        Console.WriteLine();
        Console.WriteLine("      + - - - + - - - + - - - + - - - + - - - + - - - + - - - + - - - +");
    }

    if (whiteTurn) Console.WriteLine($"    White's Move. \t Move {move}");
    else Console.WriteLine($"    Black's Move. \t Move {move}");
    ShapesCheckMoveSelect(board);
}
void ShapesCheckMoveSelect(int[,]board)
{
//Метод отвечающий за выбор фигуры
    int[] resultCoord = {0, 0};

    //Выбор цифровой координаты
    Console.WriteLine("Please enter the coordinate of the shape you want to move to.");
    char charX = Convert.ToChar(Console.ReadLine());
    int intY = Convert.ToInt32(Console.ReadLine());
    resultCoord = EnterCoord(charX, intY);

    //Проверка на фигуру правильного цвета
    if (board[resultCoord[0], resultCoord[1]] != 0)
    {
        if (board[resultCoord[0], resultCoord[1]] > 0)
        {
            if (whiteTurn)
            {
                //Завершение метода
                NavigatorShapes(board, resultCoord);
            }
            else ShapesCheckMoveSelect(board);
        }
        if (board[resultCoord[0], resultCoord[1]] < 0)
        {
            if (whiteTurn == false)
            {
                //Завершение метода
                NavigatorShapes(board, resultCoord);
            }
            else ShapesCheckMoveSelect(board);
        }
    }
    else ShapesCheckMoveSelect(board);
}
int[,] NavigatorShapes(int[,] board, int[] coordinates)
{
//Метод отвечающий за передвижение фигуры
    int selectShapes = board[coordinates[0], coordinates[1]];
    int[] resultCoord = { 0, 0 };
    int colorShapes = 0;
    int colorMove = 0;

    //Логика фигур
    int[,] posibleMove = RevisioCord(selectShapes, coordinates, board);

    //Выбор цифровой координаты
    Console.WriteLine("Please enter the coordinate to the move to.");
    char charX = Convert.ToChar(Console.ReadLine());
    int intY = Convert.ToInt32(Console.ReadLine());
    resultCoord = EnterCoord(charX, intY);

    if (posibleMove[resultCoord[0], resultCoord[1]] == 0) ShapesCheckMoveSelect(board);

    if (selectShapes > 0) colorShapes = 1;
    else colorShapes = -1;

    if (board[resultCoord[0], resultCoord[1]] > 0) colorMove = 1;
    else if (board[resultCoord[0], resultCoord[1]] < 0) colorMove = -1;
    else colorMove = 0;

    if (colorMove <= 0 && colorShapes == 1)
    {
        board[resultCoord[0], resultCoord[1]] = selectShapes;
        board[coordinates[0], coordinates[1]] = 0;
    }
    else if (colorMove >= 0 && colorShapes == -1)
    {
        board[resultCoord[0], resultCoord[1]] = selectShapes;
        board[coordinates[0], coordinates[1]] = 0;
    }
    else ShapesCheckMoveSelect(board);

    if (whiteTurn) whiteTurn = false;
    else
    {
        whiteTurn = true;
        move++;
    }
    PrintBoard(board);
    return board;
}

int[] EnterCoord(char numberX, int numberY)
//Метод отвечающий за ввод координат
{
    int[] coord = {0, 0};
    bool trueX = false;
    char[] horiz = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };

    for (int i = 0; i < horiz.Length; i++)
    {
        if (numberX == horiz[i])
        {
            coord[1] = i;
            trueX = true;
        }
    }
    if (trueX == false) ShapesCheckMoveSelect(board);

    coord[0] = numberY;
    if (coord[0] >= 1 && coord[0] <= 8) coord[0]--;
    else ShapesCheckMoveSelect(board);

    return coord;
}
int[,] RevisioCord(int shapes, int[] shapesPosition, int[,] board)
//Метод отвечающий за проверку координаты на возможность хода
{
    int x = 0;
    int y = 0;
    int[,] possibleMove = new int[,]
    {
        {0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0}
    };
    //WhitePawn
    if (shapes == 6)
    {
        //Первый ход пешки
        if (shapesPosition[0] == 1)
        {
            if (board[shapesPosition[0] + 2, shapesPosition[1]] == 0)
            {
                if (board[shapesPosition[0] + 1, shapesPosition[1]] == 0) possibleMove[shapesPosition[0] + 2, shapesPosition[1]] = 1;
            }
        }
        if (board[shapesPosition[0] + 1, shapesPosition[1]] == 0) possibleMove[shapesPosition[0] + 1, shapesPosition[1]] = 1;
        if (board[shapesPosition[0] + 1, shapesPosition[1] - 1] < 0) possibleMove[shapesPosition[0] + 1, shapesPosition[1] - 1] = 1;
        if (board[shapesPosition[0] + 1, shapesPosition[1] + 1] < 0) possibleMove[shapesPosition[0] + 1, shapesPosition[1] + 1] = 1;
    }
    //BlackPawn
    if (shapes == -6)
    {
        //Первый ход пешки
        if (shapesPosition[0] == 6)
        {
            if (board[shapesPosition[0] - 2, shapesPosition[1]] == 0)
            {
                if (board[shapesPosition[0] - 1, shapesPosition[1]] == 0) possibleMove[shapesPosition[0] - 2, shapesPosition[1]] = 1;
            }
        }
        if (board[shapesPosition[0] - 1, shapesPosition[1]] == 0) possibleMove[shapesPosition[0] - 1, shapesPosition[1]] = 1;
        if (board[shapesPosition[0] - 1, shapesPosition[1] + 1] > 0) possibleMove[shapesPosition[0] - 1, shapesPosition[1] + 1] = 1;
        if (board[shapesPosition[0] - 1, shapesPosition[1] - 1] > 0) possibleMove[shapesPosition[0] - 1, shapesPosition[1] - 1] = 1;
    }
    //Knight
    if (shapes == 4 || shapes == -4)
    {
        int[,] moveKnight = { { -2, -1 }, { 2, -1 }, { -2, 1 }, { 2, 1 }, { -1, -2 }, { 1, -2 }, { -1, 2 }, { 1, 2 } };
        for (int i = 0; i < moveKnight.GetLength(0); i++)
        {
            x = shapesPosition[0] + moveKnight[i, 0];
            y = shapesPosition[1] + moveKnight[i, 1];
            if (x >= 0 && x < 8 && y >= 0 && y < 8)
            {
                possibleMove[x, y] = 1;
            }
        }
    }
    //Bishop and Queen
    if (shapes == 5 || shapes == -5 || shapes == 2 || shapes == -2)
    {
        for (int i = 0; i < 4; i++)
        {
            x = 0;
            y = 0;
            bool blocked = false;
            for (int j = 0; j < 8; j++)
            {
                if (i == 0) { x = shapesPosition[0] + j; y = shapesPosition[1] + j; }
                if (i == 1) { x = shapesPosition[0] - j; y = shapesPosition[1] - j; }
                if (i == 2) { x = shapesPosition[0] + j; y = shapesPosition[1] - j; }
                if (i == 3) { x = shapesPosition[0] - j; y = shapesPosition[1] + j; }
                if (x >= 0 && x < 8 && y >= 0 && y < 8)
                {
                    if (blocked == false && j > 0)
                    {
                        if (board[x, y] == 0) possibleMove[x, y] = 1;
                        if (board[x, y] < 0 && whiteTurn == false) blocked = true;
                        else if (board[x, y] < 0 && whiteTurn == true) { possibleMove[x, y] = 1; blocked = true; }
                        if (board[x, y] > 0 && whiteTurn == true) blocked = true;
                        else if (board[x, y] > 0 && whiteTurn == false) { possibleMove[x, y] = 1; blocked = true; }
                    }
                }
            }
        }
    }
    //Rook and Queen
    if (shapes == 3 || shapes == -3 || shapes == 2 || shapes == -2)
    {
        for (int i = 0; i < 4; i++)
        {
            x = 0;
            y = 0;
            bool blocked = false;
            for (int j = 0; j < 8; j++)
            {
                if (i == 0) { x = shapesPosition[0] + j; y = shapesPosition[1]; }
                if (i == 1) { x = shapesPosition[0] - j; y = shapesPosition[1]; }
                if (i == 2) { x = shapesPosition[0]; y = shapesPosition[1] - j; }
                if (i == 3) { x = shapesPosition[0]; y = shapesPosition[1] + j; }
                if (x >= 0 && x < 8 && y >= 0 && y < 8)
                {
                    if (blocked == false && j > 0)
                    {
                        if (board[x, y] == 0) possibleMove[x, y] = 1;
                        if (board[x, y] < 0 && whiteTurn == false) blocked = true;
                        else if (board[x, y] < 0 && whiteTurn == true) { possibleMove[x, y] = 1; blocked = true; }
                        if (board[x, y] > 0 && whiteTurn == true) blocked = true;
                        else if (board[x, y] > 0 && whiteTurn == false) { possibleMove[x, y] = 1; blocked = true; }
                    }
                }
            }
        }
    }
    //King
    if (shapes == 1 || shapes == -1)
    {
        for (int i = 0; i < 8; i++)
        {
            if (i == 0) { x = shapesPosition[0] + 1; y = shapesPosition[1] + 1; }
            if (i == 1) { x = shapesPosition[0] + 1; y = shapesPosition[1] - 1; }
            if (i == 2) { x = shapesPosition[0] - 1; y = shapesPosition[1] - 1; }
            if (i == 3) { x = shapesPosition[0] - 1; y = shapesPosition[1] + 1; }
            if (i == 4) { x = shapesPosition[0]; y = shapesPosition[1] + 1; }
            if (i == 5) { x = shapesPosition[0]; y = shapesPosition[1] - 1; }
            if (i == 6) { x = shapesPosition[0] + 1; y = shapesPosition[1]; }
            if (i == 7) { x = shapesPosition[0] - 1; y = shapesPosition[1]; }

            if (x >= 0 && x < 8 && y >= 0 && y < 8) possibleMove[x, y] = 1;
        }
    }
    
    return possibleMove;
}

//Старт игры
PrintBoard(board);