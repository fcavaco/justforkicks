using System.Collections.Generic;
using System.Threading.Tasks;
using kata.LawnMower.Models;

namespace kata.LawnMower
{
    public class DeviceBuffer : IBuffer
    {
        private static Stack<IPosition> _buffer;

        public DeviceBuffer()
        {
            _buffer = new Stack<IPosition>();
        }

        public void Add(IPosition position)
        {
            lock (_buffer)
            {
                 _buffer.Push(position);
            }
        }

        public async Task<IPosition> Peek()
        {
            return _buffer.Peek();
        }

        public async Task<IPosition> Pop()
        {
            return _buffer.Pop();
        }
    }
}
