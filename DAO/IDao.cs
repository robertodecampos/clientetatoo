using System;

namespace ClienteTatoo.DAO
{
    interface IDao<in Object, in Transaction> : IDisposable
    {
        int Insert(Object model, Transaction transaction);

        int Update(Object model, Transaction transaction);

        int Remove(Object model, Transaction transaction);
    }
}
