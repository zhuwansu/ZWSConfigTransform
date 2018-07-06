using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ZWS.ConfigTransform
{
    public static class ConfigTransformer
    {
        public static void Transform(string configName, string sourceDir, string targetFile)
        {

            var baseConfigNode = XElement.Load(targetFile);
            var targetConfigNode = XElement.Parse(File.ReadAllText(sourceDir + $"{configName}.config"));

            ReplaceAddElements("appSettings", "key", baseConfigNode, targetConfigNode);

            ReplaceAddElements("connectionStrings", "name", baseConfigNode, targetConfigNode);

            baseConfigNode.Save(targetFile);
        }

        static void ReplaceAddElements(string fElement, string matchAttrName, XElement baseConfigNode, XElement targetConfigNode)
        {
            var baseConfig = baseConfigNode.Element(fElement).Elements("add").ToArray();
            var targetConfig = targetConfigNode.Element(fElement).Elements("add").ToArray();

            var temp = baseConfig.ToDictionary((m) =>
            {
                return m.Attribute(matchAttrName).Value;
            });

            for (int j = 0; j < targetConfig.Length; j++)
            {
                var targetItem = targetConfig[j];
                var targetKey = targetItem.Attribute(matchAttrName).Value;
                temp[targetKey].ReplaceWith(targetItem);
            }
        }
    }
}
