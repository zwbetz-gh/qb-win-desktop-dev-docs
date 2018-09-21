using System;
using System.Collections.Generic;
using QBFC13Lib;

public class Import
{
    public void ImportTimesheets()
    {
        try
        {
            IMsgSetRequest requestSet = SessionManager.Instance.CreateMsgSetRequest();
            requestSet.Attributes.OnError = ENRqOnError.roeStop;
            ITimeTrackingAdd timeTrackingAdd;

            // This is just an empty list
            // But let's assume you actually have a list of (custom) Timesheet objects
            List<Timesheet> tmList = new List<Timesheet>();

            foreach (Timesheet tm in tmList)
            {
                timeTrackingAdd = requestSet.AppendTimeTrackingAddRq();

                timeTrackingAdd.CustomerRef.FullName.SetValue(tm.CustomerJob);
                timeTrackingAdd.Duration.SetValue(tm.Hours);
                timeTrackingAdd.BillableStatus.SetValue(ENBillableStatus.bsBillable);
                timeTrackingAdd.EntityRef.FullName.SetValue(tm.EmployeeName);
                timeTrackingAdd.ItemServiceRef.FullName.SetValue(tm.ServiceItem);
                timeTrackingAdd.PayrollItemWageRef.FullName.SetValue(tm.PayrollItem);
                timeTrackingAdd.TxnDate.SetValue(tm.TransactionDate);
                timeTrackingAdd.Notes.SetValue(tm.Notes);
            }

            IMsgSetResponse responeSet = SessionManager.Instance.DoRequests(requestSet);
            IResponseList responseList = responeSet.ResponseList;

            if (responseList.Count > 0)
            {
                for (int i = 0; i < responseList.Count; i++)
                {
                    IResponse response = responseList.GetAt(i);
                    if (response.StatusCode == 0)
                    {
                        Console.WriteLine("Successfully imported timesheets");
                    }
                    else
                    {
                        Console.WriteLine("Response status message: " + response.StatusMessage);
                        Console.WriteLine("Response status code: " + response.StatusCode);
                    }
                }
            }
        }
    }
    catch (Exception ex)
    {
        // Handle exception
    }
}