using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace twclient
{
    class Tweet
    {
        // 1つのつぶやきを管理

        private string tweetData;

        public Tweet(string tweetData)
        {
            this.tweetData = tweetData;
        }

        public void Set(string tweetData)
        {
            this.tweetData = tweetData;
        }

        public string Get()
        {
            return this.tweetData;
        }
    }
}
