namespace SudokuSolver;

public class Solution
{
    char[][] board = [
        ['5', '3', '.',   '.', '7', '.',   '.', '.', '.'],
        ['6', '.', '.',   '1', '9', '5',   '.', '.', '.'],
        ['.', '9', '8',   '.', '.', '.',   '.', '6', '.'],

        ['8', '.', '.',   '.', '6', '.',   '.', '.', '3'],
        ['4', '.', '.',   '8', '.', '3',   '.', '.', '1'],
        ['7', '.', '.',   '.', '2', '.',   '.', '.', '6'],

        ['.', '6', '.',   '.', '.', '.',   '2', '8', '.'],
        ['.', '.', '.',   '4', '1', '9',   '.', '.', '5'],
        ['.', '.', '.',   '.', '8', '.',   '.', '7', '9']];


    public void SolveSudoku(char[][] board)
    {
        var sudoku = new Sudoku(board);
        Console.WriteLine(sudoku);

        int i = 0;

        do
        {
            Console.WriteLine($"Iteration {++i}");
            SolveSinglePossibilities(sudoku);

        } while (!sudoku.IsSolved);


        Validate(sudoku);

        Console.WriteLine(sudoku);
    }

    private void SolveSinglePossibilities(Sudoku sudoku)
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                var candidates = sudoku.GetCandidatesForSpot(i, j).ToList();

                if (candidates.Count == 1)
                {
                    var theChosenOne = candidates[0];
                    sudoku.board[i][j] = theChosenOne;

                    Console.WriteLine($"    Spot [{i}:{j}] has been solved. Value: {theChosenOne}");
                }
            }
        }
    }

    private void Validate(Sudoku sudoku)
    {
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
        Solution solution = new();

        solution.SolveSudoku(solution.board);
    }
}
