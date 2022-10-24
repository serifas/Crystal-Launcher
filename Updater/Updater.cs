using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.ComponentModel;
using System.IO;
using CrystalUpdater.Data;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;
using System.Windows.Forms;
using CrystalUpdater.UI;
using System.Resources;
using System.Globalization;
using System.Diagnostics;

namespace CrystalUpdater
{
    public class Updater : Component
    {
        private const String LocalUpdateFile = @".\UpdateInfo.dat";

        public String UpdateUrl { get; set; }

        public void CheckForUpdates()
        {
            try
            {
                CleanUp();

                WebClient downloadClient = new WebClient();
                downloadClient.DownloadFile(UpdateUrl, LocalUpdateFile);
                downloadClient.Dispose();

                if (!File.Exists(LocalUpdateFile))
                    throw new FileNotFoundException("The local update file is missing!", LocalUpdateFile);

                UpdateSaveFile localFile = DecodeSaveFile(LocalUpdateFile);

                Version localVersion = Assembly.GetEntryAssembly().GetName().Version;
                Version onlineVersion = Version.Parse(localFile.VersionString);
                //check to see if the onlineVersion is higher than the localVersion
                if (onlineVersion > localVersion)
                {
                    if (MessageBox.Show(String.Format("Version {0} available,\nInstall it now?", onlineVersion.ToString()), "Crystal Refuge Updater", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        frmUpdater updateForm = new frmUpdater(localFile, GetPath(UpdateUrl));
                        updateForm.ShowDialog();                        
                    }

                }
                else
                {
                    //run the application
                    //example 
                    Process.Start(@"wow.exe");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error checking for updates\ntry again later!\n\nError Message:" + e.Message, "Crystal Refuge Updater", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       
        private string GetPath(string UpdateUrl)
        {
            StringBuilder sb = new StringBuilder();
            String[] updatePieces = UpdateUrl.Split('/');
            for (int i = 0; i < updatePieces.Length -1; i++)
            {
                sb.Append(updatePieces[i] + "/");
            }
            return sb.ToString();
        }

        private UpdateSaveFile DecodeSaveFile(string LocalUpdateFile)
        {
            FileStream localFileStream = null;
                BinaryFormatter decoder = null;
                try
                {
                    localFileStream = File.Open(LocalUpdateFile, FileMode.Open, FileAccess.Read);
                    decoder = new BinaryFormatter();
                    return (UpdateSaveFile)decoder.Deserialize(localFileStream);
                }
                catch (Exception e)
                {
                    throw new InvalidDataException("The local update info file is corrupt!", e);
                }
                finally
                {
                    if (localFileStream != null)
                        localFileStream.Dispose();
                }

        }

        private void CleanUp()
        {
            DirectoryInfo di = new DirectoryInfo(Application.StartupPath);
            foreach (FileInfo fi in di.GetFiles("*.old"))
            {
                fi.Delete();
            }
        }
    }
}
