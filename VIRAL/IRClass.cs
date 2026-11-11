using System;
using System.Collections.Generic;
using System.Text;

namespace Veyr.VIRAL
{
    public class IRClass
    {
        public bool IsExtern { get; }
        public string Name { get; }
        public IRModule DeclaringModule { get; internal set; }
        public List<IRFunction> Methods { get; }

        public void InsertMethod(IRFunction func)
        {
            Methods.Add(func);
        }

        public IRClass(string name, List<IRFunction> methods, bool isExtern = false)
        {
            Name = name;
            Methods = methods;
            IsExtern = isExtern;
        }
    }
}
