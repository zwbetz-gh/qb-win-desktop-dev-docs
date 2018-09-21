using System;
using QBFC13Lib;

// Singleton pattern by Jon Skeet, see below article
// http://csharpindepth.com/Articles/General/Singleton.aspx

public sealed class SessionManager
{
    private static readonly SessionManager instance = new SessionManager();

    bool isConnectionOpen = false;
    bool isSessionBegun = false;

    public bool IsSessionBegun
    {
        get
        {
            return isSessionBegun;
        }
    }

    QBSessionManager session = new QBSessionManager();

    static SessionManager() { }

    private SessionManager() { }

    public static SessionManager Instance
    {
        get
        {
            return instance;
        }
    }

    public void OpenConnection()
    {
        if (!isConnectionOpen)
        {
            // Args: App ID, App Name
            session.OpenConnection("", "YOUR_APP_NAME");
            isConnectionOpen = true;
        }
    }

    public void BeginSession()
    {
        try
        {
            if (!isSessionBegun)
            {
                // By passing `""` for QB Filename, begin session with currently open QB Filename
                // Args: QB Filename, ENOpenMode
                session.BeginSession("", ENOpenMode.omDontCare);
                isSessionBegun = true;
            }
        }
        catch (Exception ex)
        {
            // Handle exception
        }

    }

    public void EndSession()
    {
        if (isSessionBegun)
        {
            session.EndSession();
        }
    }

    public void CloseConnection()
    {
        if (isConnectionOpen)
        {
            session.CloseConnection();
        }
    }

    public string GetCurrentCompanyFileName()
    {
        return session.GetCurrentCompanyFileName();
    }

    public IMsgSetRequest CreateMsgSetRequest()
    {
        // Args: Request Country, QBFC Major Version, QBFC Minor Version
        return session.CreateMsgSetRequest("US", 13, 0);
    }

    public IMsgSetResponse DoRequests(IMsgSetRequest requestSet)
    {
        return session.DoRequests(requestSet);
    }
}