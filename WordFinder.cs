namespace WordFinderApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class WordFinder
    {
        private readonly char[,] _characterMatrix;

        public WordFinder(IEnumerable<string> matrix)
        {
            var rowsCount = matrix.Count();
            var columnsCount = matrix.First().Length;

            ValidateInitialize(matrix, rowsCount, columnsCount);

            _characterMatrix = new char[rowsCount, columnsCount];

            for (int i = 0; i < rowsCount; i++)
            {
                for (int j = 0; j < columnsCount; j++)
                {
                    _characterMatrix[i, j] = matrix.ElementAt(i)[j];
                }
            }
        }

        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {
            if (wordstream == null || !wordstream.Any())
            {
                return Enumerable.Empty<string>();
            }

            var wordCounts = new Dictionary<string, int>();

            foreach (var word in wordstream)
            {
                var searchCount = SearchWord(word);

                if (searchCount > 0)
                {
                    wordCounts.Add(word, searchCount); 
                }
            }

            var topWords = wordCounts.OrderByDescending(kv => kv.Value).Take(10).Select(kv => kv.Key);

            return topWords;
        }

        public void PrintMatrix()
        {
            for (int i = 0; i < _characterMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < _characterMatrix.GetLength(1); j++)
                {
                    Console.Write(_characterMatrix[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        private int SearchWord(string word)
        {
            var searchCount = 0;

            for (int i = 0; i < _characterMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < _characterMatrix.GetLength(1); j++)
                {
                    if (SearchWordFromPosition(word, i, j))
                    {
                        searchCount++;
                    }
                }
            }

            return searchCount;
        }

        private bool SearchWordFromPosition(string word, int row, int col)
        {
            if (col + word.Length > _characterMatrix.GetLength(1) && row + word.Length > _characterMatrix.GetLength(0))
            {
                return false;
            }

            for (int i = 0; i < word.Length; i++)
            {
                if (col + i >= _characterMatrix.GetLength(1) || _characterMatrix[row, col + i] != word[i])
                {
                    break;
                }

                if (i == word.Length - 1)
                {
                    return true;
                }
            }

            for (int i = 0; i < word.Length; i++)
            {
                if (row + i >= _characterMatrix.GetLength(0) || _characterMatrix[row + i, col] != word[i])
                {
                    break;
                }

                if (i == word.Length - 1)
                {
                    return true;
                }
            }

            return false;
        }

        private void ValidateInitialize(IEnumerable<string> matrix, int rowsCount, int columnsCount)
        {
            if (matrix == null || !matrix.Any())
            {
                throw new ArgumentException("Matrix must not be null or empty.");
            }

            if (rowsCount > 64 || columnsCount > 64)
            {
                throw new ArgumentException("Matrix size should not exceed 64x64.");
            }

            if (matrix.Any(row => row.Length != columnsCount))
            {
                throw new ArgumentException("All strings in the matrix must have the same number of characters.");
            }
        }
    }
}