using System;
using System.Collections.Generic;
using QBFC13Lib;

public class Query
{
    public List<string> QueryEmployeeNames()
    {
        try
        {
            IMsgSetRequest requestSet = SessionManager.Instance.CreateMsgSetRequest();
            requestSet.Attributes.OnError = ENRqOnError.roeStop;
            IEmployeeQuery employeeQuery = requestSet.AppendEmployeeQueryRq();

            IMsgSetResponse responeSet = SessionManager.Instance.DoRequests(requestSet);
            IResponseList responseList = responeSet.ResponseList;

            List<string> employeeNames = new List<string>();

            for (int i = 0; i < responseList.Count; i++)
            {
                IResponse response = responseList.GetAt(i);
                if (response.StatusCode == 0)
                {
                    IEmployeeRetList employeeList = (IEmployeeRetList) response.Detail;
                    for (int j = 0; j < employeeList.Count; j++)
                    {
                        IEmployeeRet employee = employeeList.GetAt(j);
                        employeeNames.Add(employee.Name.GetValue());
                    }
                }
            }

            return employeeNames;
        }
        catch (Exception ex)
        {
            // Handle exception
        }
    }
}