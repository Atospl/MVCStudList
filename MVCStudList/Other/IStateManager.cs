using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCStudList.Other
{
    public interface IStateManager<T>
    {
        void Save(string name, T state);
        T Load(string name);
    }
}