using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Youtube_Master
{
    class PlayList
    {
        List<JObject> list = new List<JObject>();
        public int index = 0;
        public int totalIndex = 0;
        public bool shuffle;
        public bool repeat;

        public PlayList()
        {
            this.shuffle = false;
            this.repeat = false;
            this.index = 0;
        }

        public List<JObject> getPlayList()
        {
            return list;
        }

        public void AddList(JObject next)
        {
            list.Add(next);
            this.totalIndex++;
        }

        public void RemoveList(params int[] _index)
        {
            for (int i = _index.Length - 1; i >= 0; i++)
            {
                if (_index[i] < this.index) this.index -= 1;
                list.Remove(this.list[_index[i]]);
            }
            this.totalIndex -= _index.Length;
            if (this.totalIndex < 0) this.totalIndex = 0;
        }

        public void AddList(params JObject[] obj)
        {
            for (int i = 0; i < obj.Length; i++)
            {
                list.Add(obj[i]);
                this.totalIndex += 1;
            }
        }

        public JObject GetNextList()
        {
            if (this.shuffle)
            {
                var prevIndex = this.index;
                Random rand = new Random();
                this.index = rand.Next(this.totalIndex);
                while(prevIndex == this.index)
                {
                    this.index = rand.Next(this.totalIndex);
                }
            }
            else
            {
                this.index += 1;
                if (this.index >= totalIndex)
                {
                    this.index = 0;
                }
            }
            return this.list[this.index];
        }

        public JObject GetItemFromIndex(int index)
        {
            this.index = index;
            return this.list[this.index];
        }

        public JObject GetPreList()
        {
            if (this.shuffle)
            {
                var prevIndex = this.index;
                Random rand = new Random();
                this.index = rand.Next(this.totalIndex);
                while (prevIndex == this.index)
                {
                    this.index = rand.Next(this.totalIndex);
                }
            }
            else
            {
                this.index -= 1;

                if (this.index < 0)
                {
                    this.index = this.totalIndex-1;
                }
            }
            return this.list[this.index];
        }

        public JObject GetNowList()
        {
            return this.list[this.index];
        }

    }
}
