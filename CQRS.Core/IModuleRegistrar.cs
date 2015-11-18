using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core
{
    public interface IModuleRegistrar
    {
        void RegisterType<TFrom, TTo>(bool withInterception = false) where TTo : TFrom;
        void RegisterTypeWithContainerControlledLife<TFrom, TTo>(bool withInterception = false) where TTo : TFrom;
        void RegisterTypeWithPerRequestLife<TFrom, TTo>(bool withInterception = false) where TTo : TFrom;
    }
}
