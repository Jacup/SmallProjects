using System.Diagnostics;

namespace SudokuSolver;

public class Solution
{
    public void SolveSudoku(char[][] board)
    {
        var sudoku = new Sudoku(board);
        Console.WriteLine(sudoku);

        if (!sudoku.IsSolved)
        {
            BacktrackSolve(sudoku);
        }

        Validate(sudoku);

        Console.WriteLine(sudoku);
    }

    private bool BacktrackSolve(Sudoku sudoku)
    {
        for (int row = 0; row < 9; row++)
        {
            for (int column = 0; column < 9; column++)
            {
                if (sudoku.board[row][column] == '.')
                {
                    var candidates = sudoku.GetCandidatesForSpot(row, column).ToArray();

                    foreach (var candidate in candidates)
                    {
                        sudoku.board[row][column] = candidate;

                        if (BacktrackSolve(sudoku))
                            return true;

                        sudoku.board[row][column] = '.';
                    }

                    return false;
                }
            }
        }

        Console.WriteLine("Sudoku rozwiązane.");
        return true;
    }

    private void Validate(Sudoku sudoku)
    {
        if (!sudoku.IsSolved)
            Console.WriteLine("Sudoku is not solved.");

        for (int i = 0; i < 9; i++)
        {
            if (sudoku.GetRow(i).Distinct().Count() > 9)
            {
                throw new Exception($"Invalid row {i}");
            }

            if (sudoku.GetColumn(i).Distinct().Count() > 9)
            {
                throw new Exception($"Invalid column {i}");
            }
        }

        for (int i = 0; i < 3; i = +3)
        {
            for (int j = 0; j < 3; j = +3)
            {
                var box = sudoku.GetBoxForSpot(i, j);

                if (box.Cast<char>().Distinct().Count() > 9)
                {
                    throw new Exception("Invalid box");
                }
            }
        }

        Console.WriteLine($"Sudoku validated successfully.{Environment.NewLine}");
    }

    public static void Main(string[] args)
    {
        char[][] board1 = [
            ['5', '3', '.',   '.', '7', '.',   '.', '.', '.'],
            ['6', '.', '.',   '1', '9', '5',   '.', '.', '.'],
            ['.', '9', '8',   '.', '.', '.',   '.', '6', '.'],

            ['8', '.', '.',   '.', '6', '.',   '.', '.', '3'],
            ['4', '.', '.',   '8', '.', '3',   '.', '.', '1'],
            ['7', '.', '.',   '.', '2', '.',   '.', '.', '6'],

            ['.', '6', '.',   '.', '.', '.',   '2', '8', '.'],
            ['.', '.', '.',   '4', '1', '9',   '.', '.', '5'],
            ['.', '.', '.',   '.', '8', '.',   '.', '7', '9']];


        char[][] board2 = [
            ['.', '.', '9', '7', '4', '8', '.', '.', '.'],
            ['7', '.', '.', '.', '.', '.', '.', '.', '.'],
            ['.', '2', '.', '1', '.', '9', '.', '.', '.'],
            ['.', '.', '7', '.', '.', '.', '2', '4', '.'],
            ['.', '6', '4', '.', '1', '.', '5', '9', '.'],
            ['.', '9', '8', '.', '.', '.', '3', '.', '.'],
            ['.', '.', '.', '8', '.', '3', '.', '2', '.'],
            ['.', '.', '.', '.', '.', '.', '.', '.', '6'],
            ['.', '.', '.', '2', '7', '5', '9', '.', '.']];

        Solution solution = new();

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        //solution.SolveSudoku(board1);
        solution.SolveSudoku(board2);
        stopwatch.Stop();
        Console.WriteLine($"Elapsed time: {stopwatch.ElapsedMilliseconds} ms");

    }
}
