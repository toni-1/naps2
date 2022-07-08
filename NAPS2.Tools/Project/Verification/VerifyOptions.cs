using CommandLine;

namespace NAPS2.Tools.Project.Verification;

[Verb("verify", HelpText = "Verify the packaged app, 'verify {all|exe|msi|zip}'")]
public class VerifyOptions
{
    [Value(0, MetaName = "what", Required = true, HelpText = "all|exe|msi|zip")]
    public string? What { get; set; }
    
    [Option('p', "platform", Required = false, HelpText = "win32|win64|mac|macarm|linux")]
    public string? Platform { get; set; }
}