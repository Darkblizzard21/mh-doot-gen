using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace RingingBloom.WWiseTypes
{
    class WSource
    {
        public static void MakeWSource(string outfile, List<string> wavs)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.Encoding = Encoding.UTF8;
            settings.OmitXmlDeclaration = true;
            if (!Directory.GetParent(outfile).Exists)
            {
                Directory.CreateDirectory(Directory.GetParent(outfile).FullName);
            }
            XmlWriter xml = XmlWriter.Create(outfile, settings);
            xml.WriteStartElement("ExternalSourcesList");
            xml.WriteAttributeString("SchemaVersion", "1");
            xml.WriteAttributeString("Root", "");
            for (int i = 0; i < wavs.Count; i++)
            {
                xml.WriteStartElement("Source");
                xml.WriteAttributeString("Path", wavs[i]);
                xml.WriteAttributeString("Conversion", "Vorbis Quality High");//should make this variable, but this is fine so far
                xml.WriteEndElement();
            }
            xml.WriteEndElement();
            xml.Flush();
            xml.Close();
        }
    }
}
