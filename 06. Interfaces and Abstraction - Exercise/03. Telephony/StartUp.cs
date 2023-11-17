using Telephony.Core;
using Telephony.Core.Interfaces;
using Telephony.Models;
using Telephony.Models.Interfaces;
using Telephony.IO;

namespace Telephony;

public class StartUp
{
    static void Main(string[] args)
    {
        IEngine engine = new Engine(new ConsoleReader(), new ConsoleWriter());

        engine.Run();
    }
}