using ConsoleApp1;
using Shouldly;

namespace SudokuSolverTests;

public class SudokuTests
{
    private static readonly char[][] fullBoard = [
            ['5', '3', '4',   '6', '7', '8',   '9', '1', '2'],
            ['6', '7', '2',   '1', '9', '5',   '3', '4', '8'],
            ['1', '9', '8',   '3', '4', '2',   '5', '6', '7'],

            ['8', '5', '9',   '7', '6', '1',   '4', '2', '3'],
            ['4', '2', '6',   '8', '5', '3',   '7', '9', '1'],
            ['7', '1', '3',   '9', '2', '4',   '8', '5', '6'],

            ['9', '6', '1',   '5', '3', '7',   '2', '8', '4'],
            ['2', '8', '7',   '4', '1', '9',   '6', '3', '5'],
            ['3', '4', '5',   '2', '8', '6',   '1', '7', '9']];

    private static readonly char[][] emptyBoard = [
            ['5', '3', '.',   '.', '7', '.',    '.', '.', '.'],
            ['6', '7', '2',   '1', '9', '5',    '.', '.', '.'],
            ['1', '9', '8',   '.', '.', '.',    '.', '6', '.'],

            ['8', '.', '.',   '.', '6', '.',    '.', '.', '3'],
            ['4', '.', '.',   '8', '.', '3',    '.', '.', '1'],
            ['7', '.', '.',   '.', '2', '.',    '.', '.', '6'],

            ['.', '6', '.',   '.', '.', '.',    '2', '8', '.'],
            ['.', '.', '.',   '4', '1', '9',    '.', '.', '5'],
            ['.', '.', '.',   '.', '8', '.',    '.', '7', '9']];

    private readonly Sudoku fullSudoku;
    private readonly Sudoku emptySudoku;

    public SudokuTests()
    {
        fullSudoku = new Sudoku(fullBoard);
        emptySudoku = new Sudoku(emptyBoard);
    }

    [Theory]
    [InlineData(0, new char[] { '5', '3', '4', '6', '7', '8', '9', '1', '2' })]
    [InlineData(8, new char[] { '3', '4', '5', '2', '8', '6', '1', '7', '9' })]
    public void GetRow_RowExists_ShouldReturnAllValues(int index, char[] expectedRow)
    {
        var row = fullSudoku.GetRow(index);

        row.ShouldBeEquivalentTo(expectedRow);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(9)]
    public void GetRow_RowIndexOutOfBounds_ShouldThrowException(int rowIndex)
    {
        Should.Throw<ArgumentException>(() => fullSudoku.GetRow(rowIndex));
    }

    [Theory]
    [InlineData(0, new char[] { '5', '6', '1', '8', '4', '7', '9', '2', '3' })]
    [InlineData(8, new char[] { '2', '8', '7', '3', '1', '6', '4', '5', '9' })]
    public void GetColumn_ColumnExists_ShouldReturnAllValues(int index, char[] expectedColumn)
    {
        var column = fullSudoku.GetColumn(index);

        column.ShouldBeEquivalentTo(expectedColumn);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(9)]
    public void GetColumn_ColumnIndexOutOfBounds_ShouldThrowException(int rowIndex)
    {
        Should.Throw<ArgumentException>(() => fullSudoku.GetColumn(rowIndex));
    }

    [Fact]
    public void GetBoxForSpot_ValidArguments_ShouldReturnAllBox()
    {
        char[,] expected = {
            {'4', '2', '3' },
            {'7', '9', '1' },
            {'8', '5', '6' }};


        var box = fullSudoku.GetBoxForSpot(4, 7);

        box.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public void GetCandidatesForRow_NotFullRow_ShouldReturnAllCandidates()
    {
        List<char> expected = ['1', '2', '4', '6', '8', '9'];

        var candidates = emptySudoku.GetCandidatesForRow(0).ToList();

        candidates.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public void GetCandidatesForRow_FullRow_ShouldReturnEmptyList()
    {
        var candidates = fullSudoku.GetCandidatesForRow(0).ToList();

        candidates.ShouldBeEmpty();
    }

    [Fact]
    public void GetCandidatesForColumn_NotFullColumn_ShouldReturnEmptyList()
    {
        List<char> expected = ['2', '3', '9'];

        var candidates = emptySudoku.GetCandidatesForColumn(0).ToList();

        candidates.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public void GetCandidatesForColumn_FullColumn_ShouldReturnEmptyList()
    {
        var candidates = fullSudoku.GetCandidatesForColumn(0).ToList();

        candidates.ShouldBeEmpty();
    }

    [Fact]
    public void GetCandidatesForBox_NotFullBox_ShouldReturnAllCandidates()
    {
        List<char> expected = ['1', '2', '3', '5', '6', '9'];

        var candidates = emptySudoku.GetCandidatesForBox(4, 2).ToList();

        candidates.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public void GetCandidatesForBox_FullBox_ShouldReturnEmptyList()
    {
        var candidates = fullSudoku.GetCandidatesForBox(4, 2).ToList();

        candidates.ShouldBeEmpty();
    }

    [Theory]
    [InlineData(0, 2, new char[] { '4' })]
    [InlineData(3, 3, new char[] { '5', '7', '9' })]
    public void GetCandidatesForSpot_CandidatesExists_ShouldReturnAllCandidates(int row, int column, char[] expectedCandidates)
    {
        var candidates = emptySudoku.GetCandidatesForSpot(row, column).ToArray();

        candidates.ShouldBeEquivalentTo(expectedCandidates);
    }

    [Fact]
    public void GetCandidatesForSpot_CandidatesDoesNotExists_ShouldReturnEmptyList()
    {
        var candidates = fullSudoku.GetCandidatesForSpot(0, 0);

        candidates.ShouldBeEmpty();
    }

}
