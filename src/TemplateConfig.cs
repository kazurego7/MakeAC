using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.Json;

public class TemplateConfig
{
    private static string configFileName = ".actemp";
    private static string configFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), configFileName);

    public TemplateConfig()
    {
        if (File.Exists(configFilePath))
        {
            templateConfigDAO = JsonSerializer.Deserialize<TemplateConfigDAO>(File.ReadAllText(configFilePath));
        }
        else
        {
            var templateConfigDAO = new TemplateConfigDAO();
            templateConfigDAO.templates = new Dictionary<string, TemplateDAO>();
            this.templateConfigDAO = templateConfigDAO;
            Write();
        }
    }

    private TemplateConfigDAO templateConfigDAO;

    public void Write()
    {
        File.WriteAllText(configFilePath, JsonSerializer.Serialize<TemplateConfigDAO>(templateConfigDAO));
    }

    public bool IsInstalled(string templateName)
    {
        return templateConfigDAO.templates.ContainsKey(templateName);
    }

    public bool IsInvalidPath(string templateName)
    {
        return !IsInstalled(templateName) || !Directory.Exists(templateConfigDAO.templates[templateName].path);
    }

    public void Add(Template template)
    {
        templateConfigDAO.templates.Add(template.name, new TemplateDAO { name = template.name, path = template.path });
    }

    public void Update(Template template)
    {
        templateConfigDAO.templates[template.name].path = template.path;
    }

    public void Remove(string templateName)
    {
        if (IsInstalled(templateName))
        {
            templateConfigDAO.templates.Remove(templateName);
        }
    }

    public Template Get(string templateName)
    {
        if (!IsInstalled(templateName))
        {
            throw new IOException("Does not exist template : " + templateName);
        }

        var templateDAO = templateConfigDAO.templates[templateName];
        return new Template { name = templateDAO.name, path = templateDAO.path };

    }

    public IEnumerable<Template> ListTemplate()
    {
        return templateConfigDAO.templates
            .Select(kv => new Template { name = kv.Value.name, path = kv.Value.path });
    }
}