using Realms;
using System.Collections.Generic;
using System.Linq;

namespace DB
{
    class PersonDAO_Realm : IPersonDAO
    {
        Realm realm = null;
        public PersonDAO_Realm()
        {
            realm = Realm.GetInstance();
        }

        public void Create(Person person)
        {
            realm.Write(() =>
            {
                realm.Add(new PersonR(person), false);
            });
        }

        public void Delete(Person person)
        {
            var del = realm.All<PersonR>().First(b => b.Id == person.Id);

            using (var trans = realm.BeginWrite())
            {
                realm.Remove(del);
                trans.Commit();
            }
        }

        public List<Person> Read()
        {
            List<Person> listPR = new List<Person>();
            var people = realm.All<PersonR>();
            foreach (PersonR pr in people)
            {
                listPR.Add(pr.GetPerson());
            }
            return listPR;
        }

        public Person Read(int id)
        {
            return realm.Find<PersonR>(id).GetPerson();
        }

        public void Update(Person person)
        {
            realm.Write(() => realm.Add(new PersonR(person), true));
        }
    }
}