
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
