
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrystalUpdater.Data
{
    [Serializable]
    public class UpdateSaveFile
    {
        public UpdateSaveFile(String version)
        {
            VersionString = version;
            UpdateFileCollection = new List<UpdateFileInfo>();
        }

        public String VersionString { get; set; }
        private List<UpdateFileInfo> _coll;

        public List<UpdateFileInfo> UpdateFileCollection
        {
            get { return _coll; }
            set { _coll = value; }
        }

        
    }
}
