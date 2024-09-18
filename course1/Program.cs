using System;

public class MainClass {
    public static void Main() {
        var A = new Matrix();
        A.Read();

        var C = A.transpon();
        C.Write();
    }
}

public class Matrix {
    public int N;
    public int M;
    public double[,] Data;

    public void Read() {
        var size = Console.ReadLine().Split(' ');
        N = int.Parse(size[0]);
        M = int.Parse(size[1]);

        Data = new double[N, M];

        for (int i = 0; i < N; ++i) { 
            var str = Console.ReadLine().Split(' ');
            for(int j = 0; j < M; ++j)
                Data[i, j] = double.Parse(str[j]);
        }

    }

    public void Write() {

        for (int i = 0; i < N; ++i) {

            for (int j = 0; j < M; ++j) {

                Console.Write(Data[i, j]);
                if (j == M - 1)
                    Console.Write("\n");
                else
                    Console.Write(" ");
            }
        }
    }

    public Matrix Multiply(double n) {
        Matrix c = new Matrix();
        c.N = N;
        c.M = M;
        c.Data = new double[c.N, c.M];
        for (int i = 0; i < N; ++i)
            for (int j = 0; j < M; ++j)
                c.Data[i,j] = Data[i, j] * n;
        return c;
    }

    public Matrix Sum(Matrix m) {
        Matrix c = new Matrix();
        if (N == m.N && M == m.M) {
            c.N = N;
            c.M = M;
            c.Data = new double[c.N, c.M];
            for (int i = 0; i < N; ++i)
                for (int j = 0; j < M; ++j)
                    c.Data[i, j] = Data[i, j] + m.Data[i, j];
        } else {
            c.N = 0;
            c.M = 0;
            c.Data = new double[0, 0];
        }
        return c;
    }

    public Matrix Prod(Matrix m) {
        Matrix c = new Matrix();
        if (M == m.N) {
            c.N = N;
            c.M = m.M;
            c.Data = new double[c.N, c.M];
            for (int i = 0; i < c.N; ++i)
                for (int j = 0; j < c.M; ++j) {
                    c.Data[i, j] = 0;
                    for (int k = 0; k < M; ++k)
                        c.Data[i, j] += Data[i, k] * m.Data[k, j];
                }

        } else {
            c.N = 0;
            c.M = 0;
            c.Data = new double[0, 0];
        }
        return c;
    }

    public Matrix transpon() {
        Matrix c = new Matrix();
        c.N = M;
        c.M = N;
        c.Data = new double[c.N, c.M];
        for (int i = 0; i < c.N; ++i)
            for (int j = 0; j < c.M; ++j)
                c.Data[i, j] = Data[j, i];
        return c;
    }
}