using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventObject
{
    public string numOfOrgs { get; set; }
    public IEnumerable<IDictionary<string, string>> orgs { get; set; }
    public string uid { get; set; }
}
