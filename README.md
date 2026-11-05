# Redacting a Phrase in a Word Document on Windows

[![Product Page](https://img.shields.io/badge/Product%20Page-2865E0?style=for-the-badge&logo=appveyor&logoColor=white)](https://products.groupdocs.com/redaction/net/)
[![Docs](https://img.shields.io/badge/Docs-2865E0?style=for-the-badge&logo=Hugo&logoColor=white)](https://docs.groupdocs.com/redaction/net/)
[![Blog](https://img.shields.io/badge/Blog-2865E0?style=for-the-badge&logo=WordPress&logoColor=white)](https://blog.groupdocs.com/categories/groupdocs.redaction-product-family/)
[![Free Support](https://img.shields.io/badge/Free%20Support-2865E0?style=for-the-badge&logo=Discourse&logoColor=white)](https://forum.groupdocs.com/c/redaction/33)
[![Temporary License](https://img.shields.io/badge/Temporary%20License-2865E0?style=for-the-badge&logo=rocket&logoColor=white)](https://purchase.groupdocs.com/temp-license/100492)

# Using Windows-specific builds of GroupDocs.Redaction for .NET

A **GroupDocs.Redaction for .NET** sample that redacts a phrase in a Word document with the Windows-specific build.

The project targets `net10.0-windows`. For that target framework, NuGet restores `GroupDocs.Redaction.Net100.Windows`. That build includes Windows-specific dependencies, such as `System.Drawing.Common`. The regular `net10.0` target framework keeps the cross-platform build.

## What This Example Shows

This sample demonstrates how to:

* target `net10.0-windows` so the Windows-specific build is restored;
* check `License.IsLicensed` before processing a document;
* redact a phrase with `RegexRedaction`;
* save the Word document in its original format as `document_Redacted.docx`.

The sample uses **GroupDocs.Redaction 26.10.0**.

## Prerequisites

* **Windows**
* **.NET SDK 10.0**
* **GroupDocs.Redaction for .NET 26.10.0** — restored from NuGet
* **License file** — optional. Without a license, the sample runs in evaluation mode.

In evaluation mode, one process can open up to 25 documents. Saved documents contain a trial badge.

## Project Structure

```text
WindowsSpecificRedaction/
│
├── Program.cs
├── WindowsSpecificRedaction.csproj
├── WindowsSpecificRedaction.slnx
└── document.docx
```

* `Program.cs` — checks the license, redacts the phrase, and saves the result.
* `WindowsSpecificRedaction.csproj` — targets `net10.0-windows` and references GroupDocs.Redaction 26.10.0.
* `document.docx` — input Word document.

## Code Example

### Check the license and redact a phrase

`License.IsLicensed` is `false` until `SetLicense` loads a valid license. Uncomment `SetLicense` and pass the path to a license file to leave evaluation mode.

`RegexRedaction` matches `\s+essential tool`. The search is case-insensitive. Each match is replaced with ` [redacted]`.

`RasterizeToPDF` is `false`, so the sample saves a Word document. `AddSuffix` is `true`, so the output name is `document_Redacted.docx`.

```csharp
using GroupDocs.Redaction;
using GroupDocs.Redaction.Options;
using GroupDocs.Redaction.Redactions;
using System.Text.RegularExpressions;

var license = new License();
// license.SetLicense("your_license_path");

if (!license.IsLicensed)
{
    Console.WriteLine("The library is running in evaluation mode. Some limitations may apply.");
}

using (Redactor redactor = new Redactor("document.docx"))
{
    var regex = new Regex(@"\s+essential tool", RegexOptions.IgnoreCase);
    redactor.Apply(new RegexRedaction(regex, new ReplacementOptions(" [redacted]")));

    var saveOptions = new SaveOptions() { AddSuffix = true, RasterizeToPDF = false };
    var outputFile = redactor.Save(saveOptions);
}
```

`Save` returns the output path. `Apply` returns the redaction status. The sample prints a success message after `Save`.

## Run the Sample

`document.docx` is included in the project and copied to the build output.

### Visual Studio

1. Open `WindowsSpecificRedaction.slnx`.
2. Press **F5** to debug or **Ctrl+F5** to run without debugging.

Visual Studio uses the output directory as the working directory. The redacted file is saved to `bin\Debug\net10.0-windows\document_Redacted.docx`.

### .NET CLI

From the project directory:

```bash
dotnet run
```

`dotnet run` uses the project directory as the working directory. The application reads `document.docx` there and writes `document_Redacted.docx` next to it.

## Related Documentation

* **[Text redaction](https://docs.groupdocs.com/redaction/net/text-redactions/)** — phrase and regular-expression redaction.
* **[Redaction basics](https://docs.groupdocs.com/redaction/net/redaction-basics/)** — redaction results and `Applied`, `Skipped`, and `Failed` statuses.
* **[Save in the original format](https://docs.groupdocs.com/redaction/net/save-in-original-format/)** — how `SaveOptions` works when PDF rasterization is disabled.
* **[System requirements](https://docs.groupdocs.com/redaction/net/system-requirements/)** — supported operating systems, including the Windows-specific target frameworks.
* **[Evaluation and licensing](https://docs.groupdocs.com/redaction/net/evaluation-limitations-and-licensing/)** — evaluation mode, the 25-document limit, and `License.IsLicensed`.
* **[Migration notes](https://docs.groupdocs.com/redaction/net/migration-notes/)** — Windows-specific builds introduced in version 26.10.
* **[GroupDocs.Redaction for .NET 26.10 release notes](https://releases.groupdocs.com/redaction/net/release-notes/2026/groupdocs-redaction-for-net-26-10-release-notes/)** — changes introduced in version 26.10.
