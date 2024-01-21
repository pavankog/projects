using System;
using System.Xml;

class Program
{
    static void Main()
    {
        // Replace with your input XML file path
        string inputXmlFilePath = "C:/Users/shivm/OneDrive/Desktop/new.xml";

        // Load input XML document
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(inputXmlFilePath);

        // Create a new XML document for output
        XmlDocument outputXmlDoc = new XmlDocument();
        outputXmlDoc.AppendChild(outputXmlDoc.CreateXmlDeclaration("1.0", "utf-8", null));

        // Create a new Root element for the output XML document
        XmlElement rootElement = outputXmlDoc.CreateElement("Root");
        outputXmlDoc.AppendChild(rootElement);

        // Iterate over all nodes in the input XML
        foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes)
        {
            if (node.Name == "XMLOutput" && node.SelectSingleNode("Key")?.InnerText.StartsWith("file") == true)
            {
                // Create the original node with _Original Key
                XmlNode originalNode = outputXmlDoc.CreateElement("XMLOutput");

                XmlNode originalKeyNode = outputXmlDoc.CreateElement("Key");
                XmlNode originalValueNode = outputXmlDoc.CreateElement("Value");

                string originalKey = node.SelectSingleNode("Key").InnerText;
                string originalValue = node.SelectSingleNode("Value").InnerText;

                originalKeyNode.InnerText = originalKey + "_Original";
                originalValueNode.InnerText = originalValue;

                originalNode.AppendChild(originalKeyNode);
                originalNode.AppendChild(originalValueNode);

                // Append the original node to the output XML document
                rootElement.AppendChild(originalNode);

                // Create a new node with the modified Key and Value
                XmlNode newNode = outputXmlDoc.CreateElement("XMLOutput");

                XmlNode newKeyNode = outputXmlDoc.CreateElement("Key");
                XmlNode newValueNode = outputXmlDoc.CreateElement("Value");

                newKeyNode.InnerText = originalKey;
                newValueNode.InnerText = $"\\\\networkpath\\folder\\{originalValue.Split('/').Last()}";

                newNode.AppendChild(newKeyNode);
                newNode.AppendChild(newValueNode);

                // Append the new node to the output XML document
                rootElement.AppendChild(newNode);
            }
            else
            {
                // Append other nodes as they are to the output XML document
                rootElement.AppendChild(outputXmlDoc.ImportNode(node, true));
            }
        }

        // Save the output XML document
        string outputXmlFilePath = "C:/Users/shivm/OneDrive/Desktop/" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_updated_new.xml";
        outputXmlDoc.Save(outputXmlFilePath);

        Console.WriteLine("New XML file created successfully: " + outputXmlFilePath);
    }
}
