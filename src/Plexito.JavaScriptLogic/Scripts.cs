namespace Plexito.JavaScriptLogic
{
    using System.IO;
    using System.Reflection;

    public class Scripts
    {
        static public string PlaybackApi
        {
            get
            {
                // make it Lazy<string>.

                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = "Plexito.JavaScriptLogic.JavaScript.PlaybackApi.js";

                string result;

                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                using (StreamReader reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }

                resourceName = "Plexito.JavaScriptLogic.JavaScript.PlaybackApiForJint.js";

                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                using (StreamReader reader = new StreamReader(stream))
                {
                    result += reader.ReadToEnd();
                }
                return result;
            }            
        }
    }
}