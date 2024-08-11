using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Singleton
{
    /// <summary>
    /// This class is converting to singleton class who inherit from it.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SingletonBase<T> where T : SingletonBase<T>, new()
    {
        private static readonly Lazy<T> _instance = new Lazy<T>(() => new T());
        protected SingletonBase() { }
        public static T Instance
        {
            get
            {
                return _instance.Value;
            }
        }
    }
}
