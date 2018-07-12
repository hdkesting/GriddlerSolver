using System.Linq;
using System.Text;

namespace GriddlerSolver
{
    internal class Griddler
    {
        // horizontal = horizontally laid out, so about columns
        public Griddler(int[][] columnClues, int[][] rowClues)
        {
            Height = rowClues.Length;
            Width = columnClues.Length;
            ColumnClues = columnClues;
            RowClues = rowClues;
            Grid = new bool?[Height, Width];
        }

        public Griddler(string columnClues, string rowClues)
        {
            // do NOT ignore empty parts!
            var cols = columnClues.Split(';').Select(s => s.Trim()).ToArray();
            var rows = rowClues.Split(';').Select(s => s.Trim()).ToArray();

            this.Height = rows.Length;
            this.Width = cols.Length;

            this.ColumnClues = cols.Select(s => string.IsNullOrEmpty(s) ? new int[0] : s.Split(',').Select(c => int.Parse(c)).ToArray()).ToArray();
            this.RowClues = rows.Select(s => string.IsNullOrEmpty(s) ? new int[0] : s.Split(',').Select(c => int.Parse(c)).ToArray()).ToArray();
            Grid = new bool?[Height, Width];
        }

        public int Height { get; }
        public int Width { get; }

        // about columns
        public int[][] ColumnClues { get; }

        // about rows
        public int[][] RowClues { get; }

        public bool?[,] Grid { get; }

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
    }
}
