using System.Collections.Generic;
public class ACTemplateConfig
{
    public Dictionary<string, ACTemplate> templates { get; set; }
}

public class ACTemplate
{
    public string path { get; set; }
    public bool removeFlag { get; set; }
}

