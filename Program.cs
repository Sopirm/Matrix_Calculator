
// Создание матриц (номер 1)
using System;
using System.IO.IsolatedStorage;

static double[,] creating_matrix()
{
    Console.WriteLine("Введи значение строк: ");
    int n = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("Введи значение столбцов: ");
    int m = Convert.ToInt32(Console.ReadLine());
    return new double[n, m];



}

// Собственное заполнение матрицы (номер 2)
static double[,] fulling_matrix(double[,] matrix)
{
    int rows = matrix.GetLength(0);
    int cols = matrix.GetLength(1);
    for (int i = 0; i < rows; i++)
    {
        for (int j = 0; j < cols; j++)
        {
            Console.Write($"Значение для [{i + 1}, {j + 1}]: ");
            matrix[i, j] = Convert.ToDouble(Console.ReadLine());
        }

    }
    return matrix;
}

// Рандомное заполнение матрицы (номер 3)
static double[,] rand_fulling_matrix(double[,] matrix)
{
    Random rnd = new Random();
    // a + b ввод
    int rows = matrix.GetLength(0);
    int cols = matrix.GetLength(1);
    Console.Write("Введите значение a(число от которого происходит генерация): ");
    double a = Convert.ToDouble(Console.ReadLine());

    Console.Write("Введите значение b(число от которого происходит генерация): ");
    double b = Convert.ToDouble(Console.ReadLine());

    Console.WriteLine("");
    for (int i = 0; i < rows; i++)
    {
        for (int j = 0; j < cols; j++)
        {
            matrix[i, j] = a + (rnd.NextDouble() * (b - a));
        }

    }
    return matrix;
}

// Сложение матриц (номер 4)
static (double[,], bool) adding_matrix(double[,] matrix1, double[,] matrix2)
{
    int rows1 = matrix1.GetLength(0);
    int cols1 = matrix1.GetLength(1);
    int rows2 = matrix2.GetLength(0);
    int cols2 = matrix2.GetLength(1);

    if (rows1 == rows2 && cols1 == cols2)
    {
        double[,] matrix_sum = new double[rows1, cols1];
        for (int i = 0; i < rows1; i++)
        {
            for (int j = 0; j < cols1; j++)
            {
                matrix_sum[i, j] = matrix1[i, j] + matrix2[i, j];
            }

        }
        return (matrix_sum, true);
    }
    else
    {
        return (new double[0, 0], false);
    }

}

// Умножение матриц (номер 5)
static (double[,], bool) multiplication_matrix(double[,] matrix1, double[,] matrix2)
{
    int rows1 = matrix1.GetLength(0);
    int cols1 = matrix1.GetLength(1);
    int rows2 = matrix2.GetLength(0);
    int cols2 = matrix2.GetLength(1);
    double multi_sum;


    if (cols1 == rows2)
    {
        double[,] matrix_multi = new double[rows1, cols2];
        for (int i = 0; i < rows1; i++)
        {
            for (int j = 0; j < cols2; j++)
            {
                multi_sum = 0;
                for (int k = 0; k < cols1; k++)
                {
                    multi_sum += matrix1[i, k] * matrix2[k, j];

                }
                matrix_multi[i, j] = multi_sum;
            }
        }
        return (matrix_multi, true);


    }
    else
    {
        return (new double[0, 0], false);
    }
}


// Нахождение определителя (номер 6) 
static (double, bool) determinant_matrix(double[,] matrix)
{
    int rows = matrix.GetLength(0);
    int cols = matrix.GetLength(1);

    if (rows == cols)
    {
        int size = matrix.GetLength(0);

        if (size == 1)
        {
            return (matrix[0, 0], true);
        }

        if (size == 2)
        {
            return (matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0], true);
        }

        double det = 0;
        for (int j = 0; j < size; j++)
        {
            int sign = (j % 2 == 0) ? 1 : -1;
            double[,] minor = get_minor(matrix, 0, j);
            var (minor_det, success) = determinant_matrix(minor);
            if (!success)
            {
                return (0, false);
            }
            det += sign * matrix[0, j] * minor_det;
        }

        return (det, true);
    }
    else
    {
        return (0, false);
    }

}

// Функция для вычисления минора матрицы
static double[,] get_minor(double[,] matrix, int row, int col)
{
    int size = matrix.GetLength(0);
    double[,] minor_matrix = new double[size - 1, size - 1];
    int m_row = 0, m_col = 0;

    for (int i = 0; i < size; i++)
    {
        if (i == row) continue;

        m_col = 0;
        for (int j = 0; j < size; j++)
        {
            if (j == col) continue;

            minor_matrix[m_row, m_col] = matrix[i, j];
            m_col++;
        }
        m_row++;
    }

    return minor_matrix;
}


// Функция для нахождения обратной матрицы (номер 7)
static (double[,], bool) reverse_matrix(double[,] matrix)
{
    int rows = matrix.GetLength(0);
    int cols = matrix.GetLength(1);

    double[,] reverse_matrix = new double[rows, cols];
    double[,] transposed_matrix = transposition_matrix(matrix);


    var (det, success) = determinant_matrix(matrix);
    if (success && det != 0)
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                reverse_matrix[i, j] = (1 / Math.Abs(det)) * transposed_matrix[i, j];
            }

        }
        return (reverse_matrix, true);
    }
    else
    {
        return (new double[0, 0], false);
    }
}

