namespace NesEmulator.Core
{
    public sealed class Cpu : NesComponent
    {
        private ushort _pc;
        private byte _sp;
        private byte _a;
        private byte _x;
        private byte _y;
        private StatusFlags _statusFlags;

        public Cpu(Emulator emulator) : base(emulator) { }

        public ushort PC
        {
            get => _pc; set => _pc = value;
        }

        public byte SP
        {
            get => _sp; set => _sp = value;
        }

        public byte A
        {
            get => _a; set => _a = value;
        }

        public byte X
        {
            get => _x; set => _x = value;
        }

        public byte Y
        {
            get => _y; set => _y = value; 
        }

        public StatusFlags StatusFlags
        {
            get => _statusFlags; set => _statusFlags = value;
        }
    }
}