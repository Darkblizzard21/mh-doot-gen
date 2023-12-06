using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doot_gen.util
{
    internal class CursorOverride
    {
        Cursor oldCursor;

        public CursorOverride(Cursor newCursor) {
            oldCursor = Cursor.Current;
            Cursor.Current = newCursor;
        }

        ~CursorOverride()
        {
            Cursor.Current = oldCursor;
        }
    }
}