// Функция для транспонирования матрицы (номер 8)
static double[,] transposition_matrix(double[,] matrix)
{
    int rows = matrix.GetLength(0);
    int cols = matrix.GetLength(1);
    double[,] transposed_matrix = new double[rows, cols];

    for (int i = 0; i < rows; i++)
    {
        for (int j = 0; j < cols; j++)
        {
            transposed_matrix[j, i] = matrix[i, j];
        }
    }
    return transposed_matrix;
}


// Функция для нахождения корней матрицы (номер 9)
static (double[], bool) sovling_matrix(double[,] matrix)
{
    int rows = matrix.GetLength(0);
    int cols = matrix.GetLength(1);

    if (cols != rows + 1)
    {
        return (null, false);
    }

    // матрица без правой части
    double[,] matrix_check = new double[rows, cols - 1];
    for (int i = 0; i < rows; i++)
    {
        for (int j = 0; j < cols - 1; j++)
        {
            matrix_check[i, j] = matrix[i, j];
        }
    }

    var (det, check) = determinant_matrix(matrix_check);
    if (det == 0 || check == false)
    {
        return (null, false);
    }

    for (int i = 0; i < rows; i++)
    {

        if (matrix[i, i] == 0)
        {
            bool swapped = false;
            for (int j = i + 1; j < rows; j++)
            {
                if (matrix[j, i] != 0)
                {
                    swap_rows(matrix, i, j);
                    swapped = true;
                    break;
                }
            }
            if (!swapped)
            {
                return (null, false);
            }
        }

        double lead = matrix[i, i];
        for (int j = i; j < cols; j++)
        {
            matrix[i, j] /= lead;
        }

        for (int k = 0; k < rows; k++)
        {
            if (k != i)
            {
                double factor = matrix[k, i];
                for (int j = i; j < cols; j++)
                {
                    matrix[k, j] -= factor * matrix[i, j];
                }
            }
        }
    }

    for (int i = 0; i < rows; i++)
    {
        bool all_zero = true;
        for (int j = 0; j < cols - 1; j++)
        {
            if (matrix[i, j] != 0)
            {
                all_zero = false;
                break;
            }
        }
        if (all_zero && matrix[i, cols - 1] != 0)
        {
            return (null, false);
        }
    }

    double[] result = new double[rows];
    for (int i = 0; i < rows; i++)
    {
        result[i] = matrix[i, cols - 1];
    }

    return (result, true);
}

// функция для перестановки строк матрицы
static void swap_rows(double[,] matrix, int row1, int row2)
{
    int cols = matrix.GetLength(1);
    for (int i = 0; i < cols; i++)
    {
        double temp = matrix[row1, i];
        matrix[row1, i] = matrix[row2, i];
        matrix[row2, i] = temp;
    }
}



// Вывод матрицы
static void print_matrix(double[,] matrix)
{
    int rows = matrix.GetLength(0);
    int cols = matrix.GetLength(1);

    for (int i = 0; i < rows; i++)
    {
        for (int j = 0; j < cols; j++)
        {
            Console.Write($"{matrix[i, j]} ");
        }
        Console.WriteLine();
    }
}

