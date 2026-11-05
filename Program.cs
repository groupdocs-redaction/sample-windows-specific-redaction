using GroupDocs.Redaction;
using GroupDocs.Redaction.Options;
using GroupDocs.Redaction.Redactions;
using System.Text.RegularExpressions;

Console.WriteLine("======================================");
Console.WriteLine("Redact phrase using Regex with GroupDocs.Redaction");

// This project uses the Windows-specific build of GroupDocs.Redaction for .NET
// to improve compatibility with applications running on Windows.
// The WindowsSpecificRedaction project targets net10.0-windows.
// When using a -windows target framework, NuGet restores the Windows-specific
// build of GroupDocs.Redaction, which includes Windows-specific dependencies
// and is optimized for Windows environments.


// Without a license, the library runs in evaluation mode and has some limitations. 
// Output documents will contain a trial badge. 
// You can request a temporary license from the GroupDocs team to evaluate the library 
// without limitations or badges. 
// Temporary license: https://purchase.groupdocs.com/temp-license/100492 
// Purchase a permanent license: https://purchase.groupdocs.com/buy 
// Free support: https://forum.groupdocs.com/c/redaction/33 
// Documentation: https://docs.groupdocs.com/redaction/net/

var license = new License();
//license.SetLicense("your_license_path");

if (!license.IsLicensed)
{
    Console.WriteLine("The library is running in evaluation mode. Some limitations may apply.");
}

string sourceFile = "document.docx";

using (Redactor redactor = new Redactor(sourceFile))
{
    // Replace all occurrences of the phrase " essential tool" with " [redacted]" using a regular expression
    var replacement_options = new ReplacementOptions(" [redacted]");

    var regex = new Regex(@"\s+essential tool", RegexOptions.IgnoreCase);

    var redaction = new RegexRedaction(regex, replacement_options);

    redactor.Apply(redaction);

    // Save the redacted document with a new name
    var saveOptions = new SaveOptions() { AddSuffix = true, RasterizeToPDF = false };

    // In Visual Studio, the result is saved to the \bin\Debug\net10.0-windows folder.
    var outputFile = redactor.Save(saveOptions);

    Console.WriteLine($"\nSource document was redacted successfully.\nFile saved to {outputFile}.\n");
}

Console.WriteLine("Redaction finished");
Console.WriteLine("======================================");