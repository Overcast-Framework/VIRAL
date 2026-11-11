using System;
using System.Collections.Generic;
using System.Text;

namespace Veyr.VIRAL
{
    public class IRFunction
    {
        public bool Static { get; } = false;
        public string Name { get; }

        public bool Extern { get; } = false;

        public IRType ReturnType { get; }
        public IRClass? DeclaringClass { get; private set; }

        public List<BasicBlock> Blocks { get; } = [];
        public List<IRValue> Parameters { get; } = [];

        public void SetClass(IRClass cls)
        {
            if (DeclaringClass == null)
                DeclaringClass = cls;
            else
                throw new Exception("IR function already has a declared class");
        }

        public string GetQualifiedName()
        {
            var parts = new[]
            {
                DeclaringClass?.DeclaringModule?.Name,
                DeclaringClass?.Name,
                Name
            };

            return string.Join(".", parts.Where(p => !string.IsNullOrEmpty(p)));
        }

        public IRFunction(
            string name,
            IRType returnType,
            bool isStatic = false,
            bool isExtern = false,
            IRClass? declaringClass = null)
        {
            Name = name;
            ReturnType = returnType;
            Static = isStatic;
            Extern = isExtern;
            DeclaringClass = declaringClass;
        }
    }
}
