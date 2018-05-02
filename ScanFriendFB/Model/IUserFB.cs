using System;

namespace ScanFriendFB
{
    internal interface IUserFB
    {
        string id { get; set; }
        string name { get; set; }
        string relationship_status { get; set; }
        DateTime age_range { get; set; }
    }
}