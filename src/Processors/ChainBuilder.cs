
using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace Utils.Codestat.Processors
{
    public class ChainBuilder : IChainBuilder<IFileProcessor>
    {
        protected IList<Type> _types2Construct = new List<Type>();

        public IChainBuilder<IFileProcessor> Next<TDerived>()
        {
            _types2Construct.Add(typeof(TDerived));
            return this;
        }

        public IChainBuilder<IFileProcessor> Decorate<TDerived>()
        {
            _types2Construct.Insert(0, typeof(TDerived));
            return this;
        }

        public IFileProcessor Build()
        {
            if (_types2Construct.Count == 0)
                throw new InvalidOperationException("You have to add at least one type to build.");

            var types = _types2Construct.Reverse();

            IFileProcessor chain = null;
            chain = (IFileProcessor)Activator.CreateInstance(types.First());

            if (types.Count() == 1)
                return chain;

            types = types.Skip(1); // first instance already constructed
            foreach (var type in types)
                chain = (IFileProcessor)Activator.CreateInstance(type, chain);

            return chain;
        }
    }
}