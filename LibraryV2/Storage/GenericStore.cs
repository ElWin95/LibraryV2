using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryV2.Storage.IIdentity
{
    [Serializable]
    public class GenericStore<T> : IEnumerable<T>
        where T : IIdentity
    {
        T[] data = new T[0];

        public void Add(T thing)
        {
            int len = data.Length;

            Array.Resize(ref data, len + 1);
            data[len] = thing;
        }
        public void Remove(T thing)
        {
            int index = Array.IndexOf(data, thing);
            for (int i = index; i < data.Length - 1; i++)
            {
                data[i] = data[i + 1];
            }
            Array.Resize(ref data, data.Length - 1);
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in data)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        public T this[int index]
        {
            get
            {
                return data[index];
            }

        }
        public int Length
        {
            get
            {
                return data.Length;
            }
        }

        public T Find(int id)
        {
            return Array.Find(data, x => x.Id == id);
        }

        public bool Any(Predicate<T> bax)
        {
            return Array.Exists(data, bax);
        }

        public T[] FindName(string name)
        {
            return Array.FindAll(data, x => x.Name.Contains(name));

        }
    }

    public interface IIdentity
    {
        public int Id { get; }
        public string Name { get; }

    }
}
