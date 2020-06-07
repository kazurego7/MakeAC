using System.Collections.Generic;
public class TemplateConfigDAO
{
    public Dictionary<string, TemplateDAO> templates { get; set; }
}

public class TemplateDAO
{
    public string name { get; set; }
    public string path { get; set; }
}

