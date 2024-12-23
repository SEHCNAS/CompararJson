using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

//Classe responsavel por comparar dois json e identificar as diferença entre eles
public class CompararJson
{
    public string CompararJsons(string json1, string json2)
    {
        try
        {
            JObject obj1 = JObject.Parse(json1);
            JObject obj2 = JObject.Parse(json2);

            List<JObject> differences = new List<JObject>();
            CompararJObjects(obj1, obj2, differences);

            return JsonConvert.SerializeObject(differences);
        }
        catch (Exception ex)
        {
            return JsonConvert.SerializeObject(new { error = ex.Message });
        }
    }

    //Metodo recursivo que compara as propriedades de dois jsons
    private void CompararJObjects(JObject obj1, JObject obj2, List<JObject> differences)
    {
        // Itera sobre todas as propriedades do primeiro objeto JSON
        foreach (var property in obj1.Properties())
        {
            string currentPath = property.Name; // Nome da propriedade atual

            // Verifica se a propriedade não está presente no segundo objeto
            if (!obj2.ContainsKey(property.Name))
            {
                // Adiciona uma diferença indicando que o valor está faltando no segundo JSON
                differences.Add(new JObject
                {
                    ["Campo"] = currentPath,
                    ["status"] = "Valor faltando no segundo json",
                    ["Valor"] = property.Value
                });
                continue;
            }
            // Obtém os valores das propriedades correspondentes nos dois objetos
            JToken value1 = property.Value;
            JToken value2 = obj2[property.Name];

            // Se os valores forem objetos aninhados, chama recursivamente o método para compará-los
            if (value1.Type == JTokenType.Object && value2.Type == JTokenType.Object)
            {
                CompararJObjects((JObject)value1, (JObject)value2, differences);
            }
            // Caso os valores sejam diferentes, adiciona a diferença à lista
            else if (!JToken.DeepEquals(value1, value2))
            {
                
                differences.Add(new JObject
                {
                    ["Campo"] = currentPath,
                    ["status"] = "Valor diferente",
                    ["Valor1"] = value1,
                    ["Valor2"] = value2
                });
            }
        }

        // Itera sobre todas as propriedades do segundo objeto JSON
        foreach (var property in obj2.Properties())
        {
            string currentPath = property.Name;

            // Verifica se a propriedade não está presente no primeiro objeto
            if (!obj1.ContainsKey(property.Name))
            {
                // Adiciona uma diferença indicando que o valor está faltando no primeiro JSON
                differences.Add(new JObject
                {
                    ["Campo"] = currentPath,
                    ["status"] = "Valor faltando no primeiro json",
                    ["Valor1"] = property.Value
                });
            }
        }
    }
}
