using System;

namespace kata.ms.shoppingbasket
{
    public interface IDataSourceService<T>
    {
        void Get(Guid guid);
    }
}