using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.Json;

public class TemplateConfigFile
{
    private static string configFileName = ".actemp";
    private static string configFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), configFileName);

    private TemplateConfigDAO TemplateConfigDTOCache
    {
        get => TemplateConfigDTOCache ?? JsonSerializer.Deserialize<TemplateConfigDAO>(File.ReadAllText(configFilePath));
        set => TemplateConfigDTOCache = value;
    }

    public void Write()
    {
        File.WriteAllText(configFilePath, JsonSerializer.Serialize<TemplateConfigDAO>(TemplateConfigDTOCache));
    }

    public void Init()
    {
        if (!File.Exists(configFilePath))
        {
            var templateConfigDTO = new TemplateConfigDAO();
            templateConfigDTO.templates = new Dictionary<string, TemplateDAO>();
            TemplateConfigDTOCache = templateConfigDTO;
            Write();
        }
    }

    public bool Contains(string templateName)
    {
        return TemplateConfigDTOCache.templates.ContainsKey(templateName);
    }

    public bool IsRemoved(string templateName)
    {
        return TemplateConfigDTOCache.templates[templateName].removeFlag;
    }

    public void Add(string templateName, string templatePath)
    {
        TemplateConfigDTOCache.templates.Add(templateName, new TemplateDAO { name = templateName, path = templatePath, removeFlag = false });
    }

    public void Update(string templateName, string templatePath)
    {
        TemplateConfigDTOCache.templates[templateName] = new TemplateDAO { path = templatePath, removeFlag = false };
    }

    public void Remove(string templateName)
    {
        TemplateConfigDTOCache.templates[templateName].removeFlag = true;
    }

    public void Restore(string templateName)
    {
        TemplateConfigDTOCache.templates[templateName].removeFlag = false;
    }

    public IEnumerable<Template> ListInstalledTemplate()
    {
        return TemplateConfigDTOCache.templates
            .Where(kv => !kv.Value.removeFlag)
            .Select(kv => new Template { name = kv.Value.name, path = kv.Value.path });
    }

    public IEnumerable<Template> ListRemovedTemplate()
    {
        return TemplateConfigDTOCache.templates
            .Where(kv => kv.Value.removeFlag)
            .Select(kv => new Template { name = kv.Value.name, path = kv.Value.path });
    }
}