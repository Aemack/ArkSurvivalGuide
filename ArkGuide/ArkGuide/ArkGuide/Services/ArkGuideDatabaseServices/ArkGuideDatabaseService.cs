using ArkGuide.Models;
using ArkGuide.Models.CreatureModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Xamarin.Essentials;

namespace ArkGuide.Services.ResourceMapServices
{
    public class ArkGuideDatabaseService
    {
        public ResourceMapCollection ResourceMapCollection { get; set; }
        public DinoCollection DinoCollection { get; set; }

        public ResourceMapCollection LoadMapDetails()
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(ResourceMapCollection)).Assembly;
            XmlSerializer serializer = new XmlSerializer(typeof(ResourceMapCollection));
            
            using (Stream reader = assembly.GetManifestResourceStream("ArkGuide.ResourceMaps.xml"))
            {
                ResourceMapCollection = (ResourceMapCollection)serializer.Deserialize(reader);
                return ResourceMapCollection;
            }
        }
        
        public DinoCollection LoadDinoDetails()
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(DinoCollection)).Assembly;
            XmlSerializer serializer = new XmlSerializer(typeof(DinoCollection));
            
            using (Stream reader = assembly.GetManifestResourceStream("ArkGuide.CreatureInfo.xml"))
            {
                DinoCollection = (DinoCollection)serializer.Deserialize(reader);
                return DinoCollection;
            }
        }
    }
}
