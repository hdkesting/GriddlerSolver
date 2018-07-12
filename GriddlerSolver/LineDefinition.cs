namespace GriddlerSolver
{
    internal struct LineDefinition
    {
        public LineDefinition(GroupType groupType, int index)
        {
            this.GroupType = groupType;
            this.Index = index;
        }

        public GroupType GroupType { get; }
        public int Index { get; }

        public void Deconstruct(out GroupType groupType, out int index)
        {
            groupType = this.GroupType;
            index = this.Index;
        }
    }
}
