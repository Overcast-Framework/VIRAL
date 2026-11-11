using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Veyr.VIRAL
{
    public class VIRALPrinter
    {
        StringWriter _writer = new();

        public void WriteClass(IRClass cls)
        {
            _writer.WriteLine($".{(cls.IsExtern ? "ext " : "")}class {cls.Name}");
            _writer.WriteLine("{");
            foreach(var method in cls.Methods)
            {
                WriteFunction(method);
            }
            _writer.WriteLine("}");
        }

        public string WriteParameter(IRValue p)
        {
            return $"%{p.Id}: {TypeAsString(p.Type)}";
        }

        public void WriteFunction(IRFunction func)
        {
            string paramsAsText = "";
            for (int i = 0; i < func.Parameters.Count; i++)
            {
                paramsAsText += WriteParameter(func.Parameters[i]);
                if(i != func.Parameters.Count-1)
                {
                    paramsAsText += ", ";
                }
            }

            _writer.WriteLine($"\t{(func.Extern ? "ext " : "")}{(func.Static ? "stat" : "")} mtd {func.Name}({paramsAsText}) -> {TypeAsString(func.ReturnType)}");
            if (!func.Extern)
            {
                _writer.WriteLine("\t{");
                foreach (var block in func.Blocks)
                {
                    WriteBlock(block);
                }
                _writer.WriteLine("\t}");
            }
        }

        public void WriteBlock(BasicBlock block)
        {
            _writer.WriteLine($"\t{block.Name}:");
            foreach (var inst in block.Instructions)
            {
                _writer.WriteLine($"\t\t{WriteInstruction(inst)}");
            }
            
            switch(block.Terminator)
            {
                case ReturnTerminator retTerm:
                    if(retTerm.Value == null)
                    {
                        _writer.WriteLine($"\t\tret void");
                    }
                    else
                    {
                        _writer.WriteLine($"\t\tret %{retTerm.Value.Id}");
                    }
                    break;
            }
        }

        public string WriteAllArgs(List<IRValue> args)
        {
            string argsAsText = "";
            for (int i = 0; i < args.Count; i++)
            {
                argsAsText += "%"+args[i].Id;
                if (i != args.Count - 1)
                {
                    argsAsText += ", ";
                }
            }
            return argsAsText;
        }

        public string WriteInstruction(IRInstruction instr) => instr switch
        {
            ConstStringInstruction strInstruction => $"%{strInstruction.Result.Id} = const_str \"{strInstruction.Value}\"",
            CallStaticInstruction callStatInstr => $"call_stat {callStatInstr.Method.GetQualifiedName()}({WriteAllArgs(callStatInstr.Arguments)})",
            _ => throw new ArgumentException("Invalid instruction")
        };

        public void WriteImport(string import)
        {
            _writer.WriteLine($".import {import}");
        }

        public string TypeAsString(IRType type) => type switch
        {
            VoidIRType => "void",
            IntegerIRType intType => $"i{intType.Bits}",
            StringIRType => "str",
            ArrayIRType arrType => $"{TypeAsString(arrType.ElementType)}#",
            ClassIRType classType => classType.Class.Name,
            _ => throw new ArgumentException("Incorrect type")
        };

        public string Print(IRModule module)
        {
            _writer.WriteLine($".module {module.Name}");
            foreach(var imp in module.Imports) 
                WriteImport(imp);

            foreach(var cls in module.Classes)
                WriteClass(cls);

            _writer.Flush();
            return _writer.ToString();
        }
    }
}
