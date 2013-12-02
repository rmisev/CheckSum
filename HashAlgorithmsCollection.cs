using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CheckSum
{
    /***
    class HashAlgorithmItem
    {
        public delegate HashAlgorithm CreateAction();        

        public HashAlgorithmItem(string name, CreateAction create)
        {
            this._name = name;
            this._create = create;
        }

        public string Name { get { return this._name; } }
        public CreateAction Create { get { return this._create; } }

        private string _name;
        private CreateAction _create;
    }
    ***/

    class HashAlgorithmsCollection
    {
        public delegate HashAlgorithm CreateAction();
        private static Dictionary<string, CreateAction> dic = null;

        public static void Init()
        {
            if (dic == null)
            {
                dic = new Dictionary<string, CreateAction>(5);
                dic.Add("MD5", MD5.Create);
                dic.Add("SHA1", SHA1Managed.Create);
                dic.Add("SHA256", SHA256Managed.Create);
                dic.Add("SHA384", SHA384Managed.Create);
                dic.Add("SHA512", SHA512Managed.Create);
            }
        }

        public static Dictionary<string, CreateAction> Dic { get { return dic; } }
        public static HashAlgorithm Create(string key)
        {
            return dic[key]();
        }
    }
}
