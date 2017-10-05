using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    class PersonDAO_SQLite : IPersonDAO
    {
        SQLiteConnection connection = null;
        public PersonDAO_SQLite(string path)
        {
            connection = new SQLiteConnection(path);
            connection.CreateTable<Person>();
        }

        public void Create(Person person)
        {
            connection.Insert(person);
        }

        public void Delete(Person person)
        {
            connection.Delete(person);
        }

        public List<Person> Read()
        {
            return connection.Table<Person>().ToList();
        }

        public Person Read(int id)
        {
            return connection.Find<Person>(id);
        }

        public void Update(Person person)
        {
            connection.Update(person);
        }
    }
}
