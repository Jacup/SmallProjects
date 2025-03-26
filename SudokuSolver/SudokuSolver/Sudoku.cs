using System.Text;

namespace SudokuSolver;

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
            var row = boxRowIndex * 3 + i;

            for (int j = 0; j < 3; j++)
            {
                var column = boxColumnIndex * 3 + j;

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
