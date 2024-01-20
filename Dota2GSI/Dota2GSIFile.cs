using Dota2GSI.Utils;
using System;
using System.IO;

namespace Dota2GSI
{
    /// <summary>
    /// Class handling Game State Integration configuration file generation.
    /// </summary>
    public class Dota2GSIFile
    {
        /// <summary>
        /// Attempts to create a Game State Integration configuraion file.<br/>
        /// The configuration will target <c>http://localhost:{port}/</c> address.<br/>
        /// Returns true on success, false otherwise.
        /// </summary>
        /// <param name="name">The name of your integration.</param>
        /// <param name="port">The port for your integration.</param>
        /// <returns>Returns true on success, false otherwise.</returns>
        public static bool CreateFile(string name, int port)
        {
            return CreateFile(name, $"http://localhost:{port}/");
        }

        /// <summary>
        /// Attempts to create a Game State Integration configuraion file.<br/>
        /// The configuration will target the specified URI address.<br/>
        /// Returns true on success, false otherwise.
        /// </summary>
        /// <param name="name">The name of your integration.</param>
        /// <param name="uri">The URI for your integration.</param>
        /// <returns>Returns true on success, false otherwise.</returns>
        public static bool CreateFile(string name, string uri)
        {
            string game_path = SteamUtils.GetGamePath(570);

            try
            {
                if (!string.IsNullOrWhiteSpace(game_path))
                {
                    string gsifolder = game_path + @"\game\dota\cfg\gamestate_integration\";
                    Directory.CreateDirectory(gsifolder);
                    string gsifile = gsifolder + @$"gamestate_integration_{name}.cfg";

                    ACF provider_configuration = new ACF();
                    provider_configuration.Items["auth"] = "1";
                    provider_configuration.Items["provider"] = "1";
                    provider_configuration.Items["map"] = "1";
                    provider_configuration.Items["player"] = "1";
                    provider_configuration.Items["hero"] = "1";
                    provider_configuration.Items["abilities"] = "1";
                    provider_configuration.Items["items"] = "1";
                    provider_configuration.Items["events"] = "1";
                    provider_configuration.Items["buildings"] = "1";
                    provider_configuration.Items["league"] = "1";
                    provider_configuration.Items["draft"] = "1";
                    provider_configuration.Items["wearables"] = "1";
                    provider_configuration.Items["minimap"] = "1";
                    provider_configuration.Items["roshan"] = "1";
                    provider_configuration.Items["couriers"] = "1";
                    provider_configuration.Items["neutralitems"] = "1";

                    ACF gsi_configuration = new ACF();
                    gsi_configuration.Items["uri"] = uri;
                    gsi_configuration.Items["timeout"] = "5.0";
                    gsi_configuration.Items["buffer"] = "0.1";
                    gsi_configuration.Items["throttle"] = "0.1";
                    gsi_configuration.Items["heartbeat"] = "10.0";
                    gsi_configuration.Children["data"] = provider_configuration;

                    ACF gsi = new ACF();
                    gsi.Children[$"{name} Integration Configuration"] = gsi_configuration;

                    File.WriteAllText(gsifile, gsi.ToString());

                    return true;
                }
            }
            catch (Exception)
            {
            }

            return false;
        }
    }
}
