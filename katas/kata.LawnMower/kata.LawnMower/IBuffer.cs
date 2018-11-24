using System.Threading.Tasks;
using kata.LawnMower.Models;

namespace kata.LawnMower
{
    public interface IBuffer
    {
        Task<IPosition> Pop();
        Task<IPosition> Peek();
        void Add(IPosition position);
    }
}
