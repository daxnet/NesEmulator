using NesEmulator.Core;
using NesEmulator.Core.OpCodes;
using System.Diagnostics;
using System.Text;

//using var fileStream = new FileStream(@"nesemulator.log.txt", FileMode.Create, FileAccess.Write);
var cartridge = new Cartridge("nestest.nes");
using var emulator = new Emulator(new EmulatorOptions
{
    EnableLogging = true,
    LogOutputStream = Console.OpenStandardOutput()
});


emulator.Cpu.LoadAndRun(cartridge.PrgRom, address: 0xC000);
//Console.ReadLine();
