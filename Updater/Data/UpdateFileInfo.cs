using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrystalUpdater.Data
{
    [Serializable]
    public class UpdateFileInfo
    {
        public UpdateFileInfo(String name, String desc, String path)
        {
            Name = name;
            Description = desc;
            Path = path;
        }

        public String Name { get; set; }
        public String Description { get; set; }
        public String Path { get; set; }
    }
}
