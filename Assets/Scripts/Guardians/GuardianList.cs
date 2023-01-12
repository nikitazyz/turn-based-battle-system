using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Guardians
{
    public class GuardianList : IReadOnlyList<GuardianCell>
    {
        private readonly IList<GuardianCell> _guardianCells;

        public GuardianCell this[int index]
        {
            get => _guardianCells[index];
            set => _guardianCells[index] = value;
        }

        public GuardianList(IEnumerable<GuardianCell> guardianCells)
        {
            _guardianCells = guardianCells.ToArray();
        }
        
        public IEnumerator<GuardianCell> GetEnumerator()
        {
            return _guardianCells.Cast<GuardianCell>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count => _guardianCells.Count;
    }
}