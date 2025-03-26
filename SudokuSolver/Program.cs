using System.Text;

namespace ConsoleApp1;

public class Sudoku(char[][] board)
{
    public readonly char[][] originalBoard = board;
    public char[][] board = board;
    
    private readonly char[] possibleValues = ['1', '2', '3', '4', '5', '6', '7', '8', '9'];
    
    public bool IsSolved
    {
        get
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (board[i][j] == '.')
                        return false;
                }
            }

            return true;
        }
    }

    public char[,] GetBoxForSpot(int rowIndex, int columnIndex)
    {
        char[,] box = new char[3, 3];

        int boxRowIndex = rowIndex / 3;
        int boxColumnIndex = columnIndex / 3;

        for (int i = 0; i < 3; i++)
        {
            var row = (boxRowIndex * 3) + i;

            for (int j = 0; j < 3; j++)
            {
                var column = (boxColumnIndex * 3) + j;

                box[i, j] = originalBoard[row][column];
            }
        }

        return box;
    }

    public char[] GetRow(int boardRowIndex)
    {
        if (boardRowIndex < 0 || boardRowIndex > 8)
            throw new ArgumentException("Row index must be between 0 and 8");

        return board[boardRowIndex];
    }

    public char[] GetColumn(int index)
    {
        if (index < 0 || index > 8)
            throw new ArgumentException("Column index must be between 0 and 8");

        var column = new char[9];

        for (int i = 0; i < 9; i++)
        {
            column[i] = GetSpot(i, index);
        }

        return column;
    }

    public char GetSpot(int rowIndex, int columnIndex)
    {
        if (rowIndex < 0 || rowIndex > 8)
            throw new ArgumentException("Row index must be between 0 and 8");

        if (columnIndex < 0 || columnIndex > 8)
            throw new ArgumentException("Column index must be between 0 and 8");

        return board[rowIndex][columnIndex];
    }

    public IEnumerable<char> GetCandidatesForRow(int index)
    {
        var row = GetRow(index);

        return possibleValues.Except(row);
    }

    public IEnumerable<char> GetCandidatesForColumn(int index)
    {
        var column = GetColumn(index);

        return possibleValues.Except(column);
    }

    public IEnumerable<char> GetCandidatesForBox(int rowSpotIndex, int columnSpotIndex)
    {
        var box = GetBoxForSpot(rowSpotIndex, columnSpotIndex).Cast<char>();

        return possibleValues.Except(box);
    }

    public IEnumerable<char> GetCandidatesForSpot(int rowIndex, int columnIndex)
    {
        if (GetSpot(rowIndex, columnIndex) != '.')
            return [];

        var rowCandidates = GetCandidatesForRow(rowIndex);
        var columnCandidates = GetCandidatesForColumn(columnIndex);
        var boxCandidates = GetCandidatesForBox(rowIndex, columnIndex);

        return rowCandidates.Intersect(columnCandidates).Intersect(boxCandidates);
    }

    public override string? ToString()
    {
        StringBuilder sb = new();

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                sb.Append($"{board[i][j]} ");

                if (j % 3 == 2)
                    sb.Append("  ");
            }

            sb.AppendLine();

            if (i % 3 == 2)
                sb.AppendLine();
        }

        return sb.ToString();
    }
}

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
