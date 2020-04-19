using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.Json;

public class TemplateConfig
{
    private static string configFileName = ".actemp";
    private static string configFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), configFileName);

    private TemplateConfigDAO _templateConfigDAOCache;
    private TemplateConfigDAO TemplateConfigDAOCache
    {
        get => _templateConfigDAOCache ?? JsonSerializer.Deserialize<TemplateConfigDAO>(File.ReadAllText(configFilePath));
        set => _templateConfigDAOCache = value;
    }

    public void Write()
    {
        File.WriteAllText(configFilePath, JsonSerializer.Serialize<TemplateConfigDAO>(TemplateConfigDAOCache));
    }

    public void Init()
    {
        if (!File.Exists(configFilePath))
        {
            var templateConfigDTO = new TemplateConfigDAO();
            templateConfigDTO.templates = new Dictionary<string, TemplateDAO>();
            TemplateConfigDAOCache = templateConfigDTO;
            Write();
        }
    }

    public bool IsInstalled(string templateName)
    {
        return TemplateConfigDAOCache.templates.ContainsKey(templateName) && !TemplateConfigDAOCache.templates[templateName].removeFlag;
    }

    public bool IsRemoved(string templateName)
    {
        return TemplateConfigDAOCache.templates.ContainsKey(templateName) && TemplateConfigDAOCache.templates[templateName].removeFlag;
    }

    public bool IsInvalidPath(string templateName)
    {
        return !IsInstalled(templateName) || !Directory.Exists(TemplateConfigDAOCache.templates[templateName].path);
    }

    public void Add(Template template)
    {
        if (TemplateConfigDAOCache.templates.ContainsKey(template.name))
        {
            TemplateConfigDAOCache.templates[template.name] = new TemplateDAO { path = template.path, removeFlag = false };
        }
        else
        {
            var nextCache = TemplateConfigDAOCache;
            nextCache.templates.Add(template.name, new TemplateDAO { name = template.name, path = template.path, removeFlag = false });
            TemplateConfigDAOCache = nextCache;
        }
    }

    public void Remove(string templateName)
    {
        if (IsInstalled(templateName))
        {
            var nextCache = TemplateConfigDAOCache;
            nextCache.templates[templateName].removeFlag = true;
            TemplateConfigDAOCache = nextCache;
        }
    }

    public void Restore(string templateName)
    {
        if (IsRemoved(templateName))
        {
            var nextCache = TemplateConfigDAOCache;
            nextCache.templates[templateName].removeFlag = false;
            TemplateConfigDAOCache = nextCache;
        }
    }

    public Template Get(string templateName)
    {
        if (!IsInstalled(templateName) && !IsRemoved(templateName))
        {
            throw new IOException("Does not exist template : " + templateName);
        }

        var templateDAO = TemplateConfigDAOCache.templates[templateName];
        return new Template { name = templateDAO.name, path = templateDAO.path };

    }

    public IEnumerable<Template> ListInstalledTemplate()
    {
        return TemplateConfigDAOCache.templates
            .Where(kv => !kv.Value.removeFlag)
            .Select(kv => new Template { name = kv.Value.name, path = kv.Value.path });
    }

    public IEnumerable<Template> ListRemovedTemplate()
    {
        return TemplateConfigDAOCache.templates
            .Where(kv => kv.Value.removeFlag)
            .Select(kv => new Template { name = kv.Value.name, path = kv.Value.path });
    }
}