using System;

namespace SphereDBFileAdapter
{
    public class SphereDBFileAdapter
    {
        public SphereDBFileAdapter(string path)
        {
            // does path have a file on it?
            if (true)
            {
                // file name is bootstrap into sphere

                Init(path, "init single or array");
            }
            else
            {
                // look for default bootstrap, or bootstrap wildcard for many personas on home
                // add default file list here like IIS has

                // if any are found in sequence, put them here
                Init(path, "found default file");

            }

        }

        public SphereDBFileAdapter(string path, string file)
        {
            // check to see if valid
            if (true)
            {
                Init(path, file);
            }
        }

        public void Init (string path, string file)
        {
            // common linked list file parser.

            // linq methods to be added here and belwo >>><<>>>><><><><
        }
    }
}
