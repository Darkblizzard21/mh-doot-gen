using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace doot_gen.util
{
    internal class CursorOverride
    {
        static Stack<Cursor> stack = new Stack<Cursor>();

        public CursorOverride(Cursor newCursor) {
            stack.Push(Cursor.Current);
            Cursor.Current = newCursor;
        }

        ~CursorOverride()
        { 
            Cursor.Current = stack.Pop();
        }
    }
}
