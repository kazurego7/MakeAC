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
        return TemplateConfigDAOCache.templates.ContainsKey(templateName);
    }

    public bool IsInvalidPath(string templateName)
    {
        return !IsInstalled(templateName) || !Directory.Exists(TemplateConfigDAOCache.templates[templateName].path);
    }

    public void Add(Template template)
    {
        var nextCache = TemplateConfigDAOCache;
        nextCache.templates.Add(template.name, new TemplateDAO { name = template.name, path = template.path });
        TemplateConfigDAOCache = nextCache;
    }

    public void Update(Template template)
    {
        TemplateConfigDAOCache.templates[template.name] = new TemplateDAO { path = template.path };
    }

    public void Remove(string templateName)
    {
        if (IsInstalled(templateName))
        {
            var nextCache = TemplateConfigDAOCache;
            nextCache.templates.Remove(templateName);
            TemplateConfigDAOCache = nextCache;
        }
    }

    public Template Get(string templateName)
    {
        if (!IsInstalled(templateName))
        {
            throw new IOException("Does not exist template : " + templateName);
        }

        var templateDAO = TemplateConfigDAOCache.templates[templateName];
        return new Template { name = templateDAO.name, path = templateDAO.path };

    }

    public IEnumerable<Template> ListTemplate()
    {
        return TemplateConfigDAOCache.templates
            .Select(kv => new Template { name = kv.Value.name, path = kv.Value.path });
    }
}