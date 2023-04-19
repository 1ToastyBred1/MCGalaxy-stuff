using System;
using MCGalaxy;

public class Cmd8bit : Command
{
    public override string name { get { return "8-bit"; } }
    public override string shortcut { get { return ""; } }
    public override string type { get { return "other"; } }

    public override bool museumUsable { get { return true; } }
    public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }



    public override void Use(Player p, string message)
    {
    byte accumulator = 0;
    byte[] memory = new byte[256];

    int programCounter = 0;
    bool running = true;

        // Program code
        memory[0] = 0x0A; // LDA 10
        memory[1] = 0x05; // ADD 5
        memory[2] = 0x0B; // STA 11
        memory[3] = 0x00; // HLT
        while (running)
        {
            byte instruction = memory[programCounter];
            programCounter++;

            switch (instruction)
            {
                case 0x00: // HLT
                    running = false;
                    break;

                case 0x0A: // LDA
                    accumulator = memory[programCounter];
                    programCounter++;
                    break;

                case 0x05: // ADD
                    accumulator += memory[programCounter];
                    programCounter++;
                    break;

                case 0x0B: // STA
                    memory[programCounter] = accumulator;
                    programCounter++;
                    break;

                default:
                    p.Message("Unknown instruction: " + instruction);
                    break;
            }
        }

        p.Message("Result: " + memory[11]);
    }

    public override void Help(Player p)
    {
        p.Message("/8-bit - 8bit pc emulator.");
    }
}
