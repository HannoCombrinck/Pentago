
public interface ISquareMatrix<T>
{
    int GetSize();
    ref T At(int row, int col);
}

public static class SquareMatrixRotater
{
    public static void Rotate90DegreesClockwise<T>(ISquareMatrix<T> matrix)
    {
        Transpose(matrix);
        
        // Mirror left to right 
        for (int row = 0; row < matrix.GetSize(); row++)
            for (int col = 0; col < matrix.GetSize() / 2; col++)
                Swap(ref matrix.At(row, col), ref matrix.At(row, matrix.GetSize()-1-col));
    }

    public static void Rotate90DegreesCounterclockwise<T>(ISquareMatrix<T> matrix)
    {
        Transpose(matrix);

        // Mirror top to bottom 
        for (int col = 0; col < matrix.GetSize(); col++)
            for (int row = 0; row < matrix.GetSize() / 2; row++)
                Swap(ref matrix.At(row, col), ref matrix.At(matrix.GetSize() - 1 - row, col));
    }

    private static void Transpose<T>(ISquareMatrix<T> matrix)
    {
        for (int row = 0; row < matrix.GetSize() - 1; row++)
            for (int col = row + 1; col < matrix.GetSize(); col++)
                Swap(ref matrix.At(row, col), ref matrix.At(col, row));
    }

    private static void Swap<T>(ref T lhs, ref T rhs)
    {
        T temp;
        temp = lhs;
        lhs = rhs;
        rhs = temp;
    }
}
