using System;
using System.Linq;
using System.Text;

namespace GriddlerSolver
{
    internal class Griddler
    {
        public Griddler(string name, int columns, int rows)
        {
            this.Name = name;
            this.Height = rows;
            this.Width = columns;
            this.Grid = new bool?[this.Height, this.Width];
        }

        //// horizontal = horizontally laid out, so about columns
        //private Griddler(string name, int[][] columnClues, int[][] rowClues)
        //{
        //    this.Height = rowClues.Length;
        //    this.Width = columnClues.Length;
        //    this.Name = name;
        //    this.ColumnClues = columnClues;
        //    this.RowClues = rowClues;
        //    this.Grid = new bool?[this.Height, this.Width];

        //    this.SanityCheck(this.ColumnClues, this.RowClues);
        //}

        //private Griddler(string name, string columnClues, string rowClues)
        //{
        //    // do NOT ignore empty parts!
        //    var cols = columnClues.Split(';').Select(s => s.Trim()).ToArray();
        //    var rows = rowClues.Split(';').Select(s => s.Trim()).ToArray();

        //    this.Height = rows.Length;
        //    this.Width = cols.Length;

        //    this.ColumnClues = cols.Select(s => string.IsNullOrEmpty(s) ? new int[0] : s.Split(',').Select(c => int.Parse(c)).ToArray()).ToArray();
        //    this.RowClues = rows.Select(s => string.IsNullOrEmpty(s) ? new int[0] : s.Split(',').Select(c => int.Parse(c)).ToArray()).ToArray();
        //    this.Grid = new bool?[this.Height, this.Width];

        //    this.SanityCheck(this.ColumnClues, this.RowClues);
        //    this.Name = name;
        //}

        public int Height { get; }
        public int Width { get; }
        public string Name { get; }

        // about columns
        public int[][] ColumnClues { get; private set; }

        // about rows
        public int[][] RowClues { get; private set; }

        public bool?[,] Grid { get; }

        public double Completeness
        {
            get
            {
                int done = 0;
                for (int row = 0; row < this.Height; row++)
                {
                    for (int col = 0; col < this.Width; col++)
                    {
                        if (this.Grid[row, col].HasValue)
                        {
                            done++;
                        }
                    }
                }

                return ((double)done) / (this.Height * this.Width);
            }
        }

        public void SetColumnClues(int[][] columnClues)
        {
            // sanity check
            if (columnClues.GetLength(0) != this.Width)
            {
                throw new ArgumentException($"The size of the column array should be {this.Width}, not {columnClues.GetLength(0)}");
            }

            this.ColumnClues = columnClues;
        }

        public void SetColumnClues(string columnClues)
        {
            var cols = columnClues.Split(';').Select(s => s.Trim()).ToArray();
            this.ColumnClues = cols.Select(s => string.IsNullOrEmpty(s) ? new int[0] : s.Split(',').Select(c => int.Parse(c)).ToArray()).ToArray();

            if (this.ColumnClues.GetLength(0) != this.Width)
            {
                throw new ArgumentException($"The size of the column array should be {this.Width}, not {this.ColumnClues.GetLength(0)}");
            }
        }

        public void SetRowClues(int[][] rowClues)
        {
            // sanity check
            if (rowClues.GetLength(0) != this.Height)
            {
                throw new ArgumentException($"The size of the row array should be {this.Height}, not {rowClues.GetLength(0)}");
            }

            this.RowClues = rowClues;
        }

        public void SetRowClues(string rowClues)
        {
            var rows = rowClues.Split(';').Select(s => s.Trim()).ToArray();
            this.RowClues = rows.Select(s => string.IsNullOrEmpty(s) ? new int[0] : s.Split(',').Select(c => int.Parse(c)).ToArray()).ToArray();

            // sanity check
            if (this.RowClues.GetLength(0) != this.Height)
            {
                throw new ArgumentException($"The size of the row array should be {this.Height}, not {this.RowClues.GetLength(0)}");
            }
        }

        // set the value for this position, return whether the value was changed
        public bool SetValue(int col, int row, bool value)
        {
            var oldval = this.Grid[row, col];
            this.Grid[row, col] = value;
            return oldval != value;
        }

        public int GetGroupSize(GroupType group)
        {
            return group == GroupType.Row ? this.Width : this.Height;
        }

        public bool?[] GetGroup(GroupType group, int index)
        {
            if (group == GroupType.Row)
            {
                return Enumerable.Range(0, this.Width)
                    .Select(c => this.Grid[index, c])
                    .ToArray();
            }

            return Enumerable.Range(0, this.Height)
                    .Select(r => this.Grid[r, index])
                    .ToArray();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int row = 0; row < this.Height; row++)
            {
                for (int col = 0; col < this.Width; col++)
                {
                    switch (this.Grid[row, col])
                    {
                        case null:
                            sb.Append("▒"); //(" "); // en-space
                            break;
                        case true:
                            sb.Append("█");
                            break;
                        case false:
                            sb.Append("░");
                            break;
                    }
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }

        public void SanityCheck()
        {
            var colblacks = this.ColumnClues.Select(a => a.Sum()).Sum();
            var rowblacks = this.RowClues.Select(a => a.Sum()).Sum();

            if (colblacks != rowblacks)
            {
                throw new InvalidOperationException($"The number of black fields must match between rows and columns. In cols: {colblacks}, in rows: {rowblacks}.");
            }
        }
    }
}
