using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Core.OpCodes
{
    public abstract class OpCode
    {
        public void Execute(byte opcode, Cpu cpu)
        {
            var opCodeDefinition = cpu.GetOpCodeDefinition(opcode);
            DoExecute(cpu, opCodeDefinition);
            IncreaseProgramCounter(cpu, opCodeDefinition);
        }

        protected abstract void DoExecute(Cpu cpu, OpCodeDefinitionAttribute opCodeDefinition);

        protected virtual void IncreaseProgramCounter(Cpu cpu, OpCodeDefinitionAttribute opCodeDefinition) => cpu.PC += (ushort)(opCodeDefinition.Bytes - 1);

        public override string ToString() => this.GetType().Name;
    }
}
