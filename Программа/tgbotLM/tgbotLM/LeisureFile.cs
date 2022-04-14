using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tgbotLM
{
    public class LeisureFile
    {
        private Random r;
        public List<(string, string, string)> Place { get; private set; }

        public LeisureFile(string path)
        {
            string[] data = System.IO.File.ReadAllLines(path);
            Place = data
                .Select(s => s.Split('|'))
                .Select(s => (s[0], s[1], s[2]))
                .ToList();

            r = new Random();

        }

        public string GetRandomLeisure()
        {
            int x = r.Next(Place.Count-1);
            return Place[x].Item1 + "\n" + Place[x].Item2 + "\n" + Place[x].Item3;
        }
    }
}
