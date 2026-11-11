using System;
using System.Collections.Generic;
using System.Text;

namespace Veyr.VIRAL
{
    public class BasicBlock
    {
        public string Name { get; }
        public List<IRInstruction> Instructions { get; } = [];
        public IRTerminator? Terminator { get; set; }

        public BasicBlock(string name)
        {
            Name = name;
        }
    }
}
