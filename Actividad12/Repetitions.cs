using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad12
{
    [Serializable]
    class Repetitions
    {
        private string word;
        private int repetitions;
        private HashSet<string> repeatsPerFile = new HashSet<string>();

        public Repetitions(string w, string fileName)
        {
            word = w;
            repetitions = 1;
            repeatsPerFile.Add(fileName);
        }

        public void addWord(string fileName)
        {
            repetitions++;
            repeatsPerFile.Add(fileName);
        }

        public override bool Equals(object rc)
        {
            return Equals(rc as Repetitions);
        }

        public int getRepetitions()
        {
            return this.repetitions;
        }

        public int getRepeatsCount()
        {
            return this.repeatsPerFile.Count();
        }

        public HashSet<string> getFilesFoundIn()
        {
            return this.repeatsPerFile;
        }
    }
}
