using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GriddlerSolver
{
    internal class Solver
    {
        private readonly Queue<LineDefinition> processQueue = new Queue<LineDefinition>();
        private readonly Griddler game;

        public Solver(Griddler griddler)
        {
            for (int i = 0; i < griddler.Width; i++)
            {
                processQueue.Enqueue(new LineDefinition(GroupType.Column, i));
            }

            for (int i = 0; i < griddler.Height; i++)
            {
                processQueue.Enqueue(new LineDefinition(GroupType.Row, i));
            }

            this.game = griddler;
        }

        public IEnumerable<Griddler> Solve()
        {
            while (processQueue.Any())
            {
                var (group, index) = processQueue.Dequeue();

                var pattern = group == GroupType.Column ? game.ColumnClues[index] : game.RowClues[index];
                var existing = game.GetGroup(group, index);

                if (existing.All(x => x.HasValue))
                {
                    // nothing left to do, just skip
                    continue;
                }

                var options = MakePermutations(pattern, game.GetGroupSize(group))
                    .Where(p => MatchesExisting(p, existing))
                    .ToList();

                bool foundone = false;
                //options.Dump();
                // are there positions that have the same values in all remaining options?
                // then set that value
                // if value was changed, then add both column and row to queue
                if (options.Any())
                {
                    var merged = this.Merge(options);

                    for (int i = 0; i < merged.Length; i++)
                    {
                        if (merged[i].HasValue && !existing[i].HasValue)
                        {
                            existing[i] = merged[i];
                            if (group == GroupType.Row)
                            {
                                game.SetValue(i, index, merged[i].Value);
                            }
                            else
                            {
                                game.SetValue(index, i, merged[i].Value);
                            }

                            foundone = true;
                            processQueue.Enqueue(new LineDefinition(group == GroupType.Row ? GroupType.Column : GroupType.Row, i));
                        }
                    }
                }

                //queue.Count.Dump("Queue count");
                if (foundone)
                {
                    yield return game;
                }
            }
        }

        private bool?[] Merge(IList<bool[]> options)
        {
            // start with the first one
            var result = options.First().Select(b => (bool?)b).ToArray();

            // erase all non-matching values
            foreach (var option in options.Skip(1))
            {
                for (int i = 0; i < option.Length; i++)
                {
                    if (result[i].HasValue)
                    {
                        if (result[i].Value != option[i])
                        {
                            result[i] = null;
                        }
                    }
                }
            }

            return result;
        }

        private bool MatchesExisting(bool[] lineSuggestion, bool?[] existingLine)
        {
            // array sizes should match!
            for (int i = 0; i < existingLine.Length; i++)
            {
                if (existingLine[i].HasValue && existingLine[i].Value != lineSuggestion[i])
                {
                    return false;
                }
            }

            return true;
        }

        private IEnumerable<bool[]> MakePermutations(int[] blockPattern, int size)
        {
            foreach (var result in MakePermutations(new bool?[size], blockPattern, 0))
            {
                yield return result;
            }
        }

        // fillPattern: current partially filled-in line, to be filled from the blockPattern
        // blockPattern: black (true) lengths to add to fillPattern from "start"
        // start: start filling at this position
        private IEnumerable<bool[]> MakePermutations(bool?[] fillPattern, int[] blockPattern, int start)
        {
            if (blockPattern.Length == 0)
            {
                yield return fillPattern.Select(b => b.GetValueOrDefault()).ToArray();
                yield break;
            }

            // first block to be processed now
            var thisBlockLength = blockPattern[0];
            // the rest to be processed recursively. Add spaces separating them from this block
            var restLength = blockPattern.Skip(1).Select(l => l + 1).Sum();
            var restPattern = blockPattern.Skip(1).ToArray();
            var size = fillPattern.Length;

            for (int s = start; s <= size - restLength - thisBlockLength; s++)
            {
                var newline = FillArray(fillPattern, s, thisBlockLength);
                foreach (var line in MakePermutations(newline, restPattern, s + thisBlockLength + 1))
                {
                    yield return line;
                }
            }
        }

        private bool?[] FillArray(bool?[] group, int start, int length)
        {
            var copy = group.ToArray(); // copy
            for (int i = start; i < start + length; i++)
            {
                copy[i] = true;
            }

            return copy;
        }
    }
}