// Меню навигации по функциям
static int menu(double[,] matrix1, double[,] matrix2)
{
    Console.WriteLine("\n*********** Меню ***********");
    Console.WriteLine("1. Сложение матриц\n2. Умножение матриц\n3. Определитель матрицы\n4. Обратная матрица\n5. Транспонирование матрицы\n6. Нахождение корней матрицы");
    Console.Write("Введите число: ");

    int n = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine();

    // сложение матриц
    if (n == 1)
    {
        var (new_matrix, success) = adding_matrix(matrix1, matrix2);
        if (success)
        {
            Console.WriteLine("Полученная матрица:");
            print_matrix(new_matrix);
        }

        else
        {
            Console.WriteLine("Данные матрицы невозможно складывать!");
        }
    }

    // умножение матриц
    else if (n == 2)
    {
        var (new_matrix, success) = multiplication_matrix(matrix1, matrix2);
        if (success)
        {
            Console.WriteLine("Полученная матрица:");
            print_matrix(new_matrix);
        }

        else
        {
            Console.WriteLine("Данные матрицы невозможно умножать!");
        }
    }

    // определитель матрицы
    else if (n == 3)
    {
        Console.WriteLine("Для какой матрицы вы находите определитель?");
        Console.Write("Введите номер матрицы: ");

        int n_martx = Convert.ToInt32(Console.ReadLine());


        if (n_martx == 1)
        {
            var (det, success) = determinant_matrix(matrix1);
            if (success)
            {
                Console.WriteLine($"Полученный определитель: {det}");
            }

            else
            {
                Console.WriteLine("Для данной матрицы невозможно найти определитель!");
            }
        }


        else if (n_martx == 2)
        {
            var (det, success) = determinant_matrix(matrix2);
            if (success)
            {
                Console.WriteLine($"Полученный определитель: {det}");
            }

            else
            {
                Console.WriteLine("Для данной матрицы невозможно найти определитель!");
            }
        }

        else
        {
            Console.WriteLine("Введите верный номер матрицы!");
        }
    }

    // обратная матрица
    else if (n == 4)
    {
        Console.WriteLine("Для какой матрицы вы находите обратную матрицу?");
        Console.Write("Введите номер матрицы: ");

        int n_martx = Convert.ToInt32(Console.ReadLine());


        if (n_martx == 1)
        {
            var (new_matrix, success) = reverse_matrix(matrix1);
            if (success)
            {
                Console.WriteLine($"Полученная матрица:");
                print_matrix(new_matrix);
            }

            else
            {
                Console.WriteLine("Для данной матрицы невозможно найти обратную!");
            }
        }


        else if (n_martx == 2)
        {
            var (new_matrix, success) = reverse_matrix(matrix2);
            if (success)
            {
                Console.WriteLine($"Полученная матрица:");
                print_matrix(new_matrix);
            }

            else
            {
                Console.WriteLine("Для данной матрицы невозможно найти обратную!");
            }
        }

        else
        {
            Console.WriteLine("Введите верный номер матрицы!");
        }
    }

    // транспонированная матрица
    else if (n == 5)
    {
        Console.WriteLine("Для какой матрицы вы находите транспонированную матрицу?");
        Console.Write("Введите номер матрицы: ");

        int n_martx = Convert.ToInt32(Console.ReadLine());
        double[,] new_matrix;

        if (n_martx == 1)
        {
            new_matrix = transposition_matrix(matrix1);
            Console.WriteLine($"Полученная матрица:");
            print_matrix(new_matrix);

        }


        else if (n_martx == 2)
        {
            new_matrix = transposition_matrix(matrix2);
            Console.WriteLine($"Полученная матрица:");
            print_matrix(new_matrix);

        }

        else
        {
            Console.WriteLine("Введите верный номер матрицы!");
        }
    }

    // корни матриц
    else if (n == 6)
    {
        Console.WriteLine("Для какой матрицы вы находите корни?");
        Console.Write("Введите номер матрицы: ");

        int n_martx = Convert.ToInt32(Console.ReadLine());

        if (n_martx == 1)
        {
            var (sovles, success) = sovling_matrix(matrix1);
            if (success)
            {
                int len = sovles.Length;
                Console.WriteLine("Полученные корни:");
                for (int i = 0; i < len; i++)
                {
                    Console.Write($"{sovles[i]} ");
                }

            }

            else
            {
                Console.WriteLine("Для данной матрицы невозможно найти корни!");
            }

        }


        else if (n_martx == 2)
        {
            var (sovles, success) = sovling_matrix(matrix2);
            if (success)
            {
                int len = sovles.Length;
                Console.WriteLine("Полученные корни:");
                for (int i = 0; i < len; i++)
                {
                    Console.Write($"{sovles[i]} ");
                }
                Console.WriteLine();

            }

            else
            {
                Console.WriteLine("Для данной матрицы невозможно найти корни!");
            }

        }

        else
        {
            Console.WriteLine("Введите верный номер матрицы!");
        }


    }

    Console.WriteLine("Вы хотите продолжить? (1-да / 2-нет)");
    n = Convert.ToInt32(Console.ReadLine());
    if (n == 1)
    {
        return menu(matrix1, matrix2);
    }

    else
    {
        Console.WriteLine("Досвидания!");
    }
    return 0;
}


static (double[,], double[,]) start()
{
    int input;
    double[,] matrix1, matrix2;


    Console.WriteLine("Добро пожаловать!");

    Console.WriteLine("Создание первой матрицы...");
    matrix1 = creating_matrix();
    Console.WriteLine("\nСоздание второй матрицы...");
    matrix2 = creating_matrix();


    Console.WriteLine("\nЗаполнить первую матрицу с клавиатуры (1) или рандомно (2)?");
    Console.Write("Введите 1 или 2:");
    input = Convert.ToInt32(Console.ReadLine());
    if (input == 1)
    {
        matrix1 = fulling_matrix(matrix1);
    }
    else
    {
        matrix1 = rand_fulling_matrix(matrix1);
    }


    Console.WriteLine("\nЗаполнить вторую матрицу с клавиатуры (1) или рандомно (2)?");
    Console.Write("Введите 1 или 2:");
    input = Convert.ToInt32(Console.ReadLine());
    if (input == 1)
    {
        matrix2 = fulling_matrix(matrix2);
    }
    else
    {
        matrix2 = rand_fulling_matrix(matrix2);
    }


    Console.WriteLine("Первая матрица:");
    print_matrix(matrix1);
    Console.WriteLine("\n\nВторая матрица:");
    print_matrix(matrix2);


    return (matrix1, matrix2);
}



var (matrix1, matrix2) = start();
menu(matrix1, matrix2);

