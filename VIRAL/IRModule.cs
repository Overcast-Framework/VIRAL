using System;
using System.Collections.Generic;
using System.Text;

namespace Veyr.VIRAL
{
    public class IRModule
    {
        public string Name { get; set; }
        public List<IRClass> Classes { get; } = [];
        public List<string> Imports { get; } = [];

        public void InsertClass(IRClass cls)
        {
            if(!cls.IsExtern)
                cls.DeclaringModule = this;
            Classes.Add(cls);
        }

        public void InsertImport(string imp)
        {
            Imports.Add(imp);
        }

        public IRModule(string name)
        {
            Name = name;
        }
    }
}
