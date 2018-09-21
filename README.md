# QuickBooks (QB) Windows Desktop development docs

## Setup QB Windows desktop trial

A trial version of QB Desktop needs to be installed so that you have something to develop against:

1. Navigate to [QB trial downloads](https://community.intuit.com/articles/1207255-quickbooks-desktop-trial-links-us-uk-ca)
1. **QuickBooks Desktop trials for the United States (US)** > **QuickBooks Pro 2019 30-day trial (US only)**

If you get a [blank screen](https://community.intuit.com/articles/1501402-re-create-damaged-entitlementdatastore-ecml-file-to-resolve-license-and-registration-issues) when first opening QB, do the following:

1. End all instances of `QBW32.EXE` in task manager
1. Delete file `C:\ProgramData\Intuit\Entitlement Client\v8\EntitlementDataStore.ecml`
1. Reopen QB

The QB trial lasts 30 days. To "reactivate" it once it expires, do the above steps again. 

## Install QB SDK and add QBFC library to Visual Studio

The full QB Desktop SDK needs to be installed on your development machine. It comes with more than just the QBFC library, like documentation, example projects, etc.

1. Navigate to [QB SDK downloads](https://developer.intuit.com/docs/0200_quickbooks_desktop/0400_tools/quickbooks_desktop/download_the_sdk)
1. Select **Desktop SDK 13.0 Installer**

In Visual Studio, add the QBFC library as a reference:

1. Right-click **References** > **Add Reference...** > **Browse...** > **COM** > check **qbFC13 1.0 Type Library** > **OK**

## QB SDK documentation

QB SDK documentation can be found at `C:\Program Files (x86)\Intuit\IDN\QBSDK13.0\doc\pdf`.

## Code examples

Example usage of `SessionManager.cs`, `Query.cs`, and `Import.cs`:

```cs
// Open connection and begin session
SessionManager.Instance.OpenConnection();
SessionManager.Instance.BeginSession();
Console.WriteLine("Currently open QB filename: " + SessionManager.Instance.GetCurrentCompanyFileName());

// Query all employee names
Query query = new Query();
List<string> employeeNames = query.QueryEmployeeNames();

foreach (string name in employeeNames)
{
    Console.WriteLine(name);
}

// Import a list of timeseets
Import import = new Import();
// This is just an empty list
// But let's assume you actually have a list of (custom) Timesheet objects
List<Timesheet> tmList = new List<Timesheet>();
import.ImportTimesheets(tmList); 

// End session and close connection
SessionManager.Instance.EndSession();
SessionManager.Instance.CloseConnection();
```
