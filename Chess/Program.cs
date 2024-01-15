using System;

class Program
{
    static char[,] chessboard = new char[8, 8];

    static void Main()
    {
        InitializeChessboard();
        PrintChessboard();

        MakeMove(1, 0, 2, 0);

        PrintChessboard();

        bool gameRunning = true;

        while (gameRunning)
        {
            Console.WriteLine("Enter your move (e.g., 'a2 a4'): ");
            string moveInput = Console.ReadLine();
            if (moveInput.ToLower() == "exit")
            {
                gameRunning = false;
                continue;
            }

            if (TryParseMove(moveInput, out int fromRow, out int fromCol, out int toRow, out int toCol))
            {
                if (IsValidMove(fromRow, fromCol, toRow, toCol))
                {
                    MakeMove(fromRow, fromCol, toRow, toCol);
                    PrintChessboard();
                }
                else
                {
                    Console.WriteLine("Invalid move!");
                }
            }
            else
            {
                Console.WriteLine("Invalid input! Please enter a move in the format 'from to', e.g., 'a2 a4'.");
            }
        }

        // Bot's turn
        if (gameRunning)
        {
            Console.WriteLine("Bot's turn...");
            MakeRandomMove();
            PrintChessboard();
        }

        Console.WriteLine("Thanks for playing!");

    }

    static void InitializeChessboard()
    {
        // Initialize the chessboard with pieces
        // 'P' represents pawn, 'R' represents rook, 'N' represents knight, etc.
        // ' ' represents an empty square

        // Place white pieces
        chessboard[0, 0] = chessboard[0, 7] = 'R';
        chessboard[0, 1] = chessboard[0, 6] = 'N';
        chessboard[0, 2] = chessboard[0, 5] = 'B';
        chessboard[0, 3] = 'Q';
        chessboard[0, 4] = 'K';
        for (int i = 0; i < 8; i++)
            chessboard[1, i] = 'P';

        // Place black pieces
        chessboard[7, 0] = chessboard[7, 7] = 'r';
        chessboard[7, 1] = chessboard[7, 6] = 'n';
        chessboard[7, 2] = chessboard[7, 5] = 'b';
        chessboard[7, 3] = 'q';
        chessboard[7, 4] = 'k';
        for (int i = 0; i < 8; i++)
            chessboard[6, i] = 'p';

        // Initialize the rest of the board with empty squares
        for (int i = 2; i < 6; i++)
            for (int j = 0; j < 8; j++)
                chessboard[i, j] = ' ';
    }

    static void PrintChessboard()
    {
        Console.Clear();
        Console.WriteLine("  a b c d e f g h");
        Console.WriteLine("  ----------------");
        for (int i = 0; i < 8; i++)
        {
            Console.Write(8 - i + "|");
            for (int j = 0; j < 8; j++)
                Console.Write(chessboard[i, j] + " ");
            Console.WriteLine();
        }
        Console.WriteLine("  ----------------");
    }

    static void MakeMove(int fromRow, int fromCol, int toRow, int toCol)
    {
        // Perform the move if it's valid
        if (IsValidMove(fromRow, fromCol, toRow, toCol))
        {
            chessboard[toRow, toCol] = chessboard[fromRow, fromCol];
            chessboard[fromRow, fromCol] = ' ';
        }
        else
        {
            Console.WriteLine("Invalid move!");
        }
    }

    static bool IsValidMove(int fromRow, int fromCol, int toRow, int toCol)
    {
        // Implement the logic to check if the move is valid for the specific piece
        // For simplicity, let's just check if the destination square is empty

        if (chessboard[fromRow, fromCol] == ' ')
        {
            Console.WriteLine("No piece at the specified location!");
            return false;
        }

        if (chessboard[toRow, toCol] != ' ')
        {
            Console.WriteLine("Destination square is occupied!");
            return false;
        }

        return true;
    }

    static bool TryParseMove(string moveInput, out int fromRow, out int fromCol, out int toRow, out int toCol)
    {
        fromRow = fromCol = toRow = toCol = 0;

        if (moveInput.Length != 5)
            return false;

        char[] validFiles = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };

        char fromFile = moveInput[0];
        char toFile = moveInput[3];

        int.TryParse(moveInput[1].ToString(), out fromRow);
        int.TryParse(moveInput[4].ToString(), out toRow);

        fromCol = Array.IndexOf(validFiles, fromFile);
        toCol = Array.IndexOf(validFiles, toFile);

        return (fromRow >= 1 && fromRow <= 8 && toRow >= 1 && toRow <= 8 && fromCol != -1 && toCol != -1);
    }

    static void MakeRandomMove()
    {
        Random random = new Random();

        int fromRow, fromCol, toRow, toCol;

        do
        {
            fromRow = random.Next(1, 9);
            fromCol = random.Next(0, 8);
            toRow = random.Next(1, 9);
            toCol = random.Next(0, 8);
        } while (!IsValidMove(fromRow - 1, fromCol, toRow - 1, toCol));

        MakeMove(fromRow - 1, fromCol, toRow - 1, toCol);
    }
}
